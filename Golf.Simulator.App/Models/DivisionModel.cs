namespace Golf.Simulator.App.Models
{
    public class Division
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