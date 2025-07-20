using Golf.Simulator.App.Models;
using System.Text.Json;

namespace Golf.Simulator.App.ObjectLoads
{
    public class TeamLoad
    {
        public Team GetTeam(int TeamId)
        {
            // Use AppContext.BaseDirectory to reference the application's root directory
            var fileName = Path.Combine(AppContext.BaseDirectory, "data", "Teams", $"Team{TeamId}.json");

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Team data file not found: {fileName}");
            }

            var jsonString = File.ReadAllText(fileName);
            Team? team = JsonSerializer.Deserialize<Team>(jsonString);

            if (team == null)
            {
                throw new InvalidOperationException($"Failed to deserialize team data from: {fileName}");
            }

            return team;
        }
    }
}
