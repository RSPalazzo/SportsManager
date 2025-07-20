using Golf.Simulator.App.Models;
namespace Golf.Simulator.App.ObjectCreates
{
    public class GolfRoundCreate
    {
        public GolfRound CreateGolfRound(int courseId, int playerId, int roundId)
        {
            // Create a new GolfRound instance
            GolfRound golfRound = new GolfRound
            {
                CourseId = courseId,
                PlayerId = playerId,
                RoundId = roundId, //Look up the round number based on Datafile
                PlayerScore = 0, // Initialize player score
                RoundDate = DateTime.Now, // Replace with Game DateTime
                RoundStatus = "In Progress"
            };
            // Return the created GolfRound object
            return golfRound;
        }
    }
}
