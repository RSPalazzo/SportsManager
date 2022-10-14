
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SportsManager
{
    public class Result
    {
        ResultRoot result = new ResultRoot();
        public void PrintResult(int resultNum)
        {
            string jsonFileName = ("data/Result/Result" + resultNum + ".json");
            var jsonString = JsonConvert.SerializeObject(result);
            File.WriteAllText(jsonFileName, jsonString);
        }
        public void SetResult(string name, int roundNum)
        {
            result.fullName = name;
            result.roundNum = roundNum;
        }
        public void SetHole(int holeNumber, int holeYards, int par)
        {
            result.holeResults[holeNumber-1].holeNumber = holeNumber;
            result.holeResults[holeNumber-1].holeYards = holeYards;
            result.holeResults[holeNumber-1].par = par;
        }
        public void SetShot(int holeNumber, int shotNumber, string club, string shotType, string lie, string location, int shotDifficulty, 
                            int playerSkill, int percent, bool shot, int random, int grade, int hitDistance, int distanceLeft)
        {
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].shotNumber = shotNumber;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].club = club;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].shotType = shotType;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].lie = lie;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].location = location;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].shotDifficulty = shotDifficulty;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].playerSkill = playerSkill;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].percent = percent;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].shot = shot;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].random = random;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].grade = grade;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].hitDistance = hitDistance;
            result.holeResults[holeNumber-1].shotResults[shotNumber-1].distanceLeft = distanceLeft;
        }
        public void SetPutt(int holeNumber, int shotNumber, int puttDistance, string puttGrade, int randomNumber, 
                            int puttDifficulty, int playerSkill, bool puttResult, int puttPercent, int distanceLeft)
        {
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].puttDistance = puttDistance;
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].puttGrade = puttGrade;
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].randomNumber = randomNumber;
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].puttDifficulty = puttDifficulty;
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].playerSkill = playerSkill;
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].puttResult = puttResult;
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].puttPercent = puttPercent;
            result.holeResults[holeNumber-1].puttResults[shotNumber-1].distanceLeft = distanceLeft;
        }
        public void SetHoleScore(int holeNumber, int holeScore)
        {
            result.holeResults[holeNumber-1].holeScore = holeScore;
        }
        public void SetRoundScore(int roundScore)
        {
            result.roundScore = roundScore;
        }
    }
    public class ResultRoot
    {
        public string fullName { get; set; }
        public int roundNum { get; set; }
        public int roundScore {get; set;}
        public List<HoleResults> holeResults { get; set; }
    }
    public class HoleResults
    {
        public int holeNumber { get; set; }
        public int holeYards {get; set;}
        public int par {get; set;}
        public int holeScore {get; set;}
        public List<ShotResults> shotResults { get; set; } 
        public List<PuttResults> puttResults {get; set;}
    }
    public class ShotResults
    {
        public int shotNumber {get; set;}
        public string club {get; set;}
        public string shotType {get; set;} 
        public string lie {get; set;}
        public string location {get; set;}
        public int shotDifficulty {get; set;}
        public int playerSkill {get; set;}
        public int percent {get; set;}
        public bool shot {get; set;}
        public int random {get; set;}
        public int grade {get; set;}
        public int hitDistance {get; set;}
        public int distanceLeft {get; set;}
    }
    public class PuttResults
    {
        public int puttDistance {get; set;}
        public string puttGrade {get; set;}
        public int randomNumber {get; set;}
        public int puttDifficulty {get; set;}
        public int playerSkill {get; set;}
        public bool puttResult {get; set;}
        public int puttPercent {get; set;}
        public int distanceLeft {get; set;}
    } 
}