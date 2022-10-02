using System;

namespace SportsManager
{
    class MainClass
     {
        static void Main(string[] args)
        {
            GolfRound round = new GolfRound(1, 1);
            Console.WriteLine ("roundScore: " + round.roundScore);
        }
    }
}