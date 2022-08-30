using System;

namespace SportsManager
{
    class GolfRound
     {
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
        GolfCourse course = new GolfCourse();

        public bool getIsRoundOver()
        {
            bool roundOver = false;
            if (holeNumber == 18 && ballIsHoled == true)
            {
                roundOver = true;
            }
            return roundOver;
        }
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
            isRoundOver = false;
            while (isRoundOver == false)
            {
                isRoundOver = getIsRoundOver();

                if (isRoundOver == false && ballIsHoled == false)
                {
                    ShotDeterminer shot = new ShotDeterminer();
                    PlayerSkillDeterminer ps = new PlayerSkillDeterminer();
                    ShotSimulator sim = new ShotSimulator();
                    int shotDifficulty = shot.getShotDifficulty(holeNumber, par, holeScore, distanceToHole);
                    int playerSkill = ps.getPlayerSkill();
                    Console.WriteLine("shotDifficulty: " + shotDifficulty + " playerSkill: " + playerSkill);
                    sim.ShotGenerator(shotDifficulty, playerSkill);
                    holeScore++;
                    ballIsHoled = generateIsBallInHole();
                }
                else if (isRoundOver == false && ballIsHoled == true)
                {
                    roundScore = roundScore + holeScore;
                    holeScore = 1;
                    holeNumber++;
                    holeYardage = course.getHoleYardage(holeNumber);
                    par = course.getPar(holeNumber);
                    ballIsHoled = false;
                }
                else
                {
                    roundScore = roundScore + holeScore;
                }
            }
        }
        public bool generateIsBallInHole()
        {
            if (holeScore == 3)
            {
                Random rand = new Random();
                int chance = rand.Next(0, 9);
                if (chance <= 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (holeScore == 4)
            {
                Random rand = new Random();
                int chance = rand.Next(0, 9);
                if (chance <= 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (holeScore == 5)
            {
                Random rand = new Random();
                int chance = rand.Next(0, 9);
                if (chance <= 7)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (holeScore == 6)
            {
                Random rand = new Random();
                int chance = rand.Next(0, 9);
                if (chance <= 8)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (holeScore == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     }
}