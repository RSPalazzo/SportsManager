using Golf.Simulator.App.Models;
using System;

namespace Golf.Simulator.App.GolfCalcuation
{
    class PuttDeterminer 
    {
        public string greenLie;
        public string puttGrade;
        public int getPuttDifficulty (Course gc, int holeNum, int distanceToHole)
        {

            int uphill = gc.holes[holeNum].green.uphill;
            int downhill = gc.holes[holeNum].green.downhill + uphill;
            int sidehill = gc.holes[holeNum].green.sidehill + downhill;
              int lieDiff = 0;
            if (greenLie == "")
            {
                Random rand = new Random();
                int rando = rand.Next(0, 100);
                if (rando <= uphill)
                {
                    greenLie = "Uphill";
                    lieDiff = 0;
                }
                else if (rando > uphill && rando <= downhill)
                {
                    greenLie = "Downhill";
                    lieDiff = 10;    
                }
                else
                {
                    greenLie = "Sidehill";
                    lieDiff = 5;
                }
            }
            else
            {
                if (greenLie == "Uphill")
                {
                    lieDiff = 0;
                }
                else if (greenLie == "Sidehill")
                {
                    lieDiff = 5;    
                }
                else
                {
                    lieDiff = 10;
                }
            }
            int difficulty = gc.holes[holeNum].green.complex + gc.holes[holeNum].green.stimp + lieDiff;
            difficulty = Convert.ToInt32(difficulty + distanceToHole * 1.5);
            return difficulty;
        }
        public int getPlayerPuttingSkill(Player p)
        {
           int puttingSKill = p.attributes.mental.fortitude + p.attributes.mechanics.swing + p.attributes.mental.awareness 
                                + p.attributes.mechanics.tempo + p.attributes.mental.determination + p.attributes.equipment.fit;
            return puttingSKill;  
        }
        public int getPuttGrade(int puttPercent, int perCent, bool shot, int distanceToHole)
        {
            int feet = 0;
            int preAbsGrade = puttPercent - perCent;
            int postAbsGrade = Math.Abs(preAbsGrade);
            // TODO: Deal with 5< shot percentages
            if (postAbsGrade >= 100)
            {
                puttGrade = "Perfect";
            }
            else
            {
                if (shot == true)
                {
                    if (postAbsGrade <= 30)
                    {
                        puttGrade = "Perfect";                
                    }
                    else if (postAbsGrade > 30 && postAbsGrade <= 50)
                    {
                        puttGrade = "Well Struck";
                    }
                    else
                    {
                        puttGrade = "Good";    
                    }
                }
                else
                {
                    if (postAbsGrade <= 15)
                    {
                        puttGrade = "Alright";                
                    }
                    else if (postAbsGrade > 15 && postAbsGrade <= 40)
                    {
                        puttGrade = "Below Average";
                    }
                    else if (postAbsGrade > 40 && postAbsGrade <= 70)
                    {
                        puttGrade = "Bad";
                    }
                    else
                    {
                        puttGrade = "Horrible";    
                    }
                }
            }
            
            feet = getPuttGradeAccuracy(puttGrade, distanceToHole);
            return feet;
        }
        int getPuttGradeAccuracy(string puttGrade, int distanceToHole)
        {
            int feet;
            Random rand = new Random();
            switch (puttGrade)
            {
                case "Perfect":
                    feet = 0;
                    break;
                case "Well Struck":
                    if (distanceToHole <= 5)
                    {
                        feet = 0;
                    }
                    else
                    {
                        feet = rand.Next (0, 2);
                    }
                    break;
                case "Good":
                    if (distanceToHole <= 5)
                    {
                        feet = 0;
                    }
                    else
                    {
                        feet = rand.Next (2, 3); 
                    }
                    break;
                case "Alright":
                    if (distanceToHole <= 5)
                    {
                        feet = 1;
                    }
                    else
                    {
                        feet = rand.Next (3, 5);
                    }
                    break;
                case "Below Average":
                    if (distanceToHole <= 10)
                    {
                        feet = rand.Next (3, 5);
                    }
                    else
                    {
                        feet = rand.Next (6, 10);
                    }
                    break;
                case "Bad":
                    if (distanceToHole <= 15)
                    {
                        feet = rand.Next (6, 10);
                    }
                    else
                    {
                        feet = rand.Next (11, 15);
                    }
                    break;
                default:
                    feet = rand.Next (20, 40);
                    break;
            }
            return feet;
        }
    }
}