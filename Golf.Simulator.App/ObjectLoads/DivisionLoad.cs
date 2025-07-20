using Golf.Simulator.App.Models;
using System.Text.Json;

namespace Golf.Simulator.App.ObjectLoads
{
    public class DivisionLoad
    {
        public Division GetDivision(string DivisionName)
        {
            var jsonString = File.ReadAllText("data/Divisions/Division" + DivisionName + ".json");
            Division div = JsonSerializer.Deserialize<Division>(jsonString);
            return div;
        }
    }
}
