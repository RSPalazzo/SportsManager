using Golf.Simulator.App.Models;
using System.Text.Json;

namespace Golf.Simulator.App.ObjectLoads
{
    public class CourseLoad
    {
        public Course GetGolfCourse(int courseId)
        {
            try
            {
                var jsonString = File.ReadAllText("data/Courses/Course" + courseId + ".json");
                var options = new JsonSerializerOptions();
                options.Converters.Add(new Vector2JsonConverter());
                Course course = JsonSerializer.Deserialize<Course>(jsonString, options);
                return course;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                throw;
            }
        }
    }
}
