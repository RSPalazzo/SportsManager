using Golf.Simulator.App.Models;
using System.Numerics;

namespace Golf.Simulator.App.Interfaces
{
    public interface IShotDecision
    {
        public Shot Decision(Player player, Course course, Bag bagDistance, CurrentBallPosition currentBall);
    }
}
                                                                   