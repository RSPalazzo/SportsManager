using System.Collections.Generic;

namespace Golf.Simulator.App.Models
{
    public class Course
    {
        public string courseName { get; set; }
        public string coursePar { get; set; }
        public string courseRating { get; set; }
        public string courseSlope { get; set; }
        public string courseYardage { get; set; }
        public List<Hole> holes { get; set; }
    }

    public class Hole
    {
        public string holeName { get; set; }
        public int holeNumber { get; set; }
        public int holePar { get; set; }
        public int holeHandicap { get; set; }
        public int holeYardage { get; set; }
        public string holeDirection { get; set; }
        public HoleLayout holeLayout { get; set; }
        public Green green { get; set; }
    }

    public class HoleLayout
    {
        public List<int> fairway { get; set; }
        public List<int> firstCut { get; set; }
        public int rough { get; set; }
        public List<int> woods { get; set; }
        public List<List<int>> sand { get; set; }
        public List<List<int>> water { get; set; }
        public List<List<int>> deepRough { get; set; }
    }

    public class Green
    {
        public List<int> size { get; set; }
        public int uphill { get; set; }
        public int sidehill { get; set; }
        public int downhill { get; set; }
        public int stimp { get; set; }
        public int complex { get; set; }
    }
}