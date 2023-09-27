using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SportsManager
{
    public class Match
    {
        public int golfCourseId = 1;
        public int TeamId1 = 1;
        public int TeamId2 = 2;
        public int Team1Score1;
        public int Team1Score2;
        public int Team1Player1Score1;
        public int Team1Player1Score2;
        public int Team1Player2Score1;
        public int Team1Player2Score2;
        public int Team1Player3Score1;
        public int Team1Player3Score2;
        public int Team1Player4Score1;
        public int Team1Player4Score2;
        public int Team2Score1;
        public int Team2Score2;
        public int Team2Player1Score1;
        public int Team2Player1Score2;
        public int Team2Player2Score1;
        public int Team2Player2Score2;
        public int Team2Player3Score1;
        public int Team2Player3Score2;
        public int Team2Player4Score1;
        public int Team2Player4Score2;

        public Match(int teamId1, int teamId2)
        {
            Team team1 = new Team(teamId1);
            Team team2 = new Team(teamId2);
            int roundCount = 1;
            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < team1.team.roster.Count; i++)
                {
                    GolfRound round = new GolfRound(golfCourseId, team1.team.roster[i].playerId, roundCount);
                    roundCount++;
                    if (r == 0 && i == 0)
                    {
                        Team1Player1Score1 = round.roundScore;
                    }
                    else if (r == 0 && i == 1)
                    {
                        Team1Player2Score1 = round.roundScore;
                    }
                    else if (r == 0 && i == 2)
                    {
                        Team1Player3Score1 = round.roundScore;
                    }
                    else if (r == 0 && i == 3)
                    {
                        Team1Player4Score1 = round.roundScore;
                    }
                    else if (r == 1 && i == 0)
                    {
                        Team1Player1Score2 = round.roundScore;
                    }
                    else if (r == 1 && i == 1)
                    {
                        Team1Player2Score2 = round.roundScore;
                    }
                    else if (r == 1 && i == 2)
                    {
                        Team1Player3Score2 = round.roundScore;
                    }
                    else if (r == 1 && i == 3)
                    {
                        Team1Player4Score2 = round.roundScore;
                    }
                    else{}
                }
                for (int i = 0; i < team2.team.roster.Count; i++)
                {
                    GolfRound round = new GolfRound(golfCourseId, team2.team.roster[i].playerId, roundCount);
                    roundCount++;
                    if (r == 0 && i == 0)
                    {
                        Team2Player1Score1 = round.roundScore;
                    }
                    else if (r == 0 && i == 1)
                    {
                        Team2Player2Score1 = round.roundScore;
                    }
                    else if (r == 0 && i == 2)
                    {
                        Team2Player3Score1 = round.roundScore;
                    }
                    else if (r == 0 && i == 3)
                    {
                        Team2Player4Score1 = round.roundScore;
                    }
                    else if (r == 1 && i == 0)
                    {
                        Team2Player1Score2 = round.roundScore;
                    }
                    else if (r == 1 && i == 1)
                    {
                        Team2Player2Score2 = round.roundScore;
                    }
                    else if (r == 1 && i == 2)
                    {
                        Team2Player3Score2 = round.roundScore;
                    }
                    else if (r == 1 && i == 3)
                    {
                        Team2Player4Score2 = round.roundScore;
                    }
                    else{}
                }
            }
            Team1Score1 = Team1Player1Score1 + Team1Player2Score1 + Team1Player3Score1 + Team1Player4Score1;
            Team1Score2 = Team1Player1Score2 + Team1Player2Score2 + Team1Player3Score2 + Team1Player4Score2;
            Team2Score1 = Team2Player1Score1 + Team2Player2Score1 + Team2Player3Score1 + Team2Player4Score1;
            Team2Score2 = Team2Player1Score2 + Team2Player2Score2 + Team2Player3Score2 + Team2Player4Score2;

            if (Team1Score1 < Team2Score1)
            {
                Console.WriteLine (team1.team.teamName + " wins day 1: " + Team1Score1 + " - " + Team2Score1);
            } 
            else
            {
                Console.WriteLine (team2.team.teamName + " wins day 1: " + Team2Score1 + " - " + Team1Score1);
            }         
            if (Team1Score2 < Team2Score2)
            {
                Console.WriteLine (team1.team.teamName + " wins day 2: " + Team1Score2 + " - " + Team2Score2);
            } 
            else
            {
                Console.WriteLine (team2.team.teamName + " wins day 2: " + Team2Score2 + " - " + Team1Score2);
            }    
        } 
    }
}