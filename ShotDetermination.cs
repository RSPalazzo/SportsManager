using System;

namespace SportsManager
{
    class ShotDeterminer 
    {
        public int getShotDifficulty ()
        {
            int weather = getWeatherRating();
            int lie = getLieRating();
            int location = getLocationRating();
            int grass = getGrassRating();
            int club = getClubRating();
            int shotType = getShotTypeRating();
            int shotTraj = getShotTrajRating();
            
            int shotDifficulty = (weather + lie + location + grass + club + shotType + shotTraj);
            return shotDifficulty; 
        }
        int getWeatherRating ()
        {
            Random rand = new Random();
            int wind = rand.Next(0, 40);    
            int rain = rand.Next(0, 40);  
            int altitude = rand.Next(0, 10);  
            int temp = rand.Next(0, 10); 

            int weatherRating = (wind + rain + altitude + temp);   
            return weatherRating;      
        }
        int getLieRating()
        {
            int lieRating;
            Random rand = new Random();
            int lie = rand.Next (0, 11);
            switch (lie){
                case 0:
                    lieRating = 30;
                    break;
                case 1:
                    lieRating = 40;
                    break;
                case 2:
                    lieRating = 50;
                    break;
                case 3:
                    lieRating = 60;
                    break;
                case 4:
                    lieRating = 150;
                    break;
                case 5:
                    lieRating = 200;
                    break;
                case 6:
                    lieRating = 5000;
                    break;
                case 7:
                    lieRating = 120;
                    break;
                case 8:
                    lieRating = 90;
                    break;
                case 9:
                    lieRating = 100;
                    break;
                case 10:
                    lieRating = 140;
                    break;
                case 11:
                    lieRating = 100; 
                    break;
                default:
                    lieRating = 0;
                    break;
            }
            return lieRating;
        }
        int getLocationRating()
        {
            int locationRating;
            Random rand = new Random();
            int location = rand.Next (0, 11);
            switch (location){
                case 0:
                    locationRating = 30;
                    break;
                case 1:
                    locationRating = 50;
                    break;
                case 2:
                    locationRating = 100;
                    break;
                case 3:
                    locationRating = 200;
                    break;
                case 4:
                    locationRating = 100;
                    break;
                case 5:
                    locationRating = 130;
                    break;
                case 6:
                    locationRating = 150;
                    break;
                case 7:
                    locationRating = 200;
                    break;
                case 8:
                    locationRating = 250;
                    break;
                case 9:
                    locationRating = 250;
                    break;
                case 10:
                    locationRating = 170;
                    break;
                case 11:
                    locationRating = 220; 
                    break;
                default:
                    locationRating = 0;
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
        int getClubRating()
        {
            int clubRating;
            Random rand = new Random();
            int club = rand.Next (0, 9);
            switch (club){
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
        int getShotTypeRating()
        {
            int shotTypeRating;
            Random rand = new Random();
            int shotType = rand.Next (0, 11);
            switch (shotType){
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
    }
}