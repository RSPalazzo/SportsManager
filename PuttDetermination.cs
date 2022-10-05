using System;

namespace SportsManager
{
    class PuttDeterminer 
    {
        public string greenLie;
        public int getPuttDifficulty (GolfCourse gc, int holeNum, int distanceToHole)
        {

            int uphill = gc.course.holes[holeNum].green.uphill;
            int downhill = gc.course.holes[holeNum].green.downhill + uphill;
            int sidehill = gc.course.holes[holeNum].green.sidehill + downhill;
              int lieDiff = 0;
            if (greenLie == "")
            {
                Random rand = new Random();
                int rando = rand.Next(0, 100);
                if (rando <= uphill)
                {
                    greenLie = "Uphill";
                    lieDiff = gc.course.holes[holeNum].green.uphill;
                }
                else if (rando > uphill && rando <= downhill)
                {
                    greenLie = "Downhill";
                    lieDiff = gc.course.holes[holeNum].green.downhill;    
                }
                else
                {
                    greenLie = "Sidehill";
                    lieDiff = gc.course.holes[holeNum].green.sidehill;
                }
            }
            else
            {
                if (greenLie == "Uphill")
                {
                    lieDiff = gc.course.holes[holeNum].green.uphill;
                }
                else if (greenLie == "Sidehill")
                {
                    lieDiff = gc.course.holes[holeNum].green.downhill;    
                }
                else
                {
                    lieDiff = gc.course.holes[holeNum].green.sidehill;
                }
            }
            int difficulty = gc.course.holes[holeNum].green.complex + gc.course.holes[holeNum].green.stimp + lieDiff;
            difficulty = (difficulty * distanceToHole) / 2;
            return difficulty;
        }
        public int getPlayerPuttingSkill(Player p)
        {
           int puttingSKill = p.player.attributes.mental.fortitude + p.player.attributes.mechanics.swing + p.player.attributes.mental.awareness 
                                + p.player.attributes.mechanics.tempo + p.player.attributes.mental.determination + p.player.attributes.equipment.fit;
            return puttingSKill;  
        }
    }
}