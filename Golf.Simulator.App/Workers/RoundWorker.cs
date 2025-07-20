using Golf.Simulator.App.Models;
using Golf.Simulator.App.ObjectCreates;
using Golf.Simulator.App.GolfCalcuation;
using Golf.Simulator.App.ObjectLoads;
using Golf.Simulator.App.Interfaces;

namespace Golf.Simulator.App.Workers
{
    public class RoundWorker : IRoundWorker
    {
        private GolfRoundCreate _golfRound;
        private readonly PlayerLoad _player;
        private readonly CourseLoad _golfCourse;
        private readonly PlayerOverallSkill _overallSkill;
        //Result resultJson = new Result();
        public RoundWorker(GolfRoundCreate golfRound, PlayerLoad player, CourseLoad golfCourse, PlayerOverallSkill playerOverallSkill)
        {
            _golfRound = golfRound;
            _player = player;
            _golfCourse = golfCourse;
            _overallSkill = playerOverallSkill;
        }
        public GolfRound PlayGolfRound(int courseId, int playerId, int roundId)
        {
            var _round = _golfRound.CreateGolfRound(courseId, playerId, roundId);
            //resultJson.SetResult(play.player.playerFullName, roundTotal);              
            var round = PlayRound(_round);
            return round;
        }
        GolfRound PlayRound(GolfRound golfRound)
        {
            var player = _player.GetPlayer(golfRound.PlayerId);
            var course = _golfCourse.GetGolfCourse(golfRound.CourseId);
            var skill = _overallSkill.getPlayerSkill(player);
            int puttsPerRound = 0;
            bool ballIsHoled = false;
            bool isRoundOver = false;
            int roundScore = 0;
            int holeNumber = 1;
            int distanceToHole;
            int holeScore = 0;
            bool shotSim = false;
            int distance = 0;
            distanceToHole = course.holes[holeNumber-1].holeYardage;
            int shotGrade = 0;
            //int shotTraj = 0;
            int baseShot = 0;
            int shotPercent = 0;
            isRoundOver = false;
            //resultJson.SetHole(holeNumber, golfCourse.course.holes[holeNumber-1].holeYardage, golfCourse.course.holes[holeNumber-1].holePar); 
            while (isRoundOver == false)
            {
                isRoundOver = getIsRoundOver(holeNumber, ballIsHoled);

                if (isRoundOver == false && ballIsHoled == false)
                {
                    ShotDeterminer shot = new ShotDeterminer(holeNumber-1, holeScore, distanceToHole, player, course, shotGrade);
                    ShotSimulator sim = new ShotSimulator();
                    ShotResult result = new ShotResult();
                    Console.WriteLine ("---------------------------------------");
                    Console.WriteLine (player.playerFullName + " steps up to his " + (holeScore+1) + " shot on hole number " + holeNumber);
                    Console.WriteLine ("This is a " + course.holes[holeNumber-1].holeYardage + " yard Par: " + course.holes[holeNumber-1].holePar);
                    Console.WriteLine("Ball is " + shot.lie + " in the " + shot.location);
                    //Thread.Sleep(5000);
                    //Determinate Shot Difficulty and Player Skill Rating
                    Console.WriteLine("The Player pulls " + shot.club + " and is attempting a " + shot.shotType + " shot");
                    //Thread.Sleep(5000);
                    Console.WriteLine("shotDifficulty: " + shot.shotDifficulty + " playerSkill: " + skill);
                    //Simulate the shot
                    shotSim = sim.ShotGenerator(shot.shotDifficulty, skill);
                    Console.WriteLine("The Percent to hit the shot was " + sim.shotPercentage);
                    //Thread.Sleep(5000);
                    Console.WriteLine("Was the shot good: " + shotSim  + " The random number generated was "+ sim.perCent);
                    //Shot Grading
                    shotPercent = Convert.ToInt32(sim.shotPercentage);
                    shotGrade = result.getShotResultsGrade(shotPercent, sim.perCent, shotSim);
                    baseShot = shot.getClubBaseDistance(shot.club);
                    Console.WriteLine("Shot Grade was: " + shotGrade);
                    //Thread.Sleep(5000);
                    //Shot Distance
                    distance = result.getShotResultsYards(player, shot.shotTraj, 
                                                            shot.grass, shot.rain, shot.altitude, shot.temp, shotGrade, baseShot, shot.shotType, distanceToHole);
                    distanceToHole = result.getDistanceToHole(distanceToHole, distance);
                    if (distance <= 0){
                        Console.WriteLine("Ball went: " + Math.Abs(distanceToHole - distance) + " yards past the hole Distance Left is: " + distanceToHole + " yards");
                    }
                    else{
                        Console.WriteLine("Ball went: " + distance + " yards Distance Left is: " + distanceToHole + " yards");    
                    }
                    Console.WriteLine("Ball went: " + distance + " yards Distance Left is: " + distanceToHole + " yards");
                    //Thread.Sleep(5000);
                    Console.WriteLine ("End Shot");
                    Console.WriteLine ("---------------------------------------");
                    //Thread.Sleep(5000);
                    //Add Shot to score and check if your on green
                    holeScore++;
                    //resultJson.SetShot(holeNumber, holeScore, shot.club, shot.shotType, shot.lie, shot.location, shot.shotDifficulty, 
                    //        skill, sim.perCent, shotSim, Convert.ToInt32(sim.shotPercentage), shotGrade, distance, distanceToHole);
                    
                    if (distanceToHole <= course.holes[holeNumber-1].green.size)
                    {
                        ballIsHoled = generateIsBallInHole(golfRound, course, player, holeNumber,distanceToHole, holeScore, puttsPerRound);
                    } 
                }
                //If Ball is in the hole go to next hole
                else if (isRoundOver == false && ballIsHoled == true)
                {
                    roundScore = roundScore + holeScore;
                    //resultJson.SetHoleScore(holeNumber, holeScore);
                    holeScore = 0;
                    holeNumber++;
                    //resultJson.SetHole(holeNumber, course.holes[holeNumber-1].holeYardage, course.holes[holeNumber-1].holePar);                    
                    distanceToHole = course.holes[holeNumber-1].holeYardage;
                    distance = 0;
                    ballIsHoled = false;
                }
                //If round is over log final score
                else
                {
                    roundScore = roundScore + holeScore;
                    golfRound.PlayerScore = roundScore;
                    Console.WriteLine ("---------------------------------------");
                    Console.WriteLine("Round Score: " + roundScore);
                    Console.WriteLine("Putts Per Round " + puttsPerRound);
                    Console.WriteLine ("---------------------------------------");
                    //Thread.Sleep(5000);
                    //resultJson.SetRoundScore(roundScore);
                    //resultJson.PrintResult(golfRound.RoundId);
                    
                }
            }
            // Set the final round status
            golfRound.RoundStatus = "Completed";
            return golfRound;
        }
        bool generateIsBallInHole(GolfRound round, Course golfCourse, Player player, int holeNumber,int distanceToHole, int holeScore, int puttsPerRound)
        {
            //Convert to feet
            distanceToHole = distanceToHole * 3;
            while (distanceToHole > 0)
            {
                PuttDeterminer putt = new PuttDeterminer();
                int puttDifficulty = putt.getPuttDifficulty(golfCourse, holeNumber-1, distanceToHole);
                int playerPuttingSkill = putt.getPlayerPuttingSkill(player);
                ShotSimulator sim = new ShotSimulator();
                bool shotSim = sim.ShotGenerator(puttDifficulty, playerPuttingSkill);
                int shotPercent = Convert.ToInt32(sim.shotPercentage);
                int preDistance = distanceToHole;
                Console.WriteLine("Putt is " + distanceToHole + " feet");
                distanceToHole = putt.getPuttGrade(shotPercent, sim.perCent, shotSim, distanceToHole);
                Console.WriteLine("The Percent to hit the shot was " + sim.shotPercentage);
                Console.WriteLine("Was the shot good: " + shotSim  + " The random number generated was "+ sim.perCent);
                Console.WriteLine("Putt Difficulty " + puttDifficulty + ", Player Skill " + playerPuttingSkill + ", Putt Grade " + putt.puttGrade);
                Console.WriteLine("Distance left " + distanceToHole + "feet");
                //Thread.Sleep(5000);
                holeScore = holeScore + 1;
                //resultJson.SetPutt(holeNumber, holeScore, preDistance, putt.puttGrade, sim.perCent, puttDifficulty, playerPuttingSkill, shotSim, Convert.ToInt32(sim.shotPercentage), distanceToHole);
                puttsPerRound++;
            }
            // Add hole score to round list
            round.HoleScores.Add(holeScore);
            Console.WriteLine ("---------------------------------------");
            Console.WriteLine("Hole Score is " + holeScore);
            Console.WriteLine ("---------------------------------------");
            return true;
        }
        bool getIsRoundOver(int holeNumber, bool ballIsHoled)
        {
            bool roundOver = false;
            if (holeNumber == 18 && ballIsHoled == true)
            {
                roundOver = true;
            }
            return roundOver;
        }
     }
}