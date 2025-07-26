using Golf.Simulator.App.Models;

namespace Golf.Simulator.App.Interfaces
{
    public interface IRoundWorker
    {
        public GolfRound PlayGolfRound(int courseId, int playerId, int roundId);
        public void ResetRound(GolfRound golfRound);
    }
}
