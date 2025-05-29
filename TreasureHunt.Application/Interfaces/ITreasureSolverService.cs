using TreasureHunt.Application.Models;
namespace TreasureHunt.Application.Interfaces;


public interface ITreasureSolverService
{
    double Solve(TreasureInput input);
}