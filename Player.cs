using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SportsManager
{
    class Player
     {
        
        public PlayerRoot player { get; set; }
        public int playerOverallSkill { get; set; }
        public Player(int playerId)
        {
            var jsonString = System.IO.File.ReadAllText("data/Players/Player" + playerId + ".json");
            PlayerRoot play = JsonConvert.DeserializeObject<PlayerRoot>(jsonString);
            player = play;
            playerOverallSkill = getPlayerSkill();     
        }
        int getPlayerSkill ()
        {
            int physical = getPhysicalRating();
            int condition = player.attributes.playerCondition;
            int mental = getMentalRating();
            int equipment = getEquipmentRating();
            int mechanics = getMechanicsRating();
            
            int playerSkill = (physical + condition + mental + equipment + mechanics);
            return playerSkill; 
        }
        int getPhysicalRating ()
        {
            int physicalRating = (player.attributes.physical.agility + player.attributes.physical.flexibility + player.attributes.physical.balance 
                                    + player.attributes.physical.stamina + player.attributes.physical.strength);   
            return physicalRating;      
        }
        int getMentalRating ()
        {
            int mentalRating = (player.attributes.mental.fortitude + player.attributes.mental.demeanor + player.attributes.mental.positivity
                                 + player.attributes.mental.determination + player.attributes.mental.awareness);   
            return mentalRating;      
        }
        int getEquipmentRating ()
        {
            int equipmentRating = (player.attributes.equipment.fit + player.attributes.equipment.quality + player.attributes.equipment.accuracy);   
            return equipmentRating;      
        }
        int getMechanicsRating ()
        {
            int mechanicsRating = (player.attributes.mechanics.ballStriking + player.attributes.mechanics.accuracy + player.attributes.mechanics.swing 
                                    + player.attributes.mechanics.tempo + player.attributes.mechanics.shotShaping);   
            return mechanicsRating;      
        }
     }
    public class PlayerRoot
    {
        public string playerFirstName { get; set; }
        public string playerLastName { get; set; }
        public string playerFullName { get; set; }
        public string playerHeight { get; set; }
        public string playerWeight  { get; set; }
        public string playerBirthday { get; set; }
        public string playerImage { get; set; }
        public string playerCountry { get; set; }
        public string playerCountryImage { get; set; }
        public string playerHandedness { get; set; }
        public string playerPersonality { get; set; }

        public Attributes attributes {get; set;}
    
    }   
    public class Attributes
    {
        public Physical physical { get; set; }
        public int playerCondition { get; set;}
        public Mental mental {get; set;}
        public Equipment equipment { get; set; }
        public Mechanics mechanics {get; set;}
    }
    public class Physical
    {
        public int strength { get; set; }
        public int stamina { get; set; }
        public int balance { get; set; }
        public int flexibility { get; set; }
        public int agility { get; set; }
    }
    public class Mental 
    {
        public int awareness { get; set; }
        public int determination { get; set; }
        public int positivity { get; set; }
        public int demeanor { get; set; }
        public int fortitude { get; set; }
    }
    public class Equipment
    {
        public int accuracy { get; set; }
        public int quality { get; set; }
        public int fit { get; set; }
    }

    public class Mechanics
    {
        public int shotShaping { get; set; }
        public int tempo { get; set; }
        public int swing { get; set; }
        public int accuracy { get; set; }
        public int ballStriking { get; set; }
    }
}