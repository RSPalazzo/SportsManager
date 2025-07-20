using Golf.Simulator.App.Models;

namespace Golf.Simulator.App.Interfaces
{
    public interface IMatchWorker
    {
        public Match PlayMatch(int teamId1, int teamId2, string matchType, int courseId, int days);
    }
}
