using Golf.Simulator.App.Models;

namespace Golf.Simulator.App.ObjectCreates
{
    public class BagCreate
    {
        public Bag CreateBagDistance(Player player)
        {
            List<Clubs> clubs = new List<Clubs>();
            foreach (var club in player.playerBag)
            {
                // Create a new Clubs instance for each club in the player's bag
                var newClub = new Clubs
                {
                    ClubName = club.clubName,
                    ClubDistance = getClubDistance(club.clubName, player), // Get the distance for each club
                    Tolerance = 0//getTolerance(club.clubName, player) // Default tolerance value, can be adjusted as needed
                };
                clubs.Add(newClub);
            }
            Bag bag = new Bag
            {
                bag = clubs
            };
            return bag;
        }
        int getClubDistance(string clubChoice, Player player)
        {
            int clubYards;
            switch (clubChoice)
            {
                case "Driver":
                    clubYards = 280;
                    break;
                case "Three Wood":
                    clubYards = 255;
                    break;
                case "Five Wood":
                    clubYards = 240;
                    break;
                case "Hybrid":
                    clubYards = 225;
                    break;
                case "Driving Iron":
                    clubYards = 215;
                    break;
                case "Four Iron":
                    clubYards = 205;
                    break;
                case "Five Iron":
                    clubYards = 195;
                    break;
                case "Six Iron":
                    clubYards = 185;
                    break;
                case "Seven Iron":
                    clubYards = 175;
                    break;
                case "Eight Iron":
                    clubYards = 165;
                    break;
                case "Nine Iron":
                    clubYards = 155;
                    break;
                case "Pitching Wedge":
                    clubYards = 140;
                    break;
                case "Gap Wedge":
                    clubYards = 130;
                    break;
                case "Sand Wedge":
                    clubYards = 120;
                    break;
                case "Lob Wedge":
                    clubYards = 100;
                    break;
                default:
                    clubYards = 0;
                    break;
            }
            // Adjust club yards based on player attributes
            int playerDistance = player.attributes.physical.strength + player.attributes.physical.flexibility + player.attributes.physical.speed;

            // Example adjustment: 5's in attributes is neutral , 10's are good, and 0's are bad max stats adds 75 yards to each club Bryson shit
            //Substract 15 to make 15 neutral
            
            clubYards += (playerDistance - (20 - player.attributes.mechanics.swing)) * 5;

            return clubYards;
        }

    }
}
