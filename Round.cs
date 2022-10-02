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
        public GolfRound (int courseId, int playerId)
        {
            golfCourse = new GolfCourse(courseId);
            play = new Player(playerId);
            holeScore = 0;
            roundScore = 0;
            holeNumber = 1;
            ballIsHoled = false;
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
                    ShotDeterminer shot = new ShotDeterminer(holeNumber, holeScore, distanceToHole, play);
                    ShotSimulator sim = new ShotSimulator();
                    ShotResult result = new ShotResult();
                    Console.WriteLine ("---------------------------------------");
                    Console.WriteLine ("New Shot");
                    //Determinate Shot Difficulty and Player Skill Rating
                    Console.WriteLine("shotDifficulty: " + shot.shotDifficulty + " playerSkill: " + play.playerOverallSkill);
                    Console.WriteLine("Club choice is: " + shot.clubName + " Shot Type was: " + shot.shotTypeName);
                    Console.WriteLine("Lie was: " + shot.lieName + " Location was: " + shot.locationName);
                    //Simulate the shot
                    shotSim = sim.ShotGenerator(shot.shotDifficulty, play.playerOverallSkill);
                    Console.WriteLine("Shot Percent was: " + sim.shotPercentage + " Random was: "+ sim.perCent);
                    Console.WriteLine("Shot was: " + shotSim);
                    //Shot Grading
                    shotPercent = Convert.ToInt32(sim.shotPercentage);
                    shotGrade = result.getShotResultsGrade(shotPercent, sim.perCent, shotSim);
                    baseShot = shot.getClubBaseDistance(shot.club);
                    Console.WriteLine("Shot Grade was: " + shotGrade);
                    //Shot Distance
                    distance = result.getShotResultsYards(play, shot.club, shot.shotTraj, 
                                                            shot.grass, shot.rain, shot.altitude, shot.temp, shotGrade, baseShot, shot.shotType, distanceToHole);
                    distanceToHole = result.getDistanceToHole(distanceToHole, distance);
                    Console.WriteLine("Ball went: " + distance + " Distance Left is: " + distanceToHole);
                    Console.WriteLine ("End Shot");
                    Console.WriteLine ("---------------------------------------");
                    //Thread.Sleep(10000);
                    //Add Shot to score and check if your on green
                    holeScore++;
                    ballIsHoled = generateIsBallInHole(distanceToHole);
                    
                }
                //If Ball is in the hole go to next hole
                else if (isRoundOver == false && ballIsHoled == true)
                {
                    roundScore = roundScore + holeScore;
                    holeScore = 0;
                    holeNumber++;
                    distanceToHole = golfCourse.course.holes[holeNumber-1].holeYardage;
                    distance = 0;
                    ballIsHoled = false;
                }
                //If round is over log final score
                else
                {
                    roundScore = roundScore + holeScore;
                }
            }
        }
        bool generateIsBallInHole(int distanceToHole)
        {
            if (distanceToHole <= 8)
            {
                holeScore = holeScore + 1;
                Console.WriteLine("Hole Score is: " + holeScore);
                return true;
            }
            else if (distanceToHole <= 30)
            {
                holeScore = holeScore + 2;
                Console.WriteLine("Hole Score is: " + holeScore);
                return true;
            }
            else
            {
                return false;
            }
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