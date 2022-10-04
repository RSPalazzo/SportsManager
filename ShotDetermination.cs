using System;

namespace SportsManager
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
        public ShotDeterminer(int holeNumber, int holeShotNumber, int distanceToHole, Player play, GolfCourse course, int grade)
        {
            weather = getWeatherRating();
            grass = getGrassRating();
            int locationRating = getLocationRating(holeShotNumber, course, grade, holeNumber); 
            int lieRating = getLieRating(holeShotNumber, location);
            getShot(distanceToHole, lie, location, play);
            int clubRating = getClubChoiceRating(club);
            int shotRating = getShotTypeRating(shotType);
            shotTraj = getShotTrajRating();
            
            shotDifficulty = (weather + lieRating + locationRating + grass + clubRating + shotRating + shotTraj);
        }
        int getWeatherRating ()
        {
            Random rand = new Random();
            wind = rand.Next(0, 40);    
            rain = rand.Next(0, 40);  
            altitude = rand.Next(0, 10);  
            temp = rand.Next(0, 10); 

            int weatherRating = (wind + rain + altitude + temp);   
            return weatherRating;      
        }
        int getLocationRating(int shotNum , GolfCourse course, int grade, int holeNumber)
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
                        fairway = course.course.holes[holeNumber].holeLayout.fairway + course.course.holes[holeNumber].holeLayout.sand 
                                        + course.course.holes[holeNumber].holeLayout.deepRough + course.course.holes[holeNumber].holeLayout.woods 
                                        + course.course.holes[holeNumber].holeLayout.water;
                        firstCut = fairway + course.course.holes[holeNumber].holeLayout.firstCut;
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
                        fairway = course.course.holes[holeNumber].holeLayout.fairway + course.course.holes[holeNumber].holeLayout.sand 
                                    + course.course.holes[holeNumber].holeLayout.woods ;
                        firstCut = fairway + course.course.holes[holeNumber].holeLayout.firstCut;
                        rough = course.course.holes[holeNumber].holeLayout.rough + course.course.holes[holeNumber].holeLayout.water + firstCut;
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
                        fairway = course.course.holes[holeNumber].holeLayout.fairway; 
                        firstCut = course.course.holes[holeNumber].holeLayout.firstCut + fairway;
                        rough = course.course.holes[holeNumber].holeLayout.rough + firstCut + course.course.holes[holeNumber].holeLayout.woods;
                        deepRough = course.course.holes[holeNumber].holeLayout.deepRough + rough;
                        sand = course.course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.course.holes[holeNumber].holeLayout.water + sand;
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
                        fairway = course.course.holes[holeNumber].holeLayout.fairway; 
                        firstCut = course.course.holes[holeNumber].holeLayout.firstCut + fairway;
                        rough = course.course.holes[holeNumber].holeLayout.rough + firstCut;
                        deepRough = course.course.holes[holeNumber].holeLayout.deepRough + rough;
                        sand = course.course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.course.holes[holeNumber].holeLayout.woods + water;
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
                        firstCut = course.course.holes[holeNumber].holeLayout.firstCut;
                        rough = course.course.holes[holeNumber].holeLayout.rough + firstCut;
                        deepRough = course.course.holes[holeNumber].holeLayout.deepRough + course.course.holes[holeNumber].holeLayout.fairway + rough;
                        sand = course.course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.course.holes[holeNumber].holeLayout.woods + water;
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
                        rough = course.course.holes[holeNumber].holeLayout.rough + course.course.holes[holeNumber].holeLayout.firstCut;
                        deepRough = course.course.holes[holeNumber].holeLayout.deepRough + course.course.holes[holeNumber].holeLayout.fairway + rough;
                        sand = course.course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.course.holes[holeNumber].holeLayout.woods + water;
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
                        deepRough = course.course.holes[holeNumber].holeLayout.deepRough + course.course.holes[holeNumber].holeLayout.fairway 
                                    + course.course.holes[holeNumber].holeLayout.rough + course.course.holes[holeNumber].holeLayout.firstCut;
                        sand = course.course.holes[holeNumber].holeLayout.sand  + deepRough;
                        water = course.course.holes[holeNumber].holeLayout.water + sand;
                        woods = course.course.holes[holeNumber].holeLayout.woods + water;
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
            else if (distanceToHole > 250 && distanceToHole <= 265)
            {
                club = "Driving Iron";
            }
            else if (distanceToHole > 265 && distanceToHole <= 300)
            {
                club = "Wood";
            }
            else if (distanceToHole > 120 && distanceToHole <= 165)
            {
                club = "Wedge";
            }
            else if (distanceToHole > 165 && distanceToHole <= 185)
            {
                club = "Short Iron";
            }
            else if (distanceToHole > 185 && distanceToHole <= 210)
            {
                club = "Mid Iron";
            }
            else if (distanceToHole > 210 && distanceToHole <= 235)
            {
                club = "Long Iron";
            }
            else if (distanceToHole > 235 && distanceToHole <= 265)
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
        bool getHoleReachability (int distanceToHole, string clubDistanceSim, Player playerr)
        {
            
            int strength = playerr.player.attributes.physical.strength;
            int flexibility = playerr.player.attributes.physical.flexibility;
            int balance = playerr.player.attributes.physical.balance;
            int agility = playerr.player.attributes.physical.agility;
            int tempo = playerr.player.attributes.mechanics.tempo;
            int swing = playerr.player.attributes.mechanics.swing;
            int ballStriking = playerr.player.attributes.mechanics.ballStriking;
            int fit = playerr.player.attributes.equipment.fit;
            int quality = playerr.player.attributes.equipment.quality;
            int demeanor = playerr.player.attributes.mental.demeanor;
            int condition = playerr.player.attributes.playerCondition;
            int baseDistance = getClubBaseDistance(clubDistanceSim);
            int totalYards = (strength + flexibility + balance + agility + tempo + swing + ballStriking + fit + quality 
                                + demeanor + condition + grass + rain + altitude + temp + baseDistance);
            Console.WriteLine(totalYards);
            if (totalYards > distanceToHole)
            { 
                if (club != "Driver")
                {
                    int rightClub = totalYards - distanceToHole;
                    if (rightClub <= 20 && rightClub >= -20)
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
        public int getClubBaseDistance(string clubChoice)
        {
            int clubYards;
            switch (clubChoice){
                case "Driver":
                    clubYards = 200;
                    break;
                case "Driving Iron":
                    clubYards = 120;
                    break;
                case "Lob Wedge":
                    clubYards  = 0;
                    break;
                case "Wood":
                    clubYards  = 140;
                    break;
                case "Long Iron":
                    clubYards  = 80;
                    break;
                case "Hybrid":
                    clubYards  = 100;
                    break;
                case "Mid Iron":
                    clubYards  = 60;
                    break;
                case "Short Iron":
                    clubYards  = 40;
                    break;
                case "Wedge":
                    clubYards  = 20;
                    break;
                case "Putter":
                    clubYards  = 10;
                    break;
                default:
                    clubYards  = 0;
                    break;
            }
            return clubYards;
        }
    }
}