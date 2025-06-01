
using Microsoft.EntityFrameworkCore;
using TreasureHunt.Application.Models;
using TreasureHunt.Infrastructure.Context;
// using TreasureHunt.Infrastructure.Data.Entities;
using TreasureHunt.Infrastructure.Interfaces;

public class TreasureMapService : ITreasureMapService
{
    private readonly TreasureDbContext _context;

    public TreasureMapService(TreasureDbContext context)
    {
        _context = context;
    }

    public async Task<List<TreasureMap>> GetAllMapsAsync()
    {
        var maps = await _context.TreasureMaps
            .Include(m => m.Cells)
            .ToListAsync();

        return maps.Select(m => new TreasureMap
        {
            MapId = m.MapId,
            Name = m.Name,
            Rows = m.Rows,
            Columns = m.Columns,
            TreasureValue = m.TreasureValue,
            Cells = m.Cells.Select(c => new MapCell
            {
                MapId = c.MapId,
                CellId = c.CellId,
                RowIndex = c.RowIndex,
                ColIndex = c.ColIndex,
                Value = c.Value
            }).ToList()
        }).OrderByDescending(m => m.MapId).ToList();
    }

    public async Task<TreasureMap> CreateMapAsync(TreasureInput input)
    {
        var treasureMap = new TreasureMap
        {
            Name = input.Name ?? DateTime.Now.ToString("yyyyMMddHHmmss"),
            Rows = input.N,
            Columns = input.M,
            TreasureValue = input.P,
            Cells = new List<MapCell>()
        };

        for (int i = 0; i < input.N; i++)
        {
            for (int j = 0; j < input.M; j++)
            {
                treasureMap.Cells.Add(new MapCell
                {
                    // MapId = treasureMap.MapId,
                    RowIndex = i,
                    ColIndex = j,
                    Value = input.Matrix[i][j]
                });
            }
        }

        _context.TreasureMaps.Add(treasureMap);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? treasureMap : null;
    }

    public async Task<TreasureMap?> GetMapByIdAsync(int mapId)
    {
        var map = await _context.TreasureMaps.Include(m => m.Cells).FirstOrDefaultAsync(m => m.MapId == mapId);

        if (map == null)
        {
            return null;
        }


        return new TreasureMap
        {
            MapId = map.MapId,
            Name = map.Name,
            Rows = map.Rows,
            Columns = map.Columns,
            TreasureValue = map.TreasureValue,
            Cells = map.Cells.Select(c => new MapCell
            {
                MapId = c.MapId,
                CellId = c.CellId,
                RowIndex = c.RowIndex,
                ColIndex = c.ColIndex,
                Value = c.Value
            }).ToList()
        };
    }
    public async Task<Solution?> AddSolutionByMapIdAsync(Solution solution)
    {
        var map = await _context.TreasureMaps.FindAsync(solution.MapId);
        if (map == null)
        {
            return null;
        }

        solution.MapId = solution.MapId;
        _context.Solutions.Add(solution);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? solution : null;
    }
}