using TreasureHunt.Application.Models;
namespace TreasureHunt.Application.Interfaces;


public interface ITreasureSolverService
{
    TreasureInput CreateTreasureInputFromMap(TreasureMap map);
    Solution SolveTreasureMap(TreasureMap map);
    List<MapCell> FindShortestPath(TreasureInput map);
    double CalculateTotalFuel(List<MapCell> path);
    double Distance(int x1, int y1, int x2, int y2);
}