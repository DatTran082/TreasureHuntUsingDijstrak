using TreasureHunt.Application.Models;
// using TreasureHunt.Infrastructure.Data.Entities;
namespace TreasureHunt.Infrastructure.Interfaces;

public interface ITreasureMapService
{
    Task<TreasureMap?> GetMapByIdAsync(int mapId);
    Task<List<TreasureMap>> GetAllMapsAsync();
    Task<Solution?> AddSolutionByMapIdAsync(Solution solution);
    Task<TreasureMap> CreateMapAsync(TreasureInput input);
}