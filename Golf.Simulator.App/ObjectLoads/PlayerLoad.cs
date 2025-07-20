using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Golf.Simulator.App.Models;

namespace Golf.Simulator.App.ObjectLoads
{
    public class PlayerLoad
    {
        //public int playerOverallSkill { get; set; }
        public Player GetPlayer(int playerId)
        {
            var fileName = Path.Combine(AppContext.BaseDirectory, "data", "Players", $"Player{playerId}.json");

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Player data file not found: {fileName}");
            }

            var jsonString = File.ReadAllText(fileName);
            Player? player = JsonSerializer.Deserialize<Player>(jsonString);

            if (player == null)
            {
                throw new InvalidOperationException($"Failed to deserialize player data from: {fileName}");
            }

            return player;
        }

    }
}
