using System;

namespace SportsManager
{
    class ShotDeterminer 
    {
        public string lieName;
        public string locationName; 
        public string clubName;
        public string shotTypeName;
        public int weather;
        public int wind;    
        public int rain;  
        public int altitude; 
        public int temp; 
        public int lie;
        public int location;
        public int grass;
        public int club;
        public int shotType;
        public int shotTraj;
        public int shotDifficulty;
        public ShotDeterminer(int holeNumber, int holeShotNumber, int distanceToHole, Player play)
        {
            weather = getWeatherRating();
            grass = getGrassRating();
            int lieRating = getLieRating(holeShotNumber);
            int locationRating = getLocationRating(holeShotNumber);            
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
        int getLieRating(int shotNum)
        {
            int lieRating;
            if (shotNum == 0) {
               lie = 12;
            }
            else{ 
                Random rand = new Random();
                lie = rand.Next (0, 11);
            }
            switch (lie){
                case 0:
                    lieRating = 30;
                    lieName = "Grain With";
                    break;
                case 1:
                    lieRating = 40;
                    lieName = "Grain Against";
                    break;
                case 2:
                    lieRating = 50;
                    lieName = "Tight";
                    break;
                case 3:
                    lieRating = 60;
                    lieName = "Fluffy";
                    break;
                case 4:
                    lieRating = 150;
                    lieName = "Buried";
                    break;
                case 5:
                    lieRating = 200;
                    lieName = "Fried Egg";
                    break;
                case 6:
                    lieRating = 5000;
                    lieName = "Unplayable";
                    break;
                case 7:
                    lieRating = 120;
                    lieName = "Downhill";
                    break;
                case 8:
                    lieRating = 90;
                    lieName = "Uphill";
                    break;
                case 9:
                    lieRating = 100;
                    lieName = "Ball Above Feet";
                    break;
                case 10:
                    lieRating = 140;
                    lieName = "Ball Below Feet";
                    break;
                case 11:
                    lieRating = 100;
                    lieName = "Divot";
                    break;
                default:
                    lieRating = 0;
                    lieName = "Tee";
                    break;
            }
            return lieRating;
        }
        int getLocationRating(int shotNum)
        {
            int locationRating;
            if (shotNum == 0) {
                location = 12;
            }
            else{ 
                Random rand = new Random();
                location = rand.Next (0, 9);
            }

            switch (location){
                case 0:
                    locationRating = 30;
                    locationName = "Fairway";
                    break;
                case 1:
                    locationRating = 50;
                    locationName = "First Cut";
                    break;
                case 2:
                    locationRating = 100;
                    locationName = "Rough";
                    break;
                case 3:
                    locationRating = 200;
                    locationName = "Deep Rough";
                    break;
                case 4:
                    locationRating = 100;
                    locationName = "Pinestraw";
                    break;
                case 5:
                    locationRating = 130;
                    locationName = "Mulch";
                    break;
                case 6:
                    locationRating = 150;
                    locationName = "Sand";
                    break;
                case 7:
                    locationRating = 200;
                    locationName = "Cart Path";
                    break;
                case 8:
                    locationRating = 250;
                    locationName = "Obstructed";
                    break;
                case 9:
                    locationRating = 250;
                    locationName = "Behind Something";
                    break;
                default:
                    locationRating = 0;
                    locationName = "Tee"; 
                    break;
            }
            return locationRating;
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
        int getClubChoiceRating(int clubChoice)
        {
            int clubRating;
            switch (clubChoice){
                case 0:
                    clubRating = 100;
                    break;
                case 1:
                    clubRating = 90;
                    break;
                case 2:
                    clubRating = 80;
                    break;
                case 3:
                    clubRating = 70;
                    break;
                case 4:
                    clubRating = 60;
                    break;
                case 5:
                    clubRating = 50;
                    break;
                case 6:
                    clubRating = 40;
                    break;
                case 7:
                    clubRating = 30;
                    break;
                case 8:
                    clubRating = 20;
                    break;
                case 9:
                    clubRating = 10;
                    break;
                default:
                    clubRating = 0;
                    break;
            }
            return clubRating;
        }
        int getShotTypeRating(int shotT)
        {
            int shotTypeRating;
            switch (shotT){
                case 0:
                    shotTypeRating = 100;
                    break;
                case 1:
                    shotTypeRating = 50;
                    break;
                case 2:
                    shotTypeRating = 20;
                    break;
                case 3:
                    shotTypeRating = 80;
                    break;
                case 4:
                    shotTypeRating = 80;
                    break;
                case 5:
                    shotTypeRating = 40;
                    break;
                case 6:
                    shotTypeRating = 70;
                    break;
                case 7:
                    shotTypeRating = 55;
                    break;
                case 8:
                    shotTypeRating = 160;
                    break;
                case 9:
                    shotTypeRating = 200;
                    break;
                case 10:
                    shotTypeRating = 150;
                    break;
                case 11:
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
        void getShot(int distanceToHole, int shotLie, int shotLocation, Player player)
        {            
            if (shotLie == 6)
            {
                //Take Drop
            }
            else
            {
                if (shotLocation == 9)
                {
                    //Back in Play
                }
                else if (shotLocation == 8)
                {
                    //Layup or //Back in play
                }
                else
                {
                    //assess weather
                    bool reachable = getHoleReachability(distanceToHole, 0, player);
                    if (reachable == true)
                    {
                        club = getClubChoice(distanceToHole);
                        shotType = getShotType(distanceToHole, lie, location, player);
                    }
                    else
                    {
                        //Calculate good leave distance
                        club = getClubChoice(distanceToHole);
                        shotType = getShotType(distanceToHole, lie, location, player);
                    }
                }
            }
        }
        int getClubChoice(int distanceToHole)
        {
            int clubChoice;
            if (distanceToHole > 300)
            {
                clubChoice = 0;
                clubName = "Driver";
            }
            else if (distanceToHole > 250 && distanceToHole <= 265)
            {
                clubChoice = 1;
                clubName = "Driving Iron";
            }
            else if (distanceToHole > 265 && distanceToHole <= 300)
            {
                clubChoice = 3;
                clubName = "Wood";
            }
            else if (distanceToHole > 120 && distanceToHole <= 165)
            {
                clubChoice = 8;
                clubName = "Wedge";
            }
            else if (distanceToHole > 165 && distanceToHole <= 185)
            {
                clubChoice = 7;
                clubName = "Short Iron";
            }
            else if (distanceToHole > 185 && distanceToHole <= 210)
            {
                clubChoice = 6;
                clubName = "Mid Iron";
            }
            else if (distanceToHole > 210 && distanceToHole <= 235)
            {
                clubChoice = 4;
                clubName = "Long Iron";
            }
            else if (distanceToHole > 235 && distanceToHole <= 265)
            {
                clubChoice = 5;
                clubName = "Hybrid";
            }
            else
            {
                clubChoice = 8;
                clubName = "Wedge";
            }
            return clubChoice;
        }
        int getShotType(int shotDistanceToHole, int shotLie, int shotLocation, Player playerShotType)
        {  
            int shotTy;
            bool fullSwing = getHoleReachability(shotDistanceToHole, 0, playerShotType);
            if (fullSwing == true)
            {
                shotTy = 0;
                shotTypeName = "Full Swing";
            }
            else if (fullSwing == false && shotDistanceToHole > 30)
            {
                shotTy = 3;
                shotTypeName = "Pitch";
            }
            else if (fullSwing == false && shotDistanceToHole <= 30 && shotLocation == 6)
            {
                shotTy = 10;
                shotTypeName = "Greenside Bunker";
            }
            else if (fullSwing == false && shotDistanceToHole <= 30 && shotLocation != 6)
            {
                shotTy = getChipShotType(shotLie, shotLocation);
            }
            else
            {
                shotTy = 8;
                shotTypeName = "Flop Shot";
            }
            return shotTy;
        }
        bool getHoleReachability (int distanceToHole, int clubDistanceSim, Player playerr)
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
            int totalYards = (strength + flexibility + balance + agility + tempo + swing + ballStriking + fit + quality + demeanor + condition + grass + rain + altitude + temp + baseDistance);
            if (totalYards < distanceToHole)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        int getChipShotType(int shotLie, int shotLocation)
        {
            int shotTy = 8;
            if (shotLocation == 0)
            {
                shotTy = 6;
                shotTypeName = "Chip Shot";
            }
            else if (shotLocation != 0)
            {
                shotTy = 7;
                shotTypeName = "Hinge and Hold";
            }
            return shotTy;
        }
        public int getClubBaseDistance(int clubChoice)
        {
            int clubYards;
            switch (clubChoice){
                case 0:
                    clubYards = 200;
                    break;
                case 1:
                    clubYards = 120;
                    break;
                case 2:
                    clubYards  = 0;
                    break;
                case 3:
                    clubYards  = 140;
                    break;
                case 4:
                    clubYards  = 80;
                    break;
                case 5:
                    clubYards  = 100;
                    break;
                case 6:
                    clubYards  = 60;
                    break;
                case 7:
                    clubYards  = 40;
                    break;
                case 8:
                    clubYards  = 20;
                    break;
                case 9:
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