using System;

namespace SportsManager
{
    class PlayerSkillDeterminer
    {
        public int getPlayerSkill ()
        {
            int physical = getPhysicalRating();
            int condition = getConditionRating();
            int mental = getMentalRating();
            int equipment = getEquipmentRating();
            int mechanics = getMechanicsRating();
            
            int playerSkill = (physical + condition + mental + equipment + mechanics);
            return playerSkill; 
        }
        int getPhysicalRating ()
        {
            Random rand = new Random();
            int agility = rand.Next(0, 10);    
            int flexibility = rand.Next(0, 10);  
            int altitude = rand.Next(0, 10);  
            int stamina = rand.Next(0, 10);
            int strength = rand.Next (0, 10); 

            int physicalRating = (agility + flexibility + altitude + stamina + strength);   
            return physicalRating;      
        }
        int getConditionRating()
        {
            int conditionRating;
            Random rand = new Random();
            int condition = rand.Next (0, 3);
            switch (condition){
                case 0:
                    conditionRating = 0;
                    break;
                case 1:
                    conditionRating = 20;
                    break;
                case 2:
                    conditionRating = 40;
                    break;
                case 3:
                    conditionRating = 50;
                    break;
                default:
                    conditionRating = 0;
                    break;
            }
            return conditionRating;
        }
        int getMentalRating ()
        {
            Random rand = new Random();
            int fortitude = rand.Next(0, 10);    
            int demeanor = rand.Next(0, 10);  
            int positivity = rand.Next(0, 10);  
            int determination = rand.Next(0, 10);
            int awareness = rand.Next (0, 10); 

            int mentalRating = (fortitude + demeanor + positivity + determination + awareness);   
            return mentalRating;      
        }
        int getEquipmentRating ()
        {
            Random rand = new Random();
            int fit = rand.Next(0, 10);    
            int quality = rand.Next(0, 20);  
            int accuracy = rand.Next(0, 20);  

            int equipmentRating = (fit + quality + accuracy);   
            return equipmentRating;      
        }
        int getMechanicsRating ()
        {
            Random rand = new Random();   
            int tempo = rand.Next(0, 10);  
            int swing = rand.Next(0, 10);  
            int accuracy = rand.Next(0, 10);
            int ballStriking = rand.Next (0, 20); 

            int mechanicsRating = (ballStriking + accuracy + swing + tempo);   
            return mechanicsRating;      
        }
    }
}