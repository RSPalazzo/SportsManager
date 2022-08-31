using System;
using System.Threading;

namespace SportsManager
{
    class GolfRound
     {
        GolfCourse course = new GolfCourse();
        Player play = new Player();

        int holeNumber = 1;
        bool ballIsHoled = false;
        bool roundActive = false;
        bool isRoundOver = false;
        int courseYardage;
        int coursePar;
        int holeYardage;
        int par;
        int distanceToHole;
        int roundScore;
        int holeScore;
        

        public int startRound ()
        {
            coursePar = course.getCoursePar();
            courseYardage = course.getCourseYardage();
            holeScore = 0;
            roundScore = 0;
            holeNumber = 1;
            ballIsHoled = false;
            holeYardage = course.getHoleYardage(holeNumber);
            par = course.getPar(holeNumber);
            playRound();
            return roundScore;
        }
        public void playRound()
        {

            bool shotSim = false;
            int playerSkill = 0;
            int shotDifficulty = 0;
            int distance = 0;
            int distanceToHole = holeYardage;
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
                    ShotDeterminer shot = new ShotDeterminer();
                    ShotSimulator sim = new ShotSimulator();
                    ShotResult result = new ShotResult();
                    Console.WriteLine ("---------------------------------------");
                    Console.WriteLine ("New Shot");
                    //Determinate Shot Difficulty and Player Skill Rating
                    shotDifficulty = shot.getShotDifficulty(holeNumber, par, holeScore, distanceToHole);
                    playerSkill = play.getPlayerSkill();
                    Console.WriteLine("shotDifficulty: " + shotDifficulty + " playerSkill: " + playerSkill);
                    
                    //Simulate the shot
                    shotSim = sim.ShotGenerator(shotDifficulty, playerSkill);
                    Console.WriteLine("Shot Percent was: " + sim.shotPercentage + " Random was: "+ sim.perCent);
                    Console.WriteLine("Shot was: " + shotSim);
                    
                    //Shot Grading
                    shotPercent = Convert.ToInt32(sim.shotPercentage);
                    shotGrade = result.getShotResultsGrade(shotPercent, sim.perCent, shotSim);
                    baseShot = shot.getClubBaseDistance(shot.club);
                    Console.WriteLine("Shot Grade was: " + shotGrade + " Club choice is: "+ shot.clubName);
                    //Shot Distance
                    distance = result.getShotResultsYards(play.strength, play.flexibility, play.balance, play.agility, play.tempo, play.swing, 
                                                            play.ballStriking, play.fit, play.quality, play.demeanor, play.condition ,shot.club, shot.shotTraj, 
                                                            shot.grass, shot.rain, shot.altitude, shot.temp, shotGrade, baseShot);
                    distanceToHole = result.getDistanceToHole(distanceToHole, distance);
                    Console.WriteLine("Ball went: " + distance + " Distance Left is: " + distanceToHole);
                    Console.WriteLine ("End Shot");
                    Console.WriteLine ("---------------------------------------");
                    Thread.Sleep(5000);
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
                    holeYardage = course.getHoleYardage(holeNumber);
                    par = course.getPar(holeNumber);
                    distanceToHole = holeYardage;
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
        public bool generateIsBallInHole(int distanceToHole)
        {
            if (distanceToHole <= 5)
            {
                holeScore = holeScore + 2;
                Console.WriteLine("Hole Score is: " + holeScore);
                Thread.Sleep(5000);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool getIsRoundOver()
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