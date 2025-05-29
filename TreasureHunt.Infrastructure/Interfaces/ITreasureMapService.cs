using TreasureHunt.Application.Models;
using TreasureHunt.Infrastructure.Data.Entities;
namespace TreasureHunt.Infrastructure.Interfaces;

public interface ITreasureMapService
{
    Task<List<TreasureMap>> GetAllMapsAsync();
    Task<TreasureMap> CreateMapAsync(TreasureInput input);
}