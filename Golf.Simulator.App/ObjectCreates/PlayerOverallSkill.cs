using Golf.Simulator.App.Models;
using Golf.Simulator.App.ObjectLoads;

namespace Golf.Simulator.App.ObjectCreates
{
    public class PlayerOverallSkill
    {
        public int getPlayerSkill(Player player)
        {
            if (player == null || player.attributes == null)
            {
                throw new ArgumentNullException(nameof(player), "Player or player attributes cannot be null.");
            }
            int physical = getPhysicalRating(player);
            int condition = player.attributes.playerCondition;
            int mental = getMentalRating(player);
            int equipment = getEquipmentRating(player);
            int mechanics = getMechanicsRating(player);
            int playerSkill = physical + condition + mental + equipment + mechanics;
            if (playerSkill < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(playerSkill), "Player skill cannot be negative.");
            }
            return playerSkill;
        }
        int getPhysicalRating(Player player)
        {
            int physicalRating = player.attributes.physical.agility + player.attributes.physical.flexibility + player.attributes.physical.balance
                                    + player.attributes.physical.stamina + player.attributes.physical.strength;
            return physicalRating;
        }
        int getMentalRating(Player player)
        {
            int mentalRating = player.attributes.mental.fortitude + player.attributes.mental.demeanor + player.attributes.mental.positivity
                                 + player.attributes.mental.determination + player.attributes.mental.awareness;
            return mentalRating;
        }
        int getEquipmentRating(Player player)
        {
            int equipmentRating = player.attributes.equipment.fit + player.attributes.equipment.quality + player.attributes.equipment.accuracy;
            return equipmentRating;
        }
        int getMechanicsRating(Player player)
        {
            int mechanicsRating = player.attributes.mechanics.ballStriking + player.attributes.mechanics.accuracy + player.attributes.mechanics.swing
                                    + player.attributes.mechanics.tempo + player.attributes.mechanics.shotShaping;
            return mechanicsRating;
        }
    }
}
