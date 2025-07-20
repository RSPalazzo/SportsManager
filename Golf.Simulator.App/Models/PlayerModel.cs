using System;
using System.Text.Json;
using System.Collections.Generic;

namespace Golf.Simulator.App.Models
{
    
    public class Player
    {
        public string playerFirstName { get; set; }
        public string playerLastName { get; set; }
        public string playerFullName { get; set; }
        public string playerHeight { get; set; }
        public int playerWeight  { get; set; }
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