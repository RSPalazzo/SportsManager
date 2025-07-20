using Golf.Simulator.App;
using Golf.Simulator.App.Interfaces;
using Golf.Simulator.App.ObjectCreates;
using Golf.Simulator.App.ObjectLoads;
using Golf.Simulator.App.Workers;

class Program
{
    static void Main(string[] args)
    {
        // Initialize necessary objects
        // Note: You need to provide valid implementations for MatchCreate, RoundWorker, TeamLoad, and IMatchWorker
        MatchCreate matchCreate = new MatchCreate(); // You must assign a valid implementation here
        GolfRoundCreate golfRoundCreate = new GolfRoundCreate(); // You must assign a valid implementation here
        PlayerOverallSkill playerOverallSkill = new PlayerOverallSkill(); // You must assign a valid implementation here
        CourseLoad courseLoad = new CourseLoad(); // You must assign a valid implementation here
        PlayerLoad playerLoad = new PlayerLoad(); // You must assign a valid implementation here
        TeamLoad teamLoad = new TeamLoad(); // You must assign a valid implementation here
        IRoundWorker golfRound = new RoundWorker(golfRoundCreate, playerLoad, courseLoad, playerOverallSkill); // Use a concrete implementation instead of the interface
        IMatchWorker matchWorker = new MatchWorker(matchCreate, golfRound, teamLoad); // You must assign a valid implementation here
        var _golfMatchSimulator = new GolfMatchSimulator(matchWorker);

        _golfMatchSimulator.Run();
    }
}