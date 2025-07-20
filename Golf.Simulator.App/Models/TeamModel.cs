namespace Golf.Simulator.App.Models
{
    public class Team
    {
        public string teamName { get; set; }
        public string teamLogo { get; set; }
        public string foundingDate { get; set; }
        public int ownerId { get; set; }
        public string reputation { get; set; }
        public string nickname { get; set; }
        public int captainId { get; set; }
        public int viceCaptainId { get; set; }
        public string division { get; set; }
        public List<Roster> roster { get; set; }

    }
    public class Roster
    {
            public int playerId { get; set; }  
    }
}