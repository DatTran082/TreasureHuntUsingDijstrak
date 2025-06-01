// namespace TreasureHunt.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasureHunt.Application.Models;

public class Solution
{
    [Key]
    public int SolutionId { get; set; }

    [ForeignKey("TreasureMap")]
    public int MapId { get; set; }
    public double Fuel { get; set; }
    public List<MapCell> Path { get; set; } = new List<MapCell>();
    public DateTime SolvedAt { get; set; } = DateTime.Now;
}