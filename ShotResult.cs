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
        int  getShotGradeAccuracy(int shotGr)
        {
            int yards;
            Random rand = new Random();
            switch (shotGr)
            {
                case 1:
                    yards = rand.Next (0, 3);
                    break;
                case 2:
                    yards = rand.Next (4, 6);
                    break;
                case 3:
                    yards = rand.Next (7, 10);
                    break;
                case 4:
                    yards = rand.Next (11, 20);
                    break;
                case 5:
                    yards = rand.Next (21, 30);
                    break;
                case 6:
                    yards = rand.Next (31, 50);
                    break;
                case 7:
                    yards = rand.Next (50, 100);
                    break;
                default:
                    yards = 0;
                    break;
            }
            return yards;
        }
        public int getShotResultsYards (Player play, int shotTraj, int grass, int rain, int altitude, int temp, int shotGrade, 
                                            int baseDistance, string shotType, int distanceToHole)
        {
            //TODO: Better use of Mechanics and Mentals and Equipment in shots IMPROVE THIS SYSTEM EVENTUALLY
            if (shotType == "Full Swing")
            {
                int totalStats = (play.player.attributes.physical.strength + play.player.attributes.physical.flexibility + play.player.attributes.physical.balance 
                                    + play.player.attributes.physical.agility + play.player.attributes.mechanics.tempo + play.player.attributes.mechanics.swing 
                                    + play.player.attributes.mechanics.ballStriking + play.player.attributes.equipment.fit + play.player.attributes.equipment.quality 
                                    + play.player.attributes.mental.demeanor + play.player.attributes.playerCondition + grass + rain + altitude + temp);
                int shotGradeYards = getShotGradeYards(shotGrade);
                int distance = baseDistance + totalStats + shotGradeYards;
                return distance;
            }
            else
            {
                int distance = getShotGradeAccuracy(shotGrade);
                distance = distanceToHole - distance;
                return distance;
            }

        }
        public int getDistanceToHole (int currentDistance, int shotDistance)
        {
            int distanceToHole = currentDistance - shotDistance;
            return Math.Abs(distanceToHole);
        }
    }
}