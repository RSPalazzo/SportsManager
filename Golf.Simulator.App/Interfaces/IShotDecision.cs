using Golf.Simulator.App.Models;

namespace Golf.Simulator.App.Interfaces
{
    public interface IShotDecision
    {
        public Shot DetermineShot(Player player, Course course, BagDistance bagDistance, CurrentBallPosition currentBall);
    }
}
                                                                   