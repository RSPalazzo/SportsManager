using Golf.Simulator.App.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Golf.Simulator.App.Workers
{
    public class ShotExecutions
    {
        public Vector2 ExecuteShot(Player player, Course course, Shot shot, CurrentBallPosition currentBall)//Wind wind, Lie lie, int altitude, int temp, Rain rain, )
        {
            if (shot.ClubChoice == "Putter")
            {
                // Handle putting logic here
                var putt = GetPutt(player, course, currentBall);
                // For now, we will just return the ball position as is
                return putt;
            }
            else
            {
                var swingSetup = GetSwingSetup(player);
                var swing = GetSwing(swingSetup, player);
                var ballStrike = GetBallStrike(swing, player, shot);
                return ballStrike;
            }
        }
        int GetSwingSetup(Player player)
        {
            // Calculate swing setup based on player attributes
            var swingSetup = player.attributes.mechanics.accuracy + player.attributes.mental.awareness;
            return swingSetup;
        }
        int GetSwing(int swingSetup, Player player)
        {
            // Calculate swing based on swing setup and player attributes
            var swing = swingSetup + player.attributes.mechanics.swing + player.attributes.mechanics.tempo + player.attributes.physical.balance + player.attributes.physical.agility;
            return swing;
        }
        Vector2 GetBallStrike(int swing, Player player, Shot shot)
        {
            var ballStrike = swing + player.attributes.mechanics.ballStriking + player.attributes.equipment.fit + player.attributes.equipment.quality;
            Random random = new Random();
            // Introduce randomness to the ball strike calculation
            var rand = random.Next(1, 100);

            //Perfect Quality Ball Strike
            if (rand == ballStrike)
            {
                //TODO: replace with club based variance 
                rand = random.Next(-5, 5);
                return new Vector2(shot.BallPosition.X + rand, shot.BallPosition.Y + rand); // Add a minimal dispersion to the ball position
            }
            //Good Quality Ball Strike
            else if (rand < ballStrike)
            {
                rand = random.Next(5, 10);
                var coinFlip = random.Next(0, 1);
                if (coinFlip == 1)
                {
                    rand = rand * -1;
                }
                return new Vector2(shot.BallPosition.X + rand, shot.BallPosition.Y + rand); 
            }
            //Moderate Quality Ball Strike
            else if (rand > ballStrike && (rand - ballStrike) <= 10)
            {
                // If the random value is within a certain range, return a slightly altered position
                // TODO: Replace with club based variance * 2
                rand = random.Next(5, 30);
                var coinFlip = random.Next(0, 1);
                if (coinFlip == 1)
                {
                    rand = rand * -1;
                }
                return new Vector2(shot.BallPosition.X + rand, shot.BallPosition.Y + rand); // Add a moderate dispersion to the ball position
            }
            //Poor Quality Ball Strike
            else if (rand > ballStrike && (rand - ballStrike) <= 20)
            {
                // If the random value is within another range, return a more significant alteration
                // TODO: Replace with club based variance * 4
                rand = random.Next(30, 65);
                var coinFlip = random.Next(0, 1);
                if (coinFlip == 1)
                {
                    rand = rand * -1;
                }
                return new Vector2(shot.BallPosition.X + rand, shot.BallPosition.Y + rand); // Add a significant dispersion to the ball position
            }
            else
            {
                //Horrible Quality Ball Strike
                // Otherwise, return the calculated ball strike value
                //TODO: Replace with club based variance * 10
                rand = random.Next(66, 100);
                var coinFlip = random.Next(0, 1);
                if (coinFlip == 1)
                {
                    rand = rand * -1;
                }
                return new Vector2(shot.BallPosition.X + rand, shot.BallPosition.Y + rand);
            }
        }
        Vector2 GetPutt(Player player, Course course, CurrentBallPosition currentBall)
        {
            var green = course.holes[currentBall.holeNumber].green; // Adjust for zero-based index
            int difficulty = green.complex + green.stimp + green.firmness;
            double lengthOfPuttYards = Vector2.Distance(currentBall.ballPosition, green.pin);
            double lengthOfPuttFeet = lengthOfPuttYards * 3;
            currentBall.puttLength = Convert.ToInt16(lengthOfPuttFeet); // Convert to feet
            double puttPercent = lengthOfPuttFeet + (difficulty/15);
            //int puttPercent = (int)(100 - (lengthOfPutt / difficulty * 100)); // Simplified putt percentage calculation
            double a = -2.495;
            double b = 0.210;
            double makeProbability = (int)Math.Ceiling(100 * (1.0 / (1.0 + Math.Exp(a + b * puttPercent))));
            currentBall.puttMakePercentage = Convert.ToInt16(makeProbability);
            //var chanceToMakePutt = Convert.ToInt16(makeProbability + (difficulty / 15 * -1));

            Random random = new Random();
            int perCent = random.Next(1, 100);
            if (perCent <= makeProbability)
            {
                // Putt is successful
                return green.pin; // Return the pin position as the successful putt position
            }
            else if (perCent > makeProbability && perCent <= makeProbability + 40 && perCent < 90)
            {
                var minMiss = Convert.ToInt16(lengthOfPuttYards / 4);
                var miss = random.Next(-minMiss, minMiss);
                // Putt is close but missed, return a position near the pin
                var missedPosition = new Vector2(green.pin.X + (miss/2), green.pin.Y + (miss/2));
                return missedPosition; // Return a position near the pin if the putt is missed
            }
            else
            {
                // Putt is missed significantly, return a position further away
                int shortOrLong = random.Next(0, 1);
                var minMiss = Convert.ToInt16(lengthOfPuttYards / 4);
                var maxMiss = Convert.ToInt16(lengthOfPuttYards / 2);
                if (shortOrLong == 0)
                {

                    // Putt is long
                    var miss = random.Next(minMiss, maxMiss);
                    var missedPosition = new Vector2(green.pin.X - (miss/2), green.pin.Y - (miss/2));
                    return missedPosition; // Return a position further away if the putt is missed significantly
                }
                else
                {
                    // Putt is short
                    var miss = random.Next(minMiss, maxMiss);
                    var missedPosition = new Vector2(green.pin.X - (miss / 2), green.pin.Y - (miss / 2));
                    return missedPosition; // Return a position further away if the putt is missed significantly
                }    
            }   
        }
    }
}
