using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// namespace TreasureHunt.Infrastructure.Data.Entities;
namespace TreasureHunt.Application.Models;

public class MapCell
{
    [Key]
    public int CellId { get; set; }

    [ForeignKey("TreasureMap")]
    public int MapId { get; set; }

    [Range(0, 499)]
    public int RowIndex { get; set; }

    [Range(0, 499)]
    public int ColIndex { get; set; }

    [Range(0, int.MaxValue)]
    public int Value { get; set; }
}
// public TreasureMap TreasureMap { get; set; } = null!;