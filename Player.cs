using System;
using Newtonsoft.Json.Linq;

namespace SportsManager
{
    class Player
     {
        //Player Info
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string height { get; set; }
        public string weight  { get; set; }
        public string birthday { get; set; }
        //playerImage
        public string country { get; set; }
        //country flag
        public string handedness { get; set; }
        public string personality { get; set; }
        //Attributes
        //Physical
        public int strength { get; set; }
        public int stamina { get; set; }
        public int balance { get; set; }
        public int flexibility { get; set; }
        public int agility { get; set; }
        //Condition
        public int condition { get; set; }
        //Mental
        public int awareness { get; set; }
        public int determination { get; set; }
        public int positivity { get; set; }
        public int demeanor { get; set; }
        public int fortitude { get; set; }
        //equipment
        public int equipAccuracy { get; set; }
        public int quality { get; set; }
        public int fit { get; set; }
        //mechanics
        public int shotShaping { get; set; }
        public int tempo { get; set; }
        public int swing { get; set; }
        public int accuracy { get; set; }
        public int ballStriking { get; set; }
        public int playerOverallSkill { get; set; }

        public Player(int playerId)
        {
            var jsonString = System.IO.File.ReadAllText("data/Players/Player" + playerId + ".json");
            var jObject = JObject.Parse(jsonString);
            firstName = jObject.SelectToken("Player.playerFirstName").Value<string>();
            lastName = jObject.SelectToken("Player.playerLastName").Value<string>();
            fullName = jObject.SelectToken("Player.playerFullName").Value<string>();
            height = jObject.SelectToken("Player.playerHeight").Value<string>();
            weight = jObject.SelectToken("Player.playerWeight").Value<string>();
            birthday = jObject.SelectToken("Player.playerBirthday").Value<string>();
            //playerImage
            country = jObject.SelectToken("Player.playerCountry").Value<string>();
            //country flag
            handedness = jObject.SelectToken("Player.playerHandedness").Value<string>();
            personality = jObject.SelectToken("Player.playerPersonality").Value<string>();
            
            //Physical
            strength = jObject.SelectToken("Player.attributes.physical.strength").Value<int>();
            stamina = jObject.SelectToken("Player.attributes.physical.stamina").Value<int>();
            balance = jObject.SelectToken("Player.attributes.physical.balance").Value<int>();
            flexibility = jObject.SelectToken("Player.attributes.physical.flexibility").Value<int>();
            agility = jObject.SelectToken("Player.attributes.physical.agility").Value<int>();
            //Condition
            condition  = jObject.SelectToken("Player.attributes.playerCondition").Value<int>();
            //Mental
            awareness = jObject.SelectToken("Player.attributes.mental.awareness").Value<int>();
            determination = jObject.SelectToken("Player.attributes.mental.determination").Value<int>();
            positivity = jObject.SelectToken("Player.attributes.mental.positivity").Value<int>();
            demeanor = jObject.SelectToken("Player.attributes.mental.demeanor").Value<int>();
            fortitude = jObject.SelectToken("Player.attributes.mental.fortitude").Value<int>();
            //equipment
            equipAccuracy = jObject.SelectToken("Player.attributes.equipment.accuracy").Value<int>();
            quality = jObject.SelectToken("Player.attributes.equipment.quality").Value<int>();
            fit = jObject.SelectToken("Player.attributes.equipment.fit").Value<int>();
            //mechanics
            shotShaping = jObject.SelectToken("Player.attributes.mechanics.shotShaping").Value<int>();
            tempo = jObject.SelectToken("Player.attributes.mechanics.tempo").Value<int>();
            swing = jObject.SelectToken("Player.attributes.mechanics.swing").Value<int>();
            accuracy = jObject.SelectToken("Player.attributes.mechanics.accuracy").Value<int>();
            ballStriking = jObject.SelectToken("Player.attributes.mechanics.ballStriking").Value<int>();

            playerOverallSkill = getPlayerSkill();

        }
        int getPlayerSkill ()
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