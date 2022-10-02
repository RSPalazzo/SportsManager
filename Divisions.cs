
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SportsManager
{
    public class Division
    {
        public DivisionRoot GetDivision(string DivisionName)
        {
            var jsonString = System.IO.File.ReadAllText("data/Divisions/Division" + DivisionName + ".json");
            DivisionRoot div = JsonConvert.DeserializeObject<DivisionRoot>(jsonString);
            return div;
        }
    }
    public class DivisionRoot
    {
        public string divisionName { get; set; }
        public string divisionLogo { get; set; }
        public List<DivisionTeams> divisionTeams { get; set; }
    }
    public class DivisionTeams
    {
            public string teamId { get; set; }  
    }
}