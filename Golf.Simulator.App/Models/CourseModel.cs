using System.Collections.Generic;
using System.Numerics;

namespace Golf.Simulator.App.Models
{
    public class Course
    {
        public string courseName { get; set; }
        public int coursePar { get; set; }
        public double courseRating { get; set; }
        public int courseSlope { get; set; }
        public int courseYardage { get; set; }
        public List<Hole> holes { get; set; }
    }

    public class Hole
    {
        public string holeName { get; set; }
        public int holeNumber { get; set; }
        public int holePar { get; set; }
        public int holeHandicap { get; set; }
        public Size holeSize { get; set; }
        public int holeYardage { get; set; }
        public string holeDirection { get; set; }
        public HoleLayout holeLayout { get; set; }
        public Green green { get; set; }
    }

    public class HoleLayout
    {
        public List<Locations> fairway { get; set; }
        public List<Locations> firstCut { get; set; }
        public int rough { get; set; }
        public List<Locations> woods { get; set; }
        public List<Locations> sand { get; set; }
        public List<Locations> water { get; set; }
        public List<Locations> deepRough { get; set; }
    }

    public class Green
    {
        public Locations greenLocation { get; set; }
        public Vector2 pin { get; set; }
        public int firmness { get; set; }
        public int stimp { get; set; }
        public int complex { get; set; }
    }
    public class Locations
    {
        public int width { get; set; }
        public Vector2 startPosition { get; set; }
        public Vector2 endPosition { get; set; }
    }
    public class Size
    {
        public int length { get; set; }
        public int width { get; set; }
    }
}