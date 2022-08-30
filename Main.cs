using System;

namespace SportsManager
{
    class MainClass
     {
        static int roundScore;
        static GolfRound round = new GolfRound();
        static void Main(string[] args)
        {
            roundScore = round.startRound();
            Console.WriteLine ("roundScore: " + roundScore);
        }
    }
}