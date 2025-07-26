using System;
using System.Collections.Generic;
using Golf.Simulator.App.Models;
using Golf.Simulator.App.Workers;
using Golf.Simulator.App.ObjectCreates;
using Golf.Simulator.App.ObjectLoads;
using Golf.Simulator.App.Interfaces;

namespace Golf.Simulator.App.Workers
{
    public class MatchWorker : IMatchWorker
    {
        private readonly MatchCreate _matchCreate;
        private readonly IRoundWorker _golfRound;
        private readonly TeamLoad _teamLoad;

        public MatchWorker(MatchCreate matchCreate, IRoundWorker golfRound, TeamLoad teamLoad)
        {
            _golfRound = golfRound;
            _teamLoad = teamLoad;
            _matchCreate = matchCreate;
        }

        public Match PlayMatch(int teamId1, int teamId2, string matchType, int courseId, int days)
        {
            int roundCount = 1;
            var team1 = _teamLoad.GetTeam(teamId1);
            var team2 = _teamLoad.GetTeam(teamId2);
            var team1PlayerIds = team1.roster.ConvertAll(r => r.playerId);
            var team2PlayerIds = team2.roster.ConvertAll(r => r.playerId);

            var match = _matchCreate.CreateMatch(roundCount, days, teamId1, teamId2, team1PlayerIds, team2PlayerIds, matchType, courseId);
            for (int day = 1; day <= match.MatchDays; day++)
            {
                var Team1Score = 0;
                var Team2Score = 0;
                for (int i = 0; i < team1.roster.Count; i++)
                {
                    var round = _golfRound.PlayGolfRound(courseId, team1.roster[i].playerId, roundCount);
                    var ms1 = new MatchScore
                    {
                        PlayerId = team1.roster[i].playerId,
                        RoundScore = round.PlayerScore
                    };
                    match.MatchScores.Add(ms1);
                    Team1Score += round.PlayerScore;
                    roundCount++;
                    _golfRound.ResetRound(round); // Reset the round for the next player
                    var round1 = _golfRound.PlayGolfRound(courseId, team2.roster[i].playerId, roundCount);
                    var ms2 = new MatchScore
                    {
                        PlayerId = team2.roster[i].playerId,
                        RoundScore = round.PlayerScore
                    };
                    match.MatchScores.Add(ms2);
                    Team2Score += round1.PlayerScore;
                    roundCount++;
                }
                match.Team1Scores.Add(new TeamScores { Day = day, RoundScore = Team1Score });
                match.Team2Scores.Add(new TeamScores { Day = day, RoundScore = Team2Score });
            }
            match.MatchResult = DetermineMatchResult(match);
            match.MatchStatus = "Completed"; // Update match status to completed
            return match;
        } 
      string DetermineMatchResult(Match match)
        {
            int team1Total = 0;
            int team2Total = 0;
            int team1Wins = 0;
            int team2Wins = 0;

            for (var day = 1; day <= match.MatchDays; day++)
            {
                // Get scores for each team for the day
                var team1Score = match.Team1Scores.FirstOrDefault(s => s.Day == day)?.RoundScore ?? 0;
                var team2Score = match.Team2Scores.FirstOrDefault(s => s.Day == day)?.RoundScore ?? 0;

                if (team1Score > team2Score)
                {
                    match.MatchResult = $"Team 1 Wins Day {day}";
                    team1Wins++;
                }
                else if (team2Score > team1Score)
                {
                    match.MatchResult = $"Team 2 Wins Day {day}";
                    team2Wins++;
                }
                else
                {
                    match.MatchResult = $"Day {day} is a Draw";
                }
            }
            if (team1Wins > team2Wins)
            {
                return "Team 1 Wins the Match";
            }
            else if (team2Wins > team1Wins)
            {
                return "Team 2 Wins the Match";
            }
            else
            {
                return "The Match is a Draw";
            }
        }
    }
}