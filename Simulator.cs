using System;

namespace SportsManager
{
    class ShotSimulator
     {
        
        bool Shot = false;
        public void ShotGenerator (int shotDiff, int playerSk)
        {
            Shot = ShotSimulation(shotDiff, playerSk);
        }
        bool ShotSimulation (double shotDifficulty, double PlayerSkill)
        {
            double shotPercentage = ((PlayerSkill/shotDifficulty) * 100);
            Console.WriteLine("Shot Percent was: " + shotPercentage);
            Random s_Random = new Random();
            int perCent = s_Random.Next(0, 100);
            Console.WriteLine("Random was:"+ perCent);

            if (shotPercentage >= perCent){
                Console.WriteLine("Shot was: " + Shot);
                return true;
            }
            else{
                Console.WriteLine("Shot was: " + Shot);
                return false;
            }
        }
    }
}