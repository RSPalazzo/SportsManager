using System;

namespace SportsManager
{
    class ShotSimulator
     {
       
        public bool ShotGenerator (int shotDiff, int playerSk)
        {
            bool Shot = ShotSimulation(shotDiff, playerSk);
            Console.WriteLine("Shot was: " + Shot);
            return Shot;
        }
        bool ShotSimulation (double shotDifficulty, double PlayerSkill)
        {
            double shotPercentage = ((PlayerSkill/shotDifficulty) * 100);
            Console.WriteLine("Shot Percent was: " + shotPercentage);
            Random s_Random = new Random();
            int perCent = s_Random.Next(0, 100);
            Console.WriteLine("Random was:"+ perCent);

            if (shotPercentage >= perCent){
                return true;
            }
            else{
                return false;
            }
        }
    }
}