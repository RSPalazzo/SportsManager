using System;

namespace SportsManager
{
    class ShotResult
     {
        public int getShotResultsGrade(int shotPercentage, int perCent, bool shot)
        {
            int ShotGrade;
            int preAbsGrade = shotPercentage - perCent;
            int postAbsGrade = Math.Abs(preAbsGrade);
            // TODO: Deal with 5< shot percentages
            if (shot == true)
            {
                if (postAbsGrade <= 5)
                {
                    ShotGrade = 1;                
                }
                else if (postAbsGrade > 5 && postAbsGrade <= 30)
                {
                    ShotGrade = 2;
                }
                else
                {
                    ShotGrade = 3;    
                }
            }
            else
            {
                if (postAbsGrade <= 5)
                {
                    ShotGrade = 4;                
                }
                else if (postAbsGrade > 5 && postAbsGrade <= 30)
                {
                    ShotGrade = 5;
                }
                else if (postAbsGrade > 30 && postAbsGrade <= 60)
                {
                    ShotGrade = 6;
                }
                else
                {
                    ShotGrade = 7;    
                }
            }
            return ShotGrade;
        }
        public int getShotGradeYards (int shotG)
        {
            int yards;
            switch (shotG)
            {
                case 1:
                    yards = 10;
                    break;
                case 2:
                    yards = 5;
                    break;
                case 3:
                    yards = 0;
                    break;
                case 4:
                    yards = -10;
                    break;
                case 5:
                    yards = -20;
                    break;
                case 6:
                    yards = -40;
                    break;
                case 7:
                    yards = -100;
                    break;
                default:
                    yards = 0;
                    break;
            }
            return yards;
        }
        public int getShotResultsYards (int strength, int flexibility, int balance, int agility, int tempo, 
                                        int swing, int ballStriking, int fit, int quality,
                                        int demeanor, int condition, int clubChoice, int shotTraj,
                                        int grass, int rain, int altitude, int temp, int shotGrade, int baseDistance)
        {
             //Distance
            int totalStats = (strength + flexibility + balance + agility + tempo + swing + ballStriking + fit + quality + demeanor + condition + grass + rain + altitude + temp);
            int shotGradeYards = getShotGradeYards(shotGrade);
            int distance = baseDistance + totalStats + shotGradeYards;
            return distance;
        }
        public int getDistanceToHole (int currentDistance, int shotDistance)
        {
            int distanceToHole = currentDistance - shotDistance;
            return Math.Abs(distanceToHole);
        }
    }
}

