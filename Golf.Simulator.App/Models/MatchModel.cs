namespace Golf.Simulator.App.Models
{
    public class Match
    {
        public int Team1 { get; set; }
        public int Team2 { get; set; }
        public int MatchId { get; set; }
        public string MatchDate { get; set; }
        public int MatchDays { get; set; } // Number of days the match is played over
        public string MatchStatus { get; set; } // e.g., "Completed", "In Progress"
        public string MatchType { get; set; } // e.g., "Singles", "Doubles", "Foursomes"
        public List<int> Team1Players { get; set; }
        public List<int> Team2Players { get; set; }
        public List<MatchScore> MatchScores { get; set; } // List of scores for each match played
        public int CourseId { get; set; } // Name of the course where the match is played
        public List<TeamScores> Team1Scores { get; set; }
        public List<TeamScores> Team2Scores { get; set; }
        public string MatchResult { get; set; } // e.g., "Team1 Wins", "Team2 Wins", "Draw"
    }
    public class MatchScore
    {
        public int PlayerId { get; set; } // ID of the player whose score is recorded
        public int RoundScore { get; set; } // Score for that hole
    }
    public class TeamScores
    {
        public int Day { get; set; } // ID of the player whose score is recorded
        public int RoundScore { get; set; } // Score for that hole
    }
}
