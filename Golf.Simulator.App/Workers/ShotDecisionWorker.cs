using Golf.Simulator.App.Models;
using System.Numerics;

namespace Golf.Simulator.App.Workers
{
    public  class ShotDecisionWorker
    {
        public Shot Decision(Player player, Course course, BagDistance bagDistance, CurrentBallPosition currentBall)//Wind wind, Lie lie, int altitude, int temp, Rain rain)
        {
            int idealTolerance = 20; // This can be adjusted based on player skill or conditions
            int riskyTolerance = 5; // This can be adjusted based on player skill or conditions
            int conservitiveTolerance = 40; // This can be adjusted based on player skill or conditions
            Shot idealShot = new Shot();
            Shot riskyShot = new Shot();
            Shot conservitiveShot = new Shot();


            switch (currentBall.lie)
            {
                case "Tee":
                    {
                        idealShot = GetTeeShot(course, player, bagDistance, currentBall, idealTolerance); //wind, lie, altitude, temp, rain, );
                        riskyShot = GetTeeShot(course, player, bagDistance, currentBall, riskyTolerance); //wind, lie, altitude, temp, rain, );
                        conservitiveShot = GetTeeShot(course, player, bagDistance, currentBall, conservitiveTolerance); //wind, lie, altitude, temp, rain, );
                        break;
                    }
                case "Fairway":
                    {
                        idealShot = GetApproachShot(course, player, bagDistance, currentBall, idealTolerance); //wind, lie, altitude, temp, rain, );
                        riskyShot = GetApproachShot(course, player, bagDistance, currentBall, riskyTolerance); //wind, lie, altitude, temp, rain, );
                        conservitiveShot = GetApproachShot(course, player, bagDistance, currentBall, conservitiveTolerance); //wind, lie, altitude, temp, rain, );
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Invalid lie type");
                    }
            }
            //Decide on a shot based on players mental state and attributes
            // Mental state is a value between 0 and 100, where 0 is very aggressive and 100 is very calm multiplied by 10 to get a value between 0 and 100
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
        Shot GetTeeShot(Course course, Player player, BagDistance bagDistance, CurrentBallPosition currentBall, int tolerance) // Wind wind, Lie lie, int altitude, int temp, Rain rain,)
        {
            // Logic to determine position A based on course, player, wind, lie, altitude, temp, and rain
            Shot shot = new Shot();
            var holeLayout = course.holes[currentBall.holeNumber].holeLayout;
            //Select first fairway object
            var fairway = holeLayout.fairway.FirstOrDefault();
            var sand = holeLayout.sand.FirstOrDefault();
            var water = holeLayout.water.FirstOrDefault();
            var deepRough = holeLayout.deepRough.FirstOrDefault();
            var isDriverIdeal = true;
            var isThreeWoodIdeal = true;
            var isFiveWoodIdeal = true;
            var isHybridIdeal = true;
            var isIronIdeal = true;
            // Tolerance misses TODO Convert to a Tolerance Model where tolerance is calculated based on player attributes
            tolerance = tolerance - player.attributes.mental.courseManagement;
            // Example logic (to be replaced with actual logic)
            //if sand exists or water exists

            if (holeLayout.sand.Any() || holeLayout.water.Any() || holeLayout.deepRough.Any())
            {
                if (fairway.endPosition.Y - bagDistance.Driver <= tolerance)
                {
                    isDriverIdeal = false;
                }
                if (fairway.endPosition.Y - bagDistance.ThreeWood <= tolerance)
                {
                    isThreeWoodIdeal = false;
                }
                if (fairway.endPosition.Y - bagDistance.FiveWood <= tolerance)
                {
                    isFiveWoodIdeal = false;
                }
                if (fairway.endPosition.Y - bagDistance.Hybrid >= tolerance || fairway.endPosition.Y - bagDistance.Hybrid <= tolerance)
                {
                    isHybridIdeal = false;
                }
                if (fairway.endPosition.Y - bagDistance.DrivingIron >= tolerance || fairway.endPosition.Y - bagDistance.DrivingIron <= tolerance)
                {
                    isIronIdeal = false;
                }
                foreach (var bunkers in holeLayout.sand)
                {
                    if (bagDistance.Driver - bunkers.startPosition.Y <= tolerance || bagDistance.Driver - bunkers.endPosition.Y <= tolerance)
                    {
                        isDriverIdeal = false;
                    }
                    if (bagDistance.ThreeWood - bunkers.startPosition.Y <= tolerance || bagDistance.ThreeWood - bunkers.endPosition.Y <= tolerance)
                    {
                        isThreeWoodIdeal = false;
                    }
                    if (bagDistance.FiveWood - bunkers.startPosition.Y <= tolerance || bagDistance.FiveWood - bunkers.endPosition.Y <= tolerance)
                    {
                        isFiveWoodIdeal = false;
                    }
                    if (bagDistance.Hybrid - bunkers.startPosition.Y <= tolerance || bagDistance.Hybrid - bunkers.endPosition.Y <= tolerance)
                    {
                        isHybridIdeal = false;
                    }
                    if (bagDistance.DrivingIron - bunkers.startPosition.Y <= tolerance || bagDistance.DrivingIron - bunkers.endPosition.Y <= tolerance)
                    {
                        isIronIdeal = false;
                    }
                }
                foreach (var waterHazard in holeLayout.water)
                {
                    if (bagDistance.Driver - waterHazard.startPosition.Y <= tolerance || bagDistance.Driver - waterHazard.endPosition.Y <= tolerance)
                    {
                        isDriverIdeal = false;
                    }
                    if (bagDistance.ThreeWood - waterHazard.startPosition.Y <= tolerance || bagDistance.ThreeWood - waterHazard.endPosition.Y <= tolerance)
                    {
                        isThreeWoodIdeal = false;
                    }
                    if (bagDistance.FiveWood - waterHazard.startPosition.Y <= tolerance || bagDistance.FiveWood - waterHazard.endPosition.Y <= tolerance)
                    {
                        isFiveWoodIdeal = false;
                    }
                    if (bagDistance.Hybrid - waterHazard.startPosition.Y <= tolerance || bagDistance.Hybrid - waterHazard.endPosition.Y <= tolerance)
                    {
                        isHybridIdeal = false;
                    }
                    if (bagDistance.DrivingIron - waterHazard.startPosition.Y <= tolerance || bagDistance.DrivingIron - waterHazard.endPosition.Y <= tolerance)
                    {
                        isIronIdeal = false;
                    }
                }
            }
            if (isDriverIdeal)
            {
                shot.ClubChoice = "Driver";
                //Middle of fairway
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.Driver);
                shot.BallPosition = ballPosition;
                return shot;
            }
            if (isThreeWoodIdeal)
            {
                shot.ClubChoice = "Three Wood";
                //Middle of fairway
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.ThreeWood);
                shot.BallPosition = ballPosition;
                return shot;
            }
            if (isFiveWoodIdeal)
            {
                shot.ClubChoice = "Five Wood";
                //Middle of fairway
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.FiveWood);
                shot.BallPosition = ballPosition;
                return shot;
            }
            if (isHybridIdeal)
            {
                shot.ClubChoice = "Hybrid";
                //Middle of fairway
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.Hybrid);
                shot.BallPosition = ballPosition;
                return shot;
            }
            if (isIronIdeal)
            {
                shot.ClubChoice = "Driving Iron";
                //Middle of fairway
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.DrivingIron);
                shot.BallPosition = ballPosition;
                return shot;
            }
            return null;
        }
        Shot GetApproachShot(Course course, Player player, BagDistance bagDistance, CurrentBallPosition currentBall, int tolerance) // Wind wind, Lie lie, int altitude, int temp, Rain rain,)
        {
            Shot shot = new Shot();
            var hole = course.holes[currentBall.holeNumber];
            var holeLayout = course.holes[currentBall.holeNumber].holeLayout;
            //Select first fairway object
            var fairway = holeLayout.fairway.FirstOrDefault();
            var sand = holeLayout.sand.FirstOrDefault();
            var water = holeLayout.water.FirstOrDefault();
            var deepRough = holeLayout.deepRough.FirstOrDefault();
            var green = course.holes[currentBall.holeNumber].green;

            var distanceToPin = FindDistanceToPinPositionInYards(green, hole, currentBall);
            var isPinReachable = false;
            var target = new Vector2();

            //Get Target for shot
            if (distanceToPin <= bagDistance.Driver + tolerance)
            {
                isPinReachable = true;
                // TODO: Determine target
                // For simplicity, we will set the target to the pin position
                target = GetPinPosition(green, hole);
            }
            else
            {
                //TODO: Add Nuance to the fairway distance diesired by players based on their attributes in a later code update
                shot = GetLayupShot(fairway, bagDistance, tolerance);
                return shot; // If the pin is not reachable, we can choose a club based on the fairway distance
            }
            // Check each type of hazard and all hazards within the hole layout
            foreach (var hazard in holeLayout.sand)
            {
                // Check if the hazard is between the ball and the pin
                var hazardBetweenBallAndPin = IsHarzardBetweenBallAndTarget(hazard, tolerance, hole, target);
                if (hazardBetweenBallAndPin != null)
                {
                    // If a hazard is found, we need to adjust the shot
                    //TODO: Risk Determination Logic

                    // For simplicity, we will just skip this shot and return null
                    return null;
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

                    // For simplicity, we will just skip this shot and return null
                    return null;
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

                    // For simplicity, we will just skip this shot and return null
                    return null;
                }
            }

            var club = ChooseClub(distanceToPin, bagDistance, tolerance);
            shot.ClubChoice = club;
            shot.BallPosition = new Vector2(target.X, target.Y);
            return shot;
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
        string ChooseClub(int distanceToPin, BagDistance bagDistance, int tolerance)
        {
            if (distanceToPin <= bagDistance.Driver + tolerance)
            {
                return "Driver";
            }
            else if (distanceToPin <= bagDistance.ThreeWood + tolerance)
            {
                return "Three Wood";
            }
            else if (distanceToPin <= bagDistance.FiveWood + tolerance)
            {
                return "Five Wood";
            }
            else if (distanceToPin <= bagDistance.Hybrid + tolerance)
            {
                return "Hybrid";
            }
            else if (distanceToPin <= bagDistance.DrivingIron + tolerance)
            {
                return "Driving Iron";
            }
            else if (distanceToPin <= bagDistance.FourIron + tolerance)
            {
                return "Four Iron";
            }
            else if (distanceToPin <= bagDistance.FiveIron + tolerance)
            {
                return "Five Iron";
            }
            else if (distanceToPin <= bagDistance.SixIron + tolerance)
            {
                return "Six Iron";
            }
            else if (distanceToPin <= bagDistance.SevenIron + tolerance)
            {
                return "Seven Iron";
            }
            else if (distanceToPin <= bagDistance.EightIron + tolerance)
            {
                return "Eight Iron";
            }
            else if (distanceToPin <= bagDistance.NineIron + tolerance)
            {
                return "Nine Iron";
            }
            else if (distanceToPin <= bagDistance.PitchingWedge + tolerance)
            {
                return "Pitching Wedge";
            }
            else if (distanceToPin <= bagDistance.GapWedge + tolerance)
            {
                return "Gap Wedge";
            }
            else if (distanceToPin <= bagDistance.SandWedge + tolerance)
            {
                return "Sand Wedge";
            }
            else if (distanceToPin <= bagDistance.LobWedge + tolerance)
            {
                return "Lob Wedge";
            }
            else
            {
                return null; // Default club if none of the above conditions are met
            }
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

        Shot GetLayupShot(Locations fairway, BagDistance bagDistance, int tolerance)
        {
            Shot shot = new Shot();
            // If the pin is not reachable, we can choose a club based on the fairway distance
            if (fairway.endPosition.Y - bagDistance.Driver <= tolerance)
            {
                shot.ClubChoice = "Driver";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.Driver);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.ThreeWood <= tolerance)
            {
                shot.ClubChoice = "Three Wood";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.ThreeWood);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.FiveWood <= tolerance)
            {
                shot.ClubChoice = "Five Wood";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.FiveWood);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.Hybrid >= tolerance || fairway.endPosition.Y - bagDistance.Hybrid <= tolerance)
            {
                shot.ClubChoice = "Hybrid";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.Hybrid);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.DrivingIron >= tolerance || fairway.endPosition.Y - bagDistance.DrivingIron <= tolerance)
            {
                shot.ClubChoice = "Driving Iron";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.DrivingIron);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.FourIron >= tolerance || fairway.endPosition.Y - bagDistance.FourIron <= tolerance)
            {
                shot.ClubChoice = "Four Iron";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.FourIron);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.FiveIron >= tolerance || fairway.endPosition.Y - bagDistance.FiveIron <= tolerance)
            {
                shot.ClubChoice = "Five Iron";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.FiveIron);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.SixIron >= tolerance || fairway.endPosition.Y - bagDistance.SixIron <= tolerance)
            {
                shot.ClubChoice = "Six Iron";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.SixIron);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.SevenIron >= tolerance || fairway.endPosition.Y - bagDistance.SevenIron <= tolerance)
            {
                shot.ClubChoice = "Seven Iron";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.SevenIron);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.EightIron >= tolerance || fairway.endPosition.Y - bagDistance.EightIron <= tolerance)
            {
                shot.ClubChoice = "Eight Iron";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.EightIron);
                shot.BallPosition = ballPosition;
            }
            else if (fairway.endPosition.Y - bagDistance.NineIron >= tolerance || fairway.endPosition.Y - bagDistance.NineIron <= tolerance)
            {
                shot.ClubChoice = "Nine Iron";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.NineIron);
                shot.BallPosition = ballPosition;
            }
            else if ((fairway.endPosition.Y - bagDistance.PitchingWedge >= tolerance || fairway.endPosition.Y - bagDistance.PitchingWedge <= tolerance))
            {
                shot.ClubChoice = "Pitching Wedge";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.PitchingWedge);
                shot.BallPosition = ballPosition;
            }
            else if ((fairway.endPosition.Y - bagDistance.GapWedge >= tolerance || fairway.endPosition.Y - bagDistance.GapWedge <= tolerance))
            {
                shot.ClubChoice = "Gap Wedge";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.GapWedge);
                shot.BallPosition = ballPosition;
            }
            else if ((fairway.endPosition.Y - bagDistance.SandWedge >= tolerance || fairway.endPosition.Y - bagDistance.SandWedge <= tolerance))
            {
                shot.ClubChoice = "Sand Wedge";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.SandWedge);
                shot.BallPosition = ballPosition;
            }
            else if ((fairway.endPosition.Y - bagDistance.LobWedge >= tolerance || fairway.endPosition.Y - bagDistance.LobWedge <= tolerance))
            {
                shot.ClubChoice = "Lob Wedge";
                var ballPosition = new Vector2(fairway.startPosition.X + (fairway.width / 2), bagDistance.LobWedge);
                shot.BallPosition = ballPosition;
            }
            else
            {
                throw new Exception("No suitable club found for the distance to the pin.");
            }
            return shot;
        }

        Locations IsHarzardBetweenBallAndTarget(Locations hazard, int tolerance, Hole hole, Vector2 target)
        {
            //Create a tolerance circle around the 15 yards left and right and 15 yards forward and back of the target line
            var dispersioncircleRadius = 15; // 15 yards tolerance circle
                                             //Get Pin yards from Center of Green
                                             // Position from the left of the green
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
    }
}
