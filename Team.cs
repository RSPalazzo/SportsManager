using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SportsManager
{
    public class Team
    {
        public TeamRoot GetTeam(int TeamId)
        {
            var jsonString = System.IO.File.ReadAllText("data/Teams/Team" + TeamId + ".json");
            TeamRoot team = JsonConvert.DeserializeObject<TeamRoot>(jsonString);
            return team;
        }
    }
    public class TeamRoot
    {
        public string teamName { get; set; }
        public string teamLogo { get; set; }
        public string foundingDate { get; set; }
        public int ownerId { get; set; }
        public string reputation { get; set; }
        public string nickname { get; set; }
        public int captainId { get; set; }
        public int viceCaptainId { get; set; }
        public int division { get; set; }
        public List<Roster> roster { get; set; }
    }
    public class Roster
    {
            public string playerId { get; set; }  
    }
}