using Golf.Simulator.App.Models;

namespace Golf.Simulator.App.ObjectCreates
{
    public class MatchCreate
    {
        public Match CreateMatch(int matchId, int matchDays, int team1, int team2, List<int> team1Ids, List<int> team2Ids, string matchType, int courseId)
        {
            // Create a new Match instance
            return new Match
            {
                Team1 = team1,
                Team2 = team2,
                MatchId = matchId,
                MatchDays = matchDays, // Number of days the match is played over
                MatchDate = DateTime.Now.ToString("yyyy-MM-dd"), // Change to Game Date
                MatchType = matchType,
                MatchStatus = "In Progress", // Initial status
                Team1Players = team1Ids, // List of player IDs for Team 1
                Team2Players = team2Ids, // List of player IDs for Team 2
                MatchScores = new List<MatchScore>(),
                CourseId = courseId, // Assuming courseId is an integer, convert it to string
                Team1Scores = new List<TeamScores>(),
                Team2Scores = new List<TeamScores>(),
                MatchResult = string.Empty
            };
        }
    }
}
