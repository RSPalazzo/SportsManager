using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Golf.Simulator.App.Models
{
    public class CurrentBallPosition
    {
        public string lie { get; set; }
        public Vector2 ballPosition { get; set; }
        public int holeNumber { get; set; }
        public int distanceToPin { get; set; }
        public int puttLength { get; set; }
        public int puttMakePercentage { get; set; }

    }
}
