using System;

namespace SportsManager
{
    class Player
     {
        public int strength = 8;
        public int stamina = 7;
        public int balance = 6;
        public int flexibility = 9;
        public int agility = 5;

        public int condition;
        public int awareness = 8;
        public int determination = 10;
        public int positivity = 5;
        public int demeanor = 7;
        public int fortitude =5;

        public int equipAccuracy = 7;
        public int quality = 8;
        public int fit = 6;

        public int shotShaping = 9;
        public int tempo = 7;
        public int swing = 3;
        public int accuracy = 5;
        public int ballStriking = 10;

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
            int physicalRating = (agility + flexibility + balance + stamina + strength);   
            return physicalRating;      
        }
        int getConditionRating()
        {
            int conditionRating;
            Random rand = new Random();
            int conditionRand = rand.Next (0, 3);
            switch (conditionRand){
                case 0:
                    conditionRating = 0;
                    condition = 0;
                    break;
                case 1:
                    conditionRating = 20;
                    condition = 4;
                    break;
                case 2:
                    conditionRating = 40;
                    condition = 8;
                    break;
                case 3:
                    conditionRating = 50;
                    condition = 10;
                    break;
                default:
                    conditionRating = 0;
                    condition = 10;
                    break;
            }
            return conditionRating;
        }
        int getMentalRating ()
        {
            int mentalRating = (fortitude + demeanor + positivity + determination + awareness);   
            return mentalRating;      
        }
        int getEquipmentRating ()
        {
            int equipmentRating = (fit + quality + accuracy);   
            return equipmentRating;      
        }
        int getMechanicsRating ()
        {
            int mechanicsRating = (ballStriking + accuracy + swing + tempo);   
            return mechanicsRating;      
        }
     }
}