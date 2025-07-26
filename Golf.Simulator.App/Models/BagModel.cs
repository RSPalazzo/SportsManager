namespace Golf.Simulator.App.Models
{
    public class Bag
    {
        public List<Clubs> bag { get; set; }
    }
    public class Clubs
    {
        public string ClubName { get; set; }
        public int ClubDistance { get; set; }
        public int Tolerance { get; set; }
    }
}
