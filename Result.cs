/*
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
            result.holeResults = new List<HoleResults>();
        }
        public void SetHole(int holeNumber, int holeYards, int par)
        {
            result.holeResults.Add(new HoleResults {holeNumber = holeNumber, holeYards = holeYards, par = par, holeScore = 0});
            result.holeResults[holeNumber-1].shotResults = new List<ShotResults>();
            result.holeResults[holeNumber-1].puttResults = new  List<PuttResults>();
        }
        public void SetShot(int holeNumber, int shotNumber, string club, string shotType, string lie, string location, int shotDifficulty, 
                            int playerSkill, int percent, bool shot, int random, int grade, int hitDistance, int distanceLeft)
        {
            
            result.holeResults[holeNumber-1].shotResults.Add(new ShotResults {
                shotNumber = shotNumber, club = club, shotType = shotType, lie = lie, location = location, shotDifficulty = shotDifficulty, playerSkill = playerSkill,
                percent = percent, shot = shot, random = random, grade = grade, hitDistance = hitDistance, distanceLeft = distanceLeft});
        }
        public void SetPutt(int holeNumber, int shotNumber, int puttDistance, string puttGrade, int randomNumber, 
                            int puttDifficulty, int playerSkill, bool puttResult, int puttPercent, int distanceLeft)
        {
            
            result.holeResults[holeNumber-1].puttResults.Add(new PuttResults {
            puttDistance = puttDistance, puttGrade = puttGrade, randomNumber = randomNumber, puttDifficulty = puttDifficulty, playerSkill = playerSkill, 
            puttResult = puttResult,puttPercent = puttPercent, distanceLeft = distanceLeft});
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
}*/