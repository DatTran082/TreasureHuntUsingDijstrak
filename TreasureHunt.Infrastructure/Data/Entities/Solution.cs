namespace TreasureHunt.Infrastructure.Data.Entities;

public class Solution
{
    public int SolutionId { get; set; }

    public int MapId { get; set; }

    public double FuelUsed { get; set; }
    public DateTime SolvedAt { get; set; } = DateTime.Now;
    // public TreasureMap TreasureMap { get; set; } = null!;
}