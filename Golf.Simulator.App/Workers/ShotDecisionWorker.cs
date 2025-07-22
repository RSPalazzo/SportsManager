using Golf.Simulator.App.Models;
using System.Numerics;

namespace Golf.Simulator.App.Workers
{
    public  class ShotDecisionWorker
    {
        public Shot Decision(Player player, Course course, BagDistance bagDistance, CurrentBallPosition currentBall)//Wind wind, Lie lie, int altitude, int temp, Rain rain)
        {
            int idealTolerance = 20 + player.attributes.mental.courseManagement; // This can be adjusted based on player skill or conditions
            int riskyTolerance = 5 + player.attributes.mental.courseManagement; // This can be adjusted based on player skill or conditions
            int conservitiveTolerance = 40 + player.attributes.mental.courseManagement; // This can be adjusted based on player skill or conditions

            var idealShot = GetShot(course, player, bagDistance, currentBall, idealTolerance); //wind, lie, altitude, temp, rain, );
            var riskyShot = GetShot(course, player, bagDistance, currentBall, riskyTolerance); //wind, lie, altitude, temp, rain, );
            var conservitiveShot = GetShot(course, player, bagDistance, currentBall, conservitiveTolerance); //wind, lie, altitude, temp, rain, );

            //Decide on a shot based on players mental state and attributes
            // Mental state is a value between 0 and 100, where 0 is very aggressive and 100 is very calm multiplied by 10 to get a value between 0 and 100
            return PlayerDecision(idealShot, riskyShot, conservitiveShot, player);
        }
        Shot GetShot(Course course, Player player, BagDistance bagDistance, CurrentBallPosition currentBall, int tolerance) // Wind wind, Lie lie, int altitude, int temp, Rain rain,)
        {
            // TODO: Determine target
            var target = DetermineTarget(course, player, bagDistance, currentBall, tolerance);
            var shot = ChooseClub(target, bagDistance, tolerance);

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
        Shot ChooseClub(Vector2 target, BagDistance bagDistance, int tolerance)
        {
            Shot shot = new Shot();
            // If the pin is not reachable, we can choose a club based on the fairway distance
            if (target.Y - bagDistance.Driver <= tolerance)
            {
                shot.ClubChoice = "Driver";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.ThreeWood <= tolerance)
            {
                shot.ClubChoice = "Three Wood";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.FiveWood <= tolerance)
            {
                shot.ClubChoice = "Five Wood";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.Hybrid >= tolerance || target.Y - bagDistance.Hybrid <= tolerance)
            {
                shot.ClubChoice = "Hybrid";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.DrivingIron >= tolerance || target.Y - bagDistance.DrivingIron <= tolerance)
            {
                shot.ClubChoice = "Driving Iron";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.FourIron >= tolerance || target.Y - bagDistance.FourIron <= tolerance)
            {
                shot.ClubChoice = "Four Iron";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.FiveIron >= tolerance || target.Y - bagDistance.FiveIron <= tolerance)
            {
                shot.ClubChoice = "Five Iron";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.SixIron >= tolerance || target.Y - bagDistance.SixIron <= tolerance)
            {
                shot.ClubChoice = "Six Iron";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.SevenIron >= tolerance || target.Y - bagDistance.SevenIron <= tolerance)
            {
                shot.ClubChoice = "Seven Iron";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.EightIron >= tolerance || target.Y - bagDistance.EightIron <= tolerance)
            {
                shot.ClubChoice = "Eight Iron";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if (target.Y - bagDistance.NineIron >= tolerance || target.Y - bagDistance.NineIron <= tolerance)
            {
                shot.ClubChoice = "Nine Iron";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if ((target.Y - bagDistance.PitchingWedge >= tolerance || target.Y - bagDistance.PitchingWedge <= tolerance))
            {
                shot.ClubChoice = "Pitching Wedge";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if ((target.Y - bagDistance.GapWedge >= tolerance || target.Y - bagDistance.GapWedge <= tolerance))
            {
                shot.ClubChoice = "Gap Wedge";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if ((target.Y - bagDistance.SandWedge >= tolerance || target.Y - bagDistance.SandWedge <= tolerance))
            {
                shot.ClubChoice = "Sand Wedge";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else if ((target.Y - bagDistance.LobWedge >= tolerance || target.Y - bagDistance.LobWedge <= tolerance))
            {
                shot.ClubChoice = "Lob Wedge";
                shot.BallPosition = new Vector2(target.X, target.Y);
            }
            else
            {
                throw new Exception("No suitable club found for the distance to the pin.");
            }
            return shot;
        }
        Vector2 GetPinPosition(Green green, Hole hole)
        {
            // Get Pin yards from Center of Green
            var yardsToCenterFromWidth = green.greenLocation.width / 2; // Center of the green in yards
            var yardsToPinFromLeft = green.pin.feetFromLeft / 3;
            var pinPositionFromLeft = yardsToCenterFromWidth - yardsToPinFromLeft; // Position from the left of the green
            var yardsToPinFromFront = green.pin.feetFromFront / 3; // Position from the front of the green
            var pinPositionFromFront = yardsToCenterFromWidth - yardsToPinFromFront; // Position from the front of the green
            return new Vector2(hole.holeSize.length - (green.greenLocation.endPosition.Y - green.greenLocation.startPosition.Y) - pinPositionFromFront, (hole.holeSize.width - green.greenLocation.width - pinPositionFromLeft));
        }
        Vector2 GetLayupTarget (Locations fairway, BagDistance bagDistance, int tolerance)
        {
            // Get the middle of the fairway
            if (fairway.endPosition.Y - bagDistance.Driver <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.Driver);
            }
            else if (fairway.endPosition.Y - bagDistance.ThreeWood <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.ThreeWood);
            }
            else if (fairway.endPosition.Y - bagDistance.FiveWood <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.FiveWood);
            }
            else if (fairway.endPosition.Y - bagDistance.Hybrid >= tolerance || fairway.endPosition.Y - bagDistance.Hybrid <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.Hybrid);
            }
            else if (fairway.endPosition.Y - bagDistance.DrivingIron >= tolerance || fairway.endPosition.Y - bagDistance.DrivingIron <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.DrivingIron);
            }
            else if (fairway.endPosition.Y - bagDistance.FourIron >= tolerance || fairway.endPosition.Y - bagDistance.FourIron <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.FourIron);
            }
            else if (fairway.endPosition.Y - bagDistance.FiveIron >= tolerance || fairway.endPosition.Y - bagDistance.FiveIron <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.FiveIron);
            }
            else if (fairway.endPosition.Y - bagDistance.SixIron >= tolerance || fairway.endPosition.Y - bagDistance.SixIron <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.SixIron);
            }
            else if (fairway.endPosition.Y - bagDistance.SevenIron >= tolerance || fairway.endPosition.Y - bagDistance.SevenIron <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.SevenIron);
            }
            else if (fairway.endPosition.Y - bagDistance.EightIron >= tolerance || fairway.endPosition.Y - bagDistance.EightIron <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.EightIron);
            }
            else if (fairway.endPosition.Y - bagDistance.NineIron >= tolerance || fairway.endPosition.Y - bagDistance.NineIron <= tolerance)
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.NineIron);
            }
            else if ((fairway.endPosition.Y - bagDistance.PitchingWedge >= tolerance || fairway.endPosition.Y - bagDistance.PitchingWedge <= tolerance))
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.PitchingWedge);
            }
            else if ((fairway.endPosition.Y - bagDistance.GapWedge >= tolerance || fairway.endPosition.Y - bagDistance.GapWedge <= tolerance))
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.GapWedge);
            }
            else if ((fairway.endPosition.Y - bagDistance.SandWedge >= tolerance || fairway.endPosition.Y - bagDistance.SandWedge <= tolerance))
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.SandWedge);
            }
            else if ((fairway.endPosition.Y - bagDistance.LobWedge >= tolerance || fairway.endPosition.Y - bagDistance.LobWedge <= tolerance))
            {
                return new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.LobWedge);
            }
            else
            {
                throw new Exception("No suitable club found for the distance to the pin.");
            }




            return ballPosition;
        }
        Vector2 DetermineTarget(Course course, Player player, BagDistance bagDistance, CurrentBallPosition currentBall, int tolerance)
        {
            // Get the current hole and its layout
            var hole = course.holes[currentBall.holeNumber];
            var holeLayout = course.holes[currentBall.holeNumber].holeLayout;
            var green = course.holes[currentBall.holeNumber].green;

            //Calculate the pin position and distance to pin
            var pinPosition = GetPinPosition(green, hole);
            var distanceToPin = FindDistanceToPinPositionInYards(green, hole, currentBall);
            
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
            if (distanceToPin <= bagDistance.Driver + tolerance)
            {
                //TODO: Add Nuance to the green distance desired by players based on their attributes in a later code update
                // For simplicity, we will set the target to the pin position
                target = GetPinPosition(green, hole);
            }
            else
            {
                //TODO: Add Nuance to the fairway distance diesired by players based on their attributes in a later code update
                target = GetLayupTarget(fairway, bagDistance, tolerance);
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
        int FindDistanceToPinPositionInYards(Green green, Hole hole, CurrentBallPosition currentBall)
        {
            var pinPosition = GetPinPosition(green, hole);
            // Calculate the distance from the player's ball position to the pin position
            // Assume ballPosition.X is distance from front, ballPosition.Y is distance from left
            var dx = pinPosition.X - currentBall.ballPosition.X;
            var dy = pinPosition.Y - currentBall.ballPosition.Y;
            var distanceFromBallToPin = Convert.ToInt32(Math.Sqrt(dx * dx + dy * dy));

            return distanceFromBallToPin;
        }
    }
}
