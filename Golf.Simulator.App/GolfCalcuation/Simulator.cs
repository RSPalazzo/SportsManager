using System;

namespace Golf.Simulator.App.GolfCalcuation
{
    class ShotSimulator
     {
        public int perCent;
        public double shotPercentage;

        public bool ShotGenerator (int shotDiff, int playerSk)
        {
            bool Shot = ShotSimulation(shotDiff, playerSk);
            return Shot;
        }
        bool ShotSimulation (double shotDifficulty, double PlayerSkill)
        {
            shotPercentage = PlayerSkill/shotDifficulty * 100;
            
            Random s_Random = new Random();
            perCent = s_Random.Next(0, 100);
            if (shotPercentage >= perCent){
                return true;
            }
            else{
                return false;
            }
        }
    }
}