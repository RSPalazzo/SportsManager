using Golf.Simulator.App.Interfaces;
using Golf.Simulator.App.Models;
using Golf.Simulator.App.ObjectCreates;
using Golf.Simulator.App.ObjectLoads;
using System.Text;

namespace Golf.Simulator.App.Workers
{
    public class RoundWorker : IRoundWorker
    {
        private GolfRoundCreate _golfRound;
        private readonly PlayerLoad _player;
        private readonly CourseLoad _golfCourse;
        private readonly PlayerOverallSkill _overallSkill;
        private readonly IShotDecision _shotDecision;
        private readonly BagCreate _bag;
        private readonly ShotExecutions _shotExecutions;

        //Result resultJson = new Result();
        public RoundWorker(GolfRoundCreate golfRound, PlayerLoad player, CourseLoad golfCourse, PlayerOverallSkill playerOverallSkill, IShotDecision shotDecision, BagCreate bag, ShotExecutions shotExecutions)
        {
            _golfRound = golfRound;
            _player = player;
            _golfCourse = golfCourse;
            _overallSkill = playerOverallSkill;
            _shotDecision = shotDecision;
            _bag = bag;
            _shotExecutions = shotExecutions;
        }
        public GolfRound PlayGolfRound(int courseId, int playerId, int roundId)
        {
            var _round = _golfRound.CreateGolfRound(courseId, playerId, roundId);
            //resultJson.SetResult(play.player.playerFullName, roundTotal);              
            var round = PlayRound(_round);
            return round;
        }
        GolfRound PlayRound(GolfRound golfRound)
        {
            var player = _player.GetPlayer(golfRound.PlayerId);
            var bag = _bag.CreateBagDistance(player);
            var course = _golfCourse.GetGolfCourse(golfRound.CourseId);
            //Start on first tee
            CurrentBallPosition currentBall = new CurrentBallPosition
            {
                lie = "Tee",
                ballPosition = new System.Numerics.Vector2(0, 0),
                holeNumber = 0
            };
            //var skill = _overallSkill.getPlayerSkill(player);
            int puttsPerRound = 0;
            int roundScore = 0;
            int holeScore = 0;
            int holeNumber = 1;
            bool ballIsHoled = false;
            bool isRoundOver = false;
            while (isRoundOver == false)
            {
                isRoundOver = getIsRoundOver(currentBall.holeNumber, ballIsHoled);

                if (isRoundOver == false && ballIsHoled == false)
                {
                    var shot = _shotDecision.Decision(player, course, bag, currentBall);
                    var executedShot = _shotExecutions.ExecuteShot(player, course, shot, currentBall);
                    currentBall.ballPosition = executedShot;
                    /////////////////////Logging/////////////////////////
                    if (shot.ClubChoice == "Putter")
                    {
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine(player.playerFullName + " steps up to his " + (holeScore + 1) + " shot on hole number " + holeNumber);
                        Console.WriteLine($"He has a {currentBall.puttLength} foot putt to the hole. He has a {currentBall.puttMakePercentage} chance of making this putt");
                        Console.WriteLine("End Shot");
                        puttsPerRound++;
                        Console.WriteLine("---------------------------------------");
                        /////////////////////Logging////////////////////////////////
                    }
                    else
                    {
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine(player.playerFullName + " steps up to his " + (holeScore + 1) + " shot on hole number " + holeNumber);
                        Console.WriteLine("This is a " + course.holes[currentBall.holeNumber].holeYardage + " yard Par: " + course.holes[currentBall.holeNumber].holePar);
                        Console.WriteLine($"{player.playerFirstName} is on the {currentBall.lie} with the {shot.ClubChoice}");
                        //////////////////////Logging////////////////////////////////
                        Console.WriteLine($"Ball went: {Math.Abs(executedShot.Y - shot.BallPosition.Y)} yards from its intended distance and {Math.Abs(executedShot.X - shot.BallPosition.X)} yards offline");
                        Console.WriteLine("End Shot");
                        Console.WriteLine("---------------------------------------");
                        /////////////////////Logging////////////////////////////////
                    }

                    holeScore++;

                    ///                
                    /// 
                    //
                    /*Console.WriteLine("Putt is " + distanceToHole + " feet");
                    Console.WriteLine("The Percent to hit the shot was " + sim.shotPercentage);
                    Console.WriteLine("Was the shot good: " + shotSim + " The random number generated was " + sim.perCent);
                    Console.WriteLine("Putt Difficulty " + puttDifficulty + ", Player Skill " + playerPuttingSkill + ", Putt Grade " + putt.puttGrade);
                    Console.WriteLine("Distance left " + distanceToHole + "feet");
                    */

                    var pinPosition = course.holes[currentBall.holeNumber].green.pin;
                    if (executedShot == pinPosition)
                    {
                        ballIsHoled = true;
                        //generateIsBallInHole(golfRound, course, player, currentBall.holeNumber,distanceToHole, holeScore, puttsPerRound);
                    }
                }
                //If Ball is in the hole go to next hole
                else if (isRoundOver == false && ballIsHoled == true)
                {
                    // Add hole score to round list
                    golfRound.HoleScores.Add(holeScore);
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Hole Score is " + holeScore);
                    Console.WriteLine("---------------------------------------");
                    roundScore = roundScore + holeScore;
                    holeScore = 0;
                    currentBall.holeNumber++;
                    holeNumber++;
                    currentBall.ballPosition = new System.Numerics.Vector2(0, 0);
                    currentBall.lie = "Tee";
                    currentBall.puttLength = 0;
                    currentBall.puttMakePercentage = 0;
                    ballIsHoled = false;
                }
                //If round is over log final score
                else
                {
                    roundScore = roundScore + holeScore;
                    golfRound.PlayerScore = roundScore;
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Round Score: " + roundScore);
                    Console.WriteLine("Putts Per Round " + puttsPerRound);
                    Console.WriteLine("---------------------------------------");
                }
            }
            // Set the final round status
            golfRound.RoundStatus = "Completed";
            return golfRound;
        }
        bool getIsRoundOver(int holeNumber, bool ballIsHoled)
        {
            bool roundOver = false;
            if (holeNumber == 17 && ballIsHoled == true)
            {
                roundOver = true;
            }
            return roundOver;
        }
        public void ResetRound(GolfRound golfRound)
        {
            // Reset the round properties to their initial state
            golfRound.PlayerScore = 0;
            golfRound.HoleScores.Clear();
            golfRound.RoundStatus = "Not Started";
            // Add any other properties that need to be reset
        }
    }
}