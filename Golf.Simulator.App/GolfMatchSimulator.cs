using Golf.Simulator.App.Interfaces;    
using System;

namespace Golf.Simulator.App
{
    public class GolfMatchSimulator
    {
        private readonly IMatchWorker _matchWorker;

        public GolfMatchSimulator(IMatchWorker matchWorker)
        {
            _matchWorker = matchWorker;
        }

        public void Run()
        {
            var match = _matchWorker.PlayMatch(1, 2, "Foursomes", 1, 2);
            // You can add code here to use the 'match' variable as needed
        }
    }
}