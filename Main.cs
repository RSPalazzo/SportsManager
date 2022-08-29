using System;

namespace SportsManager
{
    class MainClass
     {
        static int shotDifficulty;
        static int playerSkill;
        static ShotSimulator sim = new ShotSimulator();
        static ShotDeterminer shot1 = new ShotDeterminer();
        static PlayerSkillDeterminer ps1 = new PlayerSkillDeterminer();
        static void Main(string[] args)
        {
            shotDifficulty = shot1.getShotDifficulty();
            playerSkill = ps1.getPlayerSkill();
            Console.WriteLine("shotDifficulty: " + shotDifficulty + " playerSkill: " + playerSkill);
            sim.ShotGenerator(shotDifficulty, playerSkill);
        }
    }
}