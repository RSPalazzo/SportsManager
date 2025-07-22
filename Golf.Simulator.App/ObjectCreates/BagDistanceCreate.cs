using Golf.Simulator.App.Models;

namespace Golf.Simulator.App.ObjectCreates
{
    public class BagDistanceCreate
    {
        public BagDistance CreateBagDistance(Player player)
        {
            // Create a new BagDistance instance
            BagDistance bagDistance = new BagDistance
            {
                Driver = getClubDistance("Driver", player), // Default value for Driver
                ThreeWood = getClubDistance("ThreeWood", player), // Default value for Three Wood
                FiveWood = getClubDistance("FiveWood", player), // Default value for Five Wood
                Hybrid = getClubDistance("Hybrid", player), // Default value for Hybrid
                DrivingIron = getClubDistance("DrivingIron", player), // Default value for Driving Iron
                FourIron = getClubDistance("FourIron", player), // Default value for Four Iron
                FiveIron = getClubDistance("FiveIron", player), // Default value for Five Iron
                SixIron = getClubDistance("SixIron", player), // Default value for Six Iron
                SevenIron = getClubDistance("SevenIron", player), // Default value for Seven Iron
                EightIron = getClubDistance("EightIron", player), // Default value for Eight Iron
                NineIron = getClubDistance("NineIron", player), // Default value for Nine Iron
                PitchingWedge = getClubDistance("PitchingWedge", player), // Default value for Pitching Wedge
                GapWedge = getClubDistance("GapWedge", player), // Default value for Gap Wedge
                SandWedge = getClubDistance("SandWedge", player), // Default value for Sand Wedge
                LobWedge = getClubDistance("LobWedge", player) // Default value for Lob Wedge
            };
            // Return the created BagDistance object
            return bagDistance;
        }
        int getClubDistance(string clubChoice, Player player)
        {
            int clubYards;
            switch (clubChoice)
            {
                case "Driver":
                    clubYards = 280;
                    break;
                case "ThreeWood":
                    clubYards = 255;
                    break;
                case "FiveWood":
                    clubYards = 240;
                    break;
                case "Hybrid":
                    clubYards = 225;
                    break;
                case "Driving Iron":
                    clubYards = 215;
                    break;
                case "FourIron":
                    clubYards = 205;
                    break;
                case "FiveIron":
                    clubYards = 195;
                    break;
                case "SixIron":
                    clubYards = 185;
                    break;
                case "SevenIron":
                    clubYards = 175;
                    break;
                case "EightIron":
                    clubYards = 165;
                    break;
                case "NineIron":
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
            clubYards += (playerDistance - 15) * 5;

            return clubYards;
        }

    }
}
