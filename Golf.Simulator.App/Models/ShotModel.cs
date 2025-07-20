using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf.Simulator.App.Models
{
    public class Shot
    {
        public string ClubChoice { get; set; }
        public int Distance { get; set; }
        public int LocationLeft { get; set; }
        public int LocationRight { get; set; }

    }
}
