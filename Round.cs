using System;
using System.Threading;

namespace SportsManager
{
    class GolfRound
     {
        public int roundScore;
        public int holeNumber;
        public int distanceToHole;
        public int holeScore;
        Player play;
        GolfCourse golfCourse;
        bool ballIsHoled = false;
        bool isRoundOver = false;
        int puttsPerRound;
        Result resultJson = new Result();
        public GolfRound (int courseId, int playerId)
        {
            golfCourse = new GolfCourse(courseId);
            play = new Player(playerId);
            holeScore = 0;
            roundScore = 0;
            holeNumber = 1;
            ballIsHoled = false;  
            resultJson.SetResult(play.player.playerFullName, 1);              
            playRound();
        }
        void playRound()
        {

            bool shotSim = false;
            int distance = 0;
            distanceToHole = golfCourse.course.holes[holeNumber-1].holeYardage;
            int shotGrade = 0;
            int shotTraj = 0;
            int baseShot = 0;
            int shotPercent = 0;
            isRoundOver = false;
            while (isRoundOver == false)
            {
                isRoundOver = getIsRoundOver();

                if (isRoundOver == false && ballIsHoled == false)
                {
                    ShotDeterminer shot = new ShotDeterminer(holeNumber-1, holeScore, distanceToHole, play, golfCourse, shotGrade);
                    ShotSimulator sim = new ShotSimulator();
                    ShotResult result = new ShotResult();
                    Console.WriteLine ("---------------------------------------");
                    Console.WriteLine (play.player.playerFullName + " steps up to his " + (holeScore+1) + " shot on hole number " + holeNumber);
                    Console.WriteLine ("This is a " + golfCourse.course.holes[holeNumber-1].holeYardage + " yard Par: " + golfCourse.course.holes[holeNumber-1].holePar);
                    Console.WriteLine("Ball is " + shot.lie + " in the " + shot.location);
                    //Thread.Sleep(5000);
                    //Determinate Shot Difficulty and Player Skill Rating
                    Console.WriteLine("The Player pulls " + shot.club + " and is attempting a " + shot.shotType + " shot");
                    //Thread.Sleep(5000);
                    Console.WriteLine("shotDifficulty: " + shot.shotDifficulty + " playerSkill: " + play.playerOverallSkill);
                    //Simulate the shot
                    shotSim = sim.ShotGenerator(shot.shotDifficulty, play.playerOverallSkill);
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
                    distance = result.getShotResultsYards(play, shot.shotTraj, 
                                                            shot.grass, shot.rain, shot.altitude, shot.temp, shotGrade, baseShot, shot.shotType, distanceToHole);
                    distanceToHole = result.getDistanceToHole(distanceToHole, distance);
                    Console.WriteLine("Ball went: " + distance + " yards Distance Left is: " + distanceToHole + " yards");
                    //Thread.Sleep(5000);
                    Console.WriteLine ("End Shot");
                    Console.WriteLine ("---------------------------------------");
                    //Thread.Sleep(5000);
                    //Add Shot to score and check if your on green
                    holeScore++;
                    resultJson.SetShot(holeNumber, holeScore, shot.club, shot.shotType, shot.lie, shot.location, shot.shotDifficulty, 
                            play.playerOverallSkill, sim.perCent, shotSim, Convert.ToInt32(sim.shotPercentage), shotGrade, distance, distanceToHole);
                    
                    if (distanceToHole <= golfCourse.course.holes[holeNumber-1].green.size)
                    {
                        ballIsHoled = generateIsBallInHole(distanceToHole);
                    } 
                }
                //If Ball is in the hole go to next hole
                else if (isRoundOver == false && ballIsHoled == true)
                {
                    roundScore = roundScore + holeScore;
                    resultJson.SetHoleScore(holeNumber, holeScore);
                    holeScore = 0;
                    holeNumber++;
                    resultJson.SetHole(holeNumber, golfCourse.course.holes[holeNumber-1].holeYardage, golfCourse.course.holes[holeNumber-1].holePar);                    
                    distanceToHole = golfCourse.course.holes[holeNumber-1].holeYardage;
                    distance = 0;
                    ballIsHoled = false;
                }
                //If round is over log final score
                else
                {
                    roundScore = roundScore + holeScore;
                    Console.WriteLine ("---------------------------------------");
                    Console.WriteLine("Round Score: " + roundScore);
                    Console.WriteLine("Putts Per Round " + puttsPerRound);
                    Console.WriteLine ("---------------------------------------");
                    Thread.Sleep(5000);
                    resultJson.SetRoundScore(roundScore);
                }
            }
        }
        bool generateIsBallInHole(int distanceToHole)
        {
            //Convert to feet
            distanceToHole = distanceToHole * 3;
            while (distanceToHole > 0)
            {
                PuttDeterminer putt = new PuttDeterminer();
                int puttDifficulty = putt.getPuttDifficulty(golfCourse, holeNumber-1, distanceToHole);
                int playerPuttingSkill = putt.getPlayerPuttingSkill(play);
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
                resultJson.SetPutt(holeNumber, holeScore, preDistance, putt.puttGrade, sim.perCent, puttDifficulty, playerPuttingSkill, shotSim, Convert.ToInt32(sim.shotPercentage), distanceToHole);
                puttsPerRound++;
            }
            Console.WriteLine ("---------------------------------------");
            Console.WriteLine("Hole Score is " + holeScore);
            Console.WriteLine ("---------------------------------------");
            return true;
        }
        bool getIsRoundOver()
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