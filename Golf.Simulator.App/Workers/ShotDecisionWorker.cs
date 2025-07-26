using Golf.Simulator.App.Models;
using Golf.Simulator.App.Interfaces;
using System.Numerics;

namespace Golf.Simulator.App.Workers
{
    public  class ShotDecision : IShotDecision
    {
        public Shot Decision(Player player, Course course, Bag bagDistance, CurrentBallPosition currentBall)//Wind wind, Lie lie, int altitude, int temp, Rain rain)
        {
            int idealTolerance = 20 + player.attributes.mental.courseManagement; // This can be adjusted based on player skill or conditions
            int riskyTolerance = 10 + player.attributes.mental.courseManagement; // This can be adjusted based on player skill or conditions
            int conservitiveTolerance = 40 + player.attributes.mental.courseManagement; // This can be adjusted based on player skill or conditions

            var idealShot = GetShot(course, player, bagDistance, currentBall, idealTolerance); //wind, lie, altitude, temp, rain, );
            var riskyShot = GetShot(course, player, bagDistance, currentBall, riskyTolerance); //wind, lie, altitude, temp, rain, );
            var conservitiveShot = GetShot(course, player, bagDistance, currentBall, conservitiveTolerance); //wind, lie, altitude, temp, rain, );

            //Decide on a shot based on players mental state and attributes
            // Mental state is a value between 0 and 100, where 0 is very aggressive and 100 is very calm multiplied by 10 to get a value between 0 and 100
            return PlayerDecision(idealShot, riskyShot, conservitiveShot, player);
        }
        Shot GetShot(Course course, Player player, Bag bagDistance, CurrentBallPosition currentBall, int tolerance) // Wind wind, Lie lie, int altitude, int temp, Rain rain,)
        {
            // TODO: Determine target
            var IsBallOnGreen = CheckIfBallIsOnGreen(course, currentBall);
            if (IsBallOnGreen)
            {
                // If the ball is on the green, we can putt
                return new Shot
                {
                    ClubChoice = "Putter",
                    BallPosition = course.holes[currentBall.holeNumber].green.pin
                };
            }
            var target = DetermineTarget(course, player, bagDistance, currentBall, tolerance);
            
            var shot = ChooseClub(target, bagDistance, currentBall, tolerance);

            return shot;
        }
        Shot PlayerDecision(Shot idealShot, Shot riskyShot, Shot conservitiveShot, Player player)
        {
            Random rand = new Random();
            //Increase variance in mental state by adding a random number between 1 and 10
            int rando = 0;
            if (player.attributes.mental.mentalState != 10)
            {
                rando = rand.Next(1, 10);
            }
            else
            {
                rando = 0; // If mental state is 10, no variance
            }
            var mentalState = player.attributes.mental.mentalState * 10 + rando;

            if (player.attributes.mental.courseManagement != 10)
            {
                rando = rand.Next(1, 10);
            }
            else
            {
                rando = 0; // If mental state is 10, no variance
            }
            var courseManagement = player.attributes.mental.courseManagement * 10 + rando;

            //Demeanor is a value between 0 and 10, where 0 is very aggressive and 10 is very calm multiplied by 10 to get a value between 0 and 100
            var demeanor = player.attributes.mental.demeanor * 10;

            var decisionMaking = (mentalState + demeanor) / 3.3; // Number choosen so that 5's across the board will equal 50, which is the average mental state of a player with slight variance
            var halfManagement = courseManagement / 2; // Number choosen so that 5's across the board will equal 50, which is the average course management of a player with slight variance
            var percentageForShotDecision = decisionMaking + halfManagement; // Average of the two values to get a percentage for shot decision             

            rando = rand.Next(1, 100);
            // If the player is very calm, they will go for the ideal shot
            if (rando <= percentageForShotDecision)
            {
                // Player is calm, go for the ideal shot
                return idealShot;
            }
            else if (rando > percentageForShotDecision && decisionMaking < 30)
            {
                // Player is slightly aggressive, go for the risky shot
                return riskyShot;
            }
            else
            {
                // Player is very aggressive, go for the conservitive shot
                return conservitiveShot;
            }
        }
        Shot ChooseClub(Vector2 target, Bag bagDistance, CurrentBallPosition currentBall, int tolerance)
        {
            Shot shot = new Shot();
            var distanceToTarget = FindDistanceToTargetInYards(target, currentBall.ballPosition); // Assuming target.Y is the distance to the pin in yards
            foreach (var club in bagDistance.bag)
            {
                // Check if the target is within the club's distance range
                if (distanceToTarget <= club.ClubDistance + tolerance && distanceToTarget >= club.ClubDistance - tolerance)
                {
                    shot.ClubChoice = club.ClubName;
                    shot.BallPosition = new Vector2(target.X, target.Y);
                    return shot; // Return the first matching club
                }
            }
            return null;
        }
        bool CheckIfBallIsOnGreen(Course course, CurrentBallPosition currentBall)
        {
            // Get the current hole and its layout
            var hole = course.holes[currentBall.holeNumber];
            var green = course.holes[currentBall.holeNumber].green;
            // Check if the target is within the green area
            return currentBall.ballPosition.X >= green.greenLocation.startPosition.X && currentBall.ballPosition.X <= green.greenLocation.endPosition.X &&
                   currentBall.ballPosition.Y >= green.greenLocation.startPosition.Y && currentBall.ballPosition.Y <= green.greenLocation.endPosition.Y;
        }
        Vector2 GetLayupTarget (Locations fairway, Bag bagDistance, CurrentBallPosition currentBall, int tolerance)
        {
            var distanceToPin = fairway.endPosition.Y - currentBall.distanceToPin; // Assuming target.Y is the distance to the pin in yards
            foreach (var club in bagDistance.bag)
            {
                // Check if the target is within the club's distance range
                if (distanceToPin <= club.ClubDistance + tolerance && fairway.endPosition.Y >= club.ClubDistance - tolerance)
                {
                    return new Vector2(fairway.startPosition.X + (fairway.width / 2), club.ClubDistance);
                }
            }
            return new Vector2(0,0);
        }
        Vector2 DetermineTarget(Course course, Player player, Bag bagDistance, CurrentBallPosition currentBall, int tolerance)
        {
            // Get the current hole and its layout
            var hole = course.holes[currentBall.holeNumber];
            var holeLayout = course.holes[currentBall.holeNumber].holeLayout;
            var green = course.holes[currentBall.holeNumber].green;

            //Calculate the pin position and distance to pin
            var distanceToPin = FindDistanceToTargetInYards(green.pin, currentBall.ballPosition);
            currentBall.distanceToPin = distanceToPin; // Store the distance to pin in the current ball position
            //Select first fairway object
            var fairway = holeLayout.fairway.FirstOrDefault();

            Shot shot = new Shot();
            var target = new Vector2();

            // Check each type of hazard and all hazards within the hole layout
            foreach (var hazard in holeLayout.sand)
            {
                // Check if the hazard is between the ball and the pin
                var hazardBetweenBallAndPin = IsHarzardBetweenBallAndTarget(hazard, tolerance, hole, target);
                if (hazardBetweenBallAndPin != null)
                {
                    // If a hazard is found, we need to adjust the shot
                    //TODO: Risk Determination Logic
                }
            }
            foreach (var hazard in holeLayout.water)
            {
                // Check if the hazard is between the ball and the pin
                var hazardBetweenBallAndPin = IsHarzardBetweenBallAndTarget(hazard, tolerance, hole, target);
                if (hazardBetweenBallAndPin != null)
                {
                    // If a hazard is found, we need to adjust the shot
                    //Check for hazards between ball and pin
                    //TODO: Risk Determination Logic
                }
            }
            foreach (var hazard in holeLayout.deepRough)
            {
                // Check if the hazard is between the ball and the pin
                var hazardBetweenBallAndPin = IsHarzardBetweenBallAndTarget(hazard, tolerance, hole, target);
                if (hazardBetweenBallAndPin != null)
                {
                    // If a hazard is found, we need to adjust the shot
                    //Check for hazards between ball and pin
                    //TODO: Risk Determination Logic
                }
            }

            //Get Target for shot
            // if distance to the ping is less then clubName Driver distance + tolerance, we can go for the pin
            var driverDistance = bagDistance.bag.FirstOrDefault(c => c.ClubName == "Driver")?.ClubDistance ?? 0;
            if (distanceToPin <= driverDistance + tolerance)
            {
                //TODO: Add Nuance to the green distance desired by players based on their attributes in a later code update
                // For simplicity, we will set the target to the pin position
                target = green.pin;
            }
            else
            {
                //TODO: Add Nuance to the fairway distance diesired by players based on their attributes in a later code update
                target = GetLayupTarget(fairway, bagDistance, currentBall, tolerance);
                // If the pin is not reachable, we can choose a club based on the fairway distance
            }
            return target;
        }
        Locations IsHarzardBetweenBallAndTarget(Locations hazard, int tolerance, Hole hole, Vector2 target)
        {
            var dispersioncircleRadius = tolerance; // The radius of the dispersion circle in yards

            var distanceToHazard = 0;
            if (hazard.startPosition.X - target.X >= hazard.endPosition.X - target.X)
            {
                // Check if the hazard is within the tolerance circle
                var dx = hazard.startPosition.X - target.X;
                var dy = hazard.startPosition.Y - target.Y;
                distanceToHazard = Convert.ToInt16(Math.Sqrt(dx * dx + dy * dy));
            }
            else
            {
                // Check if the hazard is within the tolerance circle
                var dx = hazard.endPosition.X - target.X;
                var dy = hazard.endPosition.Y - target.Y;
                distanceToHazard = Convert.ToInt16(Math.Sqrt(dx * dx + dy * dy));
            }
            if (distanceToHazard <= dispersioncircleRadius)
            {
                return hazard; // Hazard is between the ball and the pin
            }
            // Check if the hazard is between the ball and the pin
            return null; // No hazard between the ball and the pin
        }
        int FindDistanceToTargetInYards (Vector2 target, Vector2 currentBall)
        {
            var dx = target.X - currentBall.X;
            var dy = target.Y - currentBall.Y;
            var distanceFromBallToTarget = Convert.ToInt32(Math.Sqrt(dx * dx + dy * dy));

            return distanceFromBallToTarget;
        }
    }
}
