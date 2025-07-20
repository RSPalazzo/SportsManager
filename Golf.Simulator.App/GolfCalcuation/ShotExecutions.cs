using Golf.Simulator.App.Models;
using System.Numerics;
using Golf.Simulator.App.Interfaces;
using System.Security.Cryptography;

namespace Golf.Simulator.App.GolfCalcuation
{
    public class ShotExecutions
    {
        public ShotExecutions ExecuteShot(Player player, Course course, BagDistance bagDistance, Wind wind, Lie lie, int altitude, int temp, Rain rain, CurrentBallPosition currentBall)
        {
            
            return shotExecution;
        }

        Vector<int> Decision(Player player, Course course, BagDistance bagDistance ,Wind wind, Lie lie, int altitude, int temp, Rain rain, CurrentBallPosition currentBall)
        {
            if (player == null || course == null)
            {
                throw new ArgumentNullException("Player or Course cannot be null");
            }
            if (wind == null || lie == null || rain == null)
            {
                throw new ArgumentNullException("Wind, Lie, or Rain cannot be null");
            }
            if (currentBall.lie == "Tee")
            {
                Shot IdealShot = GetIdealShot(course, player, bagDistance, currentBall); //wind, lie, altitude, temp, rain, );
                Shot RiskyShot = GetRiskyShot(course, player, bagDistance, currentBall); //wind, lie, altitude, temp, rain, );
                Shot ConservitiveShot = GetConservitiveShot(course, player, bagDistance, currentBall); //wind, lie, altitude, temp, rain, );
            }


            return trajectory;
        }
        private Shot GetIdealShot(Course course, Player player, BagDistance bagDistance, CurrentBallPosition currentBall) // Wind wind, Lie lie, int altitude, int temp, Rain rain,)
        {
            // Logic to determine position A based on course, player, wind, lie, altitude, temp, and rain
            Shot idealShot = new Shot();
            var holeLayout = course.holes[currentBall.holeNumber].holeLayout;
            var isDriverIdeal = true;
            var isThreeWoodIdeal = true;
            var isFiveWoodIdeal = true;
            var isHybridIdeal = true;
            var isIronIdeal = true;
            // indexes for fairway, sand, water, etc.
            int startYardage = 1;
            int endYardage = 2;
            int width = 3;
            int positionFromLeft = 4;
            int positionFromRight = 5;
            // Tolerance misses
            int tolerance = 20 - player.attributes.mental.courseManagement;
            // Example logic (to be replaced with actual logic)
            if (holeLayout.sand.Any(s => s.Count > 0) || holeLayout.water.Any(s => s.Count > 0))
            {
                if (holeLayout.fairway[endYardage] - bagDistance.Driver <= tolerance)
                {
                    isDriverIdeal = false;
                }
                if (holeLayout.fairway[endYardage] - bagDistance.ThreeWood <= tolerance)
                {
                    isThreeWoodIdeal = false;
                }
                if (holeLayout.fairway[endYardage] - bagDistance.FiveWood <= tolerance)
                {
                    isFiveWoodIdeal = false;
                }
                if (holeLayout.fairway[startYardage] - bagDistance.Hybrid >= tolerance || holeLayout.fairway[endYardage] - bagDistance.Hybrid <= tolerance)
                {
                    isHybridIdeal = false;
                }
                if (holeLayout.fairway[startYardage] - bagDistance.DrivingIron >= tolerance || holeLayout.fairway[endYardage] - bagDistance.DrivingIron <= tolerance)
                {
                    isIronIdeal = false;
                }
                foreach (var sand in holeLayout.sand)
                {
                    if (bagDistance.Driver - sand[startYardage] <= tolerance || bagDistance.Driver - sand[endYardage] <= tolerance)
                    {
                        isDriverIdeal = false;
                    }
                    if (bagDistance.ThreeWood - sand[startYardage] <= tolerance || bagDistance.ThreeWood - sand[endYardage] <= tolerance)
                    {
                        isThreeWoodIdeal = false;
                    }
                    if (bagDistance.FiveWood - sand[startYardage] <= tolerance || bagDistance.FiveWood - sand[endYardage] <= tolerance)
                    {
                        isFiveWoodIdeal = false;
                    }
                    if (bagDistance.Hybrid - sand[startYardage] <= tolerance || bagDistance.Hybrid - sand[endYardage] <= tolerance)
                    {
                        isHybridIdeal = false;
                    }
                    if (bagDistance.DrivingIron - sand[startYardage] <= tolerance || bagDistance.DrivingIron - sand[endYardage] <= tolerance)
                    {
                        isIronIdeal = false;
                    }
                }
                foreach (var water in holeLayout.water)
                {
                    if (bagDistance.Driver - water[startYardage] <= tolerance || bagDistance.Driver - water[endYardage] <= tolerance)
                    {
                        isDriverIdeal = false;
                    }
                    if (bagDistance.ThreeWood - water[startYardage] <= tolerance || bagDistance.ThreeWood - water[endYardage] <= tolerance)
                    {
                        isThreeWoodIdeal = false;
                    }
                    if (bagDistance.FiveWood - water[startYardage] <= tolerance || bagDistance.FiveWood - water[endYardage] <= tolerance)
                    {
                        isFiveWoodIdeal = false;
                    }
                    if (bagDistance.Hybrid - water[startYardage] <= tolerance || bagDistance.Hybrid - water[endYardage] <= tolerance)
                    {
                        isHybridIdeal = false;
                    }
                    if (bagDistance.DrivingIron - water[startYardage] <= tolerance || bagDistance.DrivingIron - water[endYardage] <= tolerance)
                    {
                        isIronIdeal = false;
                    }
                }
            }
            if (isDriverIdeal)
            {
                idealShot.ClubChoice = "Driver";
                idealShot.Distance = bagDistance.Driver;
                //Middle of fairway
                idealShot.LocationLeft = (holeLayout.fairway[positionFromLeft] + (holeLayout.fairway[width] / 2));
                idealShot.LocationRight = (holeLayout.fairway[positionFromRight] + (holeLayout.fairway[width] / 2));
                return idealShot;
            }
            if (isThreeWoodIdeal)
            {
                idealShot.ClubChoice = "Three Wood";
                idealShot.Distance = bagDistance.ThreeWood;
                //Middle of fairway
                idealShot.LocationLeft = (holeLayout.fairway[positionFromLeft] + (holeLayout.fairway[width] / 2));
                idealShot.LocationRight = (holeLayout.fairway[positionFromRight] + (holeLayout.fairway[width] / 2));
                return idealShot;
            }
            if (isFiveWoodIdeal)
            {
                idealShot.ClubChoice = "Five Wood";
                idealShot.Distance = bagDistance.FiveWood;
                //Middle of fairway
                idealShot.LocationLeft = (holeLayout.fairway[positionFromLeft] + (holeLayout.fairway[width] / 2));
                idealShot.LocationRight = (holeLayout.fairway[positionFromRight] + (holeLayout.fairway[width] / 2));
                return idealShot;
            }
            if (isHybridIdeal)
            {
                idealShot.ClubChoice = "Hybrid";
                idealShot.Distance = bagDistance.Hybrid;
                //Middle of fairway
                idealShot.LocationLeft = (holeLayout.fairway[positionFromLeft] + (holeLayout.fairway[width] / 2));
                idealShot.LocationRight = (holeLayout.fairway[positionFromRight] + (holeLayout.fairway[width] / 2));
                return idealShot;
            }
            if (isIronIdeal)
            {
                idealShot.ClubChoice = "Driving Iron";
                idealShot.Distance = bagDistance.DrivingIron;
                //Middle of fairway
                idealShot.LocationLeft = (holeLayout.fairway[positionFromLeft] + (holeLayout.fairway[width] / 2));
                idealShot.LocationRight = (holeLayout.fairway[positionFromRight] + (holeLayout.fairway[width] / 2));
                return idealShot;
            }
            return null;
        }
    }
}
