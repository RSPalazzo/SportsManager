using Golf.Simulator.App.Models;
using System;

namespace Golf.Simulator.App.GolfCalcuation
{
    class ShotDeterminer 
    {
        public string lie;
        public string location;
        public string club;
        public string shotType;
        public int weather;
        public int wind;    
        public int rain;  
        public int altitude; 
        public int temp;
        public int grass;
        public int shotTraj;
        public int shotDifficulty;
        public ShotDeterminer(int holeNumber, int holeShotNumber, int distanceToHole, Player play, Course course, int grade)
        {
            weather = getWeatherRating();
            grass = getGrassRating();
            int locationRating = getLocationRating(holeShotNumber, course, grade, holeNumber); 
            int lieRating = getLieRating(holeShotNumber, location);
            getShot(distanceToHole, lie, location, play);
            int clubRating = getClubChoiceRating(club);
            int shotRating = getShotTypeRating(shotType);
            shotTraj = getShotTrajRating();
            
            shotDifficulty = weather + lieRating + locationRating + grass + clubRating + shotRating + shotTraj;
        }
        int getWeatherRating ()
        {
            Random rand = new Random();
            wind = rand.Next(0, 40);    
            rain = rand.Next(0, 40);  
            altitude = rand.Next(0, 10);  
            temp = rand.Next(0, 10); 

            int weatherRating = wind + rain + altitude + temp;   
            return weatherRating;      
        }
        int getLocationRating(int shotNum , Course course, int grade, int holeNumber)
        {
            int locationRating;
            if (shotNum == 0) {
                location = "Tee Box";
            }
            else{ 
                int fairway ;
                int firstCut;
                int rough;
                int deepRough;
                int sand;
                int water;
                int woods;
                Random rand = new Random();
                int rando = rand.Next (1, 100);
                switch (grade){                   
                    case 1:
                        fairway = course.holes[holeNumber].holeLayout.fairway + course.holes[holeNumber].holeLayout.sand 
                                        + course.holes[holeNumber].holeLayout.deepRough + course.holes[holeNumber].holeLayout.woods 
                                        + course.holes[holeNumber].holeLayout.water;
                        firstCut = fairway + course.holes[holeNumber].holeLayout.firstCut;
                        if (rando <= fairway)
                        {
                            location = "Fairway";
                        }
                        else if (rando > fairway && rando <= firstCut)
                        {
                            location = "First Cut";
                        }
                        else
                        {
                            location = "Rough";
                        }
                        break;
                    case 2:
                        fairway = course.holes[holeNumber].holeLayout.fairway + course.holes[holeNumber].holeLayout.sand 
                                    + course.holes[holeNumber].holeLayout.woods ;
                        firstCut = fairway + course.holes[holeNumber].holeLayout.firstCut;
                        rough = course.holes[holeNumber].holeLayout.rough + course.holes[holeNumber].holeLayout.water + firstCut;
                        if (rando <= fairway)
                        {
                            location = "Fairway";
                        }
                        else if (rando > fairway && rando <= firstCut)
                        {
                            location = "First Cut";
                        }
                        else if (rando > firstCut && rando <= rough)
                        {
                            location = "Rough";
                        }
                        else
                        {
                            location = "Deep Rough";
                        }
                        break;
                    case 3:
                        fairway = course.holes[holeNumber].holeLayout.fairway; 
                        firstCut = course.holes[holeNumber].holeLayout.firstCut + fairway;
                        rough = course.holes[holeNumber].holeLayout.rough + firstCut + course.holes[holeNumber].holeLayout.woods;
                        deepRough = course.holes[holeNumber].holeLayout.deepRough + rough;
                        sand = course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.holes[holeNumber].holeLayout.water + sand;
                        if (rando <= fairway)
                        {
                            location = "Fairway";
                        }
                        else if (rando > fairway && rando <= firstCut)
                        {
                            location = "First Cut";
                        }
                        else if (rando > firstCut && rando <= rough)
                        {
                            location = "Rough";
                        }
                        else if (rando > rough && rando <= deepRough)
                        {
                            location = "Deep Rough";
                        }
                        else if (rando > deepRough && rando <= sand)
                        {
                            location = "Sand";
                        }
                        else
                        {
                            location = "Water";
                        }
                        break;
                    case 4:
                        fairway = course.holes[holeNumber].holeLayout.fairway; 
                        firstCut = course.holes[holeNumber].holeLayout.firstCut + fairway;
                        rough = course.holes[holeNumber].holeLayout.rough + firstCut;
                        deepRough = course.holes[holeNumber].holeLayout.deepRough + rough;
                        sand = course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.holes[holeNumber].holeLayout.woods + water;
                        if (rando <= fairway)
                        {
                            location = "Fairway";
                        }
                        else if (rando > fairway && rando <= firstCut)
                        {
                            location = "First Cut";
                        }
                        else if (rando > firstCut && rando <= rough)
                        {
                            location = "Rough";
                        }
                        else if (rando > rough && rando <= deepRough)
                        {
                            location = "Deep Rough";
                        }
                        else if (rando > deepRough && rando <= sand)
                        {
                            location = "Sand";
                        }
                        else if (rando > sand && rando <= water)
                        {
                            location = "Water";
                        }
                        else
                        {
                            location = "Woods";
                        }
                        break;
                    case 5:
                        firstCut = course.holes[holeNumber].holeLayout.firstCut;
                        rough = course.holes[holeNumber].holeLayout.rough + firstCut;
                        deepRough = course.holes[holeNumber].holeLayout.deepRough + course.holes[holeNumber].holeLayout.fairway + rough;
                        sand = course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.holes[holeNumber].holeLayout.woods + water;
                        if (rando <= firstCut)
                        {
                            location = "First Cut";
                        }
                        else if (rando > firstCut && rando <= rough)
                        {
                            location = "Rough";
                        }
                        else if (rando > rough && rando <= deepRough)
                        {
                            location = "Deep Rough";
                        }
                        else if (rando > deepRough && rando <= sand)
                        {
                            location = "Sand";
                        }
                        else if (rando > sand && rando <= water)
                        {
                            location = "Water";
                        }
                        else
                        {
                            location = "Woods";
                        }
                        break;
                    case 6:
                        rough = course.holes[holeNumber].holeLayout.rough + course.holes[holeNumber].holeLayout.firstCut;
                        deepRough = course.holes[holeNumber].holeLayout.deepRough + course.holes[holeNumber].holeLayout.fairway + rough;
                        sand = course.holes[holeNumber].holeLayout.sand  + deepRough;   
                        water = course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.holes[holeNumber].holeLayout.woods + water;
                        if (rando <= rough)
                        {
                            location = "Rough";
                        }
                        else if (rando > rough && rando <= deepRough)
                        {
                            location = "Deep Rough";
                        }
                        else if (rando > deepRough && rando <= sand)
                        {
                            location = "Sand";
                        }
                        else if (rando > sand && rando <= water)
                        {
                            location = "Water";
                        }
                        else
                        {
                            location = "Woods";
                        }
                        break;
                    default:
                        deepRough = course.holes[holeNumber].holeLayout.deepRough + course.holes[holeNumber].holeLayout.fairway 
                                    + course.holes[holeNumber].holeLayout.rough + course.holes[holeNumber].holeLayout.firstCut;
                        sand = course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.holes[holeNumber].holeLayout.woods + water;
                        if (rando <= deepRough)
                        {
                            location = "Deep Rough";
                        }
                        else if (rando > deepRough && rando <= sand)
                        {
                            location = "Sand";
                        }
                        else if (rando > sand && rando <= water)
                        {
                            location = "Water";
                        }
                        else
                        {
                            location = "Woods";
                        }
                        break;
                }
            }
            switch (location){
                case "Fairway":
                    locationRating = 30;
                    break;
                case "First Cut":
                    locationRating = 50;
                    break;
                case "Rough":
                    locationRating = 100;
                    break;
                case "Deep Rough":
                    locationRating = 200;
                    break;
                case "Woods":
                    locationRating = 250;
                    break;
                case "Water":
                    locationRating = 250;
                    break;
                case "Sand":
                    locationRating = 150;
                    break;
                case "Cart Path":
                    locationRating = 200;
                    break;
                case "Obstructed":
                    locationRating = 250;
                    break;
                case "Behind Something":
                    locationRating = 250;
                    break;
                default:
                    locationRating = 0;   
                    break;
            }
            return locationRating;
        }
        int getLieRating(int shotNum, string location)
        {
            int lieRating;
            if (shotNum == 0) {
               lie = "Teed Up";
            }
            else{ 
                Random rand = new Random();
                int rando = rand.Next (0, 7);
                switch(location)
                {
                    case "Fairway":
                        if (rando == 0)
                        {
                            lie = "Grain With";
                        }
                        else if (rando == 1)
                        {
                            lie = "Grain Against";
                        }
                        else if (rando == 2)
                        {
                            lie = "Divot";
                        }
                        else if (rando == 3)
                        {
                            lie = "Uphill";
                        }
                        else if (rando == 4)
                        {
                            lie = "Downhill";
                        }        
                        else if (rando == 5)
                        {
                            lie = "Ball Above Feet";
                        }
                        else if (rando == 6)
                        {
                            lie = "Ball Below Feet";
                        }
                        else
                        {
                            lie = "Tight";
                        }                   
                        break;
                    case "First Cut":
                        if (rando == 0)
                        {
                            lie = "Grain With";
                        }
                        else if (rando == 1)
                        {
                            lie = "Grain Against";
                        }
                        else if (rando == 2)
                        {
                            lie = "Divot";
                        }
                        else if (rando == 3)
                        {
                            lie = "Uphill";
                        }
                        else if (rando == 4)
                        {
                            lie = "Downhill";
                        }        
                        else if (rando == 5)
                        {
                            lie = "Ball Above Feet";
                        }
                        else if (rando == 6)
                        {
                            lie = "Ball Below Feet";
                        }
                        else
                        {
                            lie = "Tight";
                        }    
                        break;
                    case "Rough":
                        if (rando == 0)
                        {
                            lie = "Grain With";
                        }
                        else if (rando == 1)
                        {
                            lie = "Grain Against";
                        }
                        else if (rando == 2)
                        {
                            lie = "Fluffy";
                        }
                        else if (rando == 3)
                        {
                            lie = "Uphill";
                        }
                        else if (rando == 4)
                        {
                            lie = "Downhill";
                        }        
                        else if (rando == 5)
                        {
                            lie = "Ball Above Feet";
                        }
                        else if (rando == 6)
                        {
                            lie = "Ball Below Feet";
                        }
                        else
                        {
                            lie = "Buried";
                        }    
                        break;
                    case "Deep Rough":
                        if (rando == 0)
                        {
                            lie = "Grain With";
                        }
                        else if (rando == 1)
                        {
                            lie = "Grain Against";
                        }
                        else if (rando == 2)
                        {
                            lie = "Fluffy";
                        }
                        else if (rando == 3)
                        {
                            lie = "Uphill";
                        }
                        else if (rando == 4)
                        {
                            lie = "Downhill";
                        }        
                        else if (rando == 5)
                        {
                            lie = "Ball Above Feet";
                        }
                        else if (rando == 6)
                        {
                            lie = "Ball Below Feet";
                        }
                        else
                        {
                            lie = "Buried";
                        } 
                        break;
                    case "Woods":
                        lie = "Hard Pan";
                        break;
                    case "Water":
                        lie = "Unplayable";
                        break;
                    case "Sand":
                        if (rando <= 5)
                        {
                            lie = "Tight";
                        }
                        else
                        {
                            lie = "Fried Egg";
                        } 
                        break;
                    case "Cart Path":
                        break;
                    case "Obstructed":
                        break;
                    case "Behind Something":
                        break;
                    default:
                        break;
                }
            }
            switch (lie){
                case "Grain With":
                    lieRating = 30;
                    break;
                case "Grain Against":
                    lieRating = 40;
                    break;
                case "Tight":
                    lieRating = 50;
                    break;
                case "Fluffy":
                    lieRating = 60;
                    break;
                case "Buried":
                    lieRating = 150;
                    break;
                case "Fried Egg":
                    lieRating = 200;
                    break;
                case "Unplayable":
                    lieRating = 5000;
                    break;
                case "Downhill":
                    lieRating = 120;
                    break;
                case "Uphill":
                    lieRating = 90;
                    break;
                case "Ball Above Feet":
                    lieRating = 100;
                    break;
                case "Ball Below Feet":
                    lieRating = 140;
                    break;
                case "Divot":
                    lieRating = 100;
                    break;
                default:
                    lieRating = 150;
                    break;
            }
            return lieRating;
        }
        int getGrassRating()
        {
            int grassRating;
            Random rand = new Random();
            int grass = rand.Next (0, 5);
            switch (grass){
                case 0:
                    grassRating = 10;
                    break;
                case 1:
                    grassRating = 10;
                    break;
                case 2:
                    grassRating = 10;
                    break;
                case 3:
                    grassRating = 10;
                    break;
                case 4:
                    grassRating = 10;
                    break;
                case 5:
                    grassRating = 10;
                    break;
                default:
                    grassRating = 0;
                    break;
            }
            return grassRating;
        }
        int getClubChoiceRating(string clubChoice)
        {
            int clubRating;
            switch (clubChoice){
                case "Driver":
                    clubRating = 100;
                    break;
                case "Driving Iron":
                    clubRating = 90;
                    break;
                case "Lob Wedge":
                    clubRating = 80;
                    break;
                case "Wood":
                    clubRating = 70;
                    break;
                case "Long Iron":
                    clubRating = 60;
                    break;
                case "Hybrid":
                    clubRating = 50;
                    break;
                case "Mid Iron":
                    clubRating = 40;
                    break;
                case "Short Iron":
                    clubRating = 30;
                    break;
                case "Wedge":
                    clubRating = 20;
                    break;
                case "Putter":
                    clubRating = 10;
                    break;
                default:
                    clubRating = 0;
                    break;
            }
            return clubRating;
        }
        int getShotTypeRating(string shotT)
        {
            int shotTypeRating;
            switch (shotT){
                case "Full Swing":
                    shotTypeRating = 100;
                    break;
                case "Layup":
                    shotTypeRating = 50;
                    break;
                case "Back in Play":
                    shotTypeRating = 20;
                    break;
                case "Pitch":
                    shotTypeRating = 80;
                    break;
                case "Bump and Run":
                    shotTypeRating = 40;
                    break;
                case "Chip Shot":
                    shotTypeRating = 70;
                    break;
                case "Hinge and Hold":
                    shotTypeRating = 55;
                    break;
                case "Flop Shot":
                    shotTypeRating = 200;
                    break;
                case "Greenside Bunker":
                    shotTypeRating = 150;
                    break;
                case "Step on it":
                    shotTypeRating = 200; 
                    break;
                default:
                    shotTypeRating = 0;
                    break;
            }
            return shotTypeRating;
        }
        int getShotTrajRating()
        {
            int shotTrajRating;
            Random rand = new Random();
            int shotTraj = rand.Next (0, 9);
            switch (shotTraj){
                case 0:
                    shotTrajRating = 20;
                    break;
                case 1:
                    shotTrajRating = 40;
                    break;
                case 2:
                    shotTrajRating = 50;
                    break;
                case 3:
                    shotTrajRating = 40;
                    break;
                case 4:
                    shotTrajRating = 80;
                    break;
                case 5:
                    shotTrajRating = 90;
                    break;
                case 6:
                    shotTrajRating = 40;
                    break;
                case 7:
                    shotTrajRating = 80;
                    break;
                case 8:
                    shotTrajRating = 90;
                    break;
                case 9:
                    shotTrajRating = 100;
                    break;
                default:
                    shotTrajRating = 0;
                    break;
            }
            return shotTrajRating;
        }
        void getShot(int distanceToHole, string shotLie, string shotLocation, Player player)
        {            
            if (shotLie == "Unplayable")
            {
                //Take Drop
            }
            else
            {
                //assess weather
                //Calculate good leave distance
                getClubChoice(distanceToHole);
                getShotType(distanceToHole, lie, location, player);

            
            }
        }
        void getClubChoice(int distanceToHole)
        {
            if (distanceToHole > 300)
            {
                club = "Driver";
            }
            else if (distanceToHole > 232 && distanceToHole <= 244)
            {
                club = "Driving Iron";
            }
            else if (distanceToHole > 245 && distanceToHole <= 300)
            {
                club = "Wood";
            }
            else if (distanceToHole > 100 && distanceToHole <= 136)
            {
                club = "Wedge";
            }
            else if (distanceToHole > 137 && distanceToHole <= 160)
            {
                club = "Short Iron";
            }
            else if (distanceToHole > 161 && distanceToHole <= 186)
            {
                club = "Mid Iron";
            }
            else if (distanceToHole > 187 && distanceToHole <= 220)
            {
                club = "Long Iron";
            }
            else if (distanceToHole > 221 && distanceToHole <= 232)
            {
                club = "Hybrid";
            }
            else
            {
                club = "Wedge";
            }
        }
        void getShotType(int shotDistanceToHole, string shotLie, string shotLocation, Player playerShotType)
        {  
            //TODO: Fix Club Choice and Reachability to account for Wind and Temp etc.
            bool fullSwing = getHoleReachability(shotDistanceToHole, club, playerShotType);
            if (fullSwing == true)
            {
                shotType = "Full Swing";
            }
            else if (fullSwing == false && shotDistanceToHole > 30)
            {
                shotType = "Pitch";
            }
            else if (fullSwing == false && shotDistanceToHole <= 30 && shotLocation == "Sand")
            {
                shotType = "Greenside Bunker";
            }
            else if (fullSwing == false && shotDistanceToHole <= 30 && shotLocation != "Sand")
            {
                getChipShotType(shotLie);
            }
            else
            {
                shotType = "Flop Shot";
            }
        }
        bool getHoleReachability (int distanceToHole, string clubDistanceSim, Player player)
        {
            
            int strength = player.attributes.physical.strength;
            int flexibility = player.attributes.physical.flexibility;
            int balance = player.attributes.physical.balance;
            int agility = player.attributes.physical.agility;
            int tempo = player.attributes.mechanics.tempo;
            int swing = player.attributes.mechanics.swing;
            int ballStriking = player.attributes.mechanics.ballStriking;
            int fit = player.attributes.equipment.fit;
            int quality = player.attributes.equipment.quality;
            int demeanor = player.attributes.mental.demeanor;
            int condition = player.attributes.playerCondition;
            int baseDistance = getClubBaseDistance(clubDistanceSim);
            //TODO Remove or Restore
            //int totalYards = (strength + flexibility + balance + agility + tempo + swing + ballStriking + fit + quality 
            //                    + demeanor + condition + grass + rain + altitude + temp + baseDistance);
            int totalYards = strength + flexibility + balance + agility + tempo + swing + ballStriking + fit + quality 
                                + demeanor + condition + baseDistance;
            Console.WriteLine(totalYards);
            if (totalYards < distanceToHole)
            { 
                Console.WriteLine(club);
                if (club != "Driver")
                {
                    int rightClub = totalYards - distanceToHole;
                    Console.WriteLine(rightClub);
                    if (rightClub <= 20)
                    {
                        return true;
                    }
                    else
                    {
                        // TODO: Club down if you can
                        Console.WriteLine("IDK MAN");
                        return false;   
                    }
                }
                else
                {
                    return true;
                } 
            }
            else
            {
                if (club == "Driver")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        void getChipShotType(string shotLie)
        {
            if (shotLie == "Grain With" || shotLie == "Grain Against")
            {
                shotType = "Chip Shot";
            }
            else if (shotLie != "Grain With" || shotLie != "Grain Against")
            {
                shotType = "Hinge and Hold";
            }
        }
        public BagDistance getBagDistances(Player player)
        {
            BagDistance bagDistance = new BagDistance();
            bagDistance.Driver = getClubDistance("Driver", player);
            bagDistance.ThreeWood = getClubDistance("3 Wood", player);
            bagDistance.FiveWood = getClubDistance("5 Wood", player);
            bagDistance.Hybrid = getClubDistance("Hybrid", player);
            bagDistance.DrivingIron = getClubDistance("Driving Iron", player);
            bagDistance.FourIron = getClubDistance("4 Iron", player);
            bagDistance.FiveIron = getClubDistance("5 Iron", player);
            bagDistance.SixIron = getClubDistance("6 Iron", player);
            bagDistance.SevenIron = getClubDistance("7 Iron", player);
            bagDistance.EightIron = getClubDistance("8 Iron", player);
            bagDistance.NineIron = getClubDistance("9 Iron", player);
            bagDistance.PitchingWedge = getClubDistance("Pitching Wedge", player);
            bagDistance.GapWedge = getClubDistance("Gap Wedge", player);
            bagDistance.SandWedge = getClubDistance("Sand Wedge", player);
            bagDistance.LobWedge = getClubDistance("Lob Wedge", player);
            return bagDistance;
        }
        public int getClubDistance(string clubChoice, Player player)
        {
            int clubYards;
            switch (clubChoice){
                case "Driver":
                    clubYards = 280;
                    break;
                case "3 Wood":
                    clubYards = 255;
                    break;
                case "5 Wood":
                    clubYards = 240;
                    break;
                case "Hybrid":
                    clubYards = 225;
                    break;
                case "Driving Iron":
                    clubYards = 215;
                    break;
                case "4 Iron":
                    clubYards  = 205;
                    break;
                case "5 Iron":
                    clubYards  = 195;
                    break;
                case "6 Iron":
                    clubYards  = 185;
                    break;
                case "7 Iron":
                    clubYards  = 175;
                    break;
                case "8 Iron":
                    clubYards  = 165;
                    break;
                case "9 Iron":
                    clubYards  = 155;
                    break;
                case "Pitching Wedge":
                    clubYards  = 140;
                    break;
                case "Gap Wedge":
                    clubYards  = 130;
                    break;
                case "Sand Wedge":
                    clubYards  = 120;
                    break;
                case "Lob Wedge":
                    clubYards  = 100;
                    break;
                default:
                    clubYards  = 0;
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