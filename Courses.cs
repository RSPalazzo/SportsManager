using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SportsManager
{
    public class GolfCourse 
    {
        public Course course { get; set; }
        public GolfCourse(int courseId)
        {
            var jsonString = System.IO.File.ReadAllText("data/Courses/Course" + courseId + ".json");
            Course co = JsonConvert.DeserializeObject<Course>(jsonString);
            course = co;
        }
    }
    public class Course
    {
        public List<Holes> holes { get; set; }
        public string courseName { get; set; }
        public int courseRating { get; set; }
        public int courseSlope { get; set; }
        public int coursePar { get; set; }
        public int courseYardage { get; set; }
    }
    public class Holes
    {
            public string holeName { get; set; }
            public int holeNumber { get; set; }
            public int holePar { get; set; }
            public int holeHandicap { get; set; }
            public int holeYardage { get; set; }
            public string holeLayout {get; set;}      
    }
}