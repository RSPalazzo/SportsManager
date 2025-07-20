using Golf.Simulator.App.Models;
using System.Text.Json;

namespace Golf.Simulator.App.ObjectLoads
{
    public class CourseLoad
    {
        public Course GetGolfCourse(int courseId)
        {
            var jsonString = File.ReadAllText("data/Courses/Course" + courseId + ".json");
            Course course = JsonSerializer.Deserialize<Course>(jsonString);
            return course;
        }
    }
}
