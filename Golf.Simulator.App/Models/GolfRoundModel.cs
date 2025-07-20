using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf.Simulator.App.Models
{
    public class GolfRound
    {
        public int RoundId { get; set; }
        public int CourseId { get; set; }
        public int PlayerId { get; set; }
        public int PlayerScore { get; set; }
        public List<int> HoleScores { get; set; } = new List<int>();
        public DateTime RoundDate { get; set; }
        public string RoundStatus { get; set; } // e.g., "Completed", "In Progress"
    }
}
