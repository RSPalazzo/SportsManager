using System;

namespace SportsManager
{
    class GolfSim {
        
        static bool Shot = false;
        static int shotDifficulty;
        static int playerSkill;
        static void Main(string[] args)
        {
            shotDifficulty = getshotDifficulty();
            playerSkill = getPlayerSkill();
            Console.WriteLine("shotDifficulty: " + shotDifficulty + " playerSkill: " + playerSkill);
            ShotGenerator(shotDifficulty, playerSkill);
            Console.WriteLine("Shot was: " + Shot);
        }

        static int getshotDifficulty ()
        {
            Console.WriteLine("Enter weather rating: ");
            int weather = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter lie rating: ");
            int lie = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter location rating: ");
            int location = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter grass rating: ");
            int grass = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter club rating: ");
            int club = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter shotType rating: ");
            int shotType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter shotTraj rating: ");
            int shotTraj = Convert.ToInt32(Console.ReadLine());
            
            int shotD = shotDifficultyCalculator(weather, lie, location, grass, club, shotType, shotTraj);
            return shotD;
        }
        static int getPlayerSkill ()
        {
            Console.WriteLine("Enter physicl rating: ");
            int physical = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter condition rating: ");
            int condition = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter mental rating: ");
            int mental = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter equipment rating: ");
            int equipment = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter mechanics rating: ");
            int mechanics = Convert.ToInt32(Console.ReadLine());
            
            int playerS = playerSkillCalculator(physical, condition, mental, equipment, mechanics);
            return playerS;
        }


        static void ShotGenerator (int shotDiff, int playerSk)
        {
            Shot = ShotSimulation(shotDiff, playerSk);
        }
        static int shotDifficultyCalculator (int weather, int lie, int location, int grass, int club, int shotType, int shotTraj)
        {
            shotDifficulty = (weather + lie + location + grass + club + shotType + shotTraj);
            return shotDifficulty; 
        }

        static int playerSkillCalculator (int physical, int condition, int mental, int equipment, int mechanics)
        {
            playerSkill = (physical + condition + mental + equipment + mechanics);
            return playerSkill; 
        }

        static bool ShotSimulation (double shotDifficulty, double PlayerSkill)
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