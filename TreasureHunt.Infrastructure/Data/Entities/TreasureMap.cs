// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace TreasureHunt.Infrastructure.Data.Entities
// {

//     [Table("TreasureMaps")]
//     public class TreasureMap
//     {
//         [Key]
//         public int MapId { get; set; }


//         [MaxLength(100)]
//         public string Name { get; set; } = string.Empty;

//         [Required]
//         [Range(1, 500)]
//         public int Rows { get; set; }

//         [Required]
//         [Range(1, 500)]
//         public int Columns { get; set; }

//         [Required]
//         [Range(1, int.MaxValue)]
//         public int MaxLevel { get; set; }

//         [Required]
//         public DateTime CreatedAt { get; set; } = DateTime.Now;

//         public List<MapCell> Cells { get; set; } = new();
//         public List<Solution> Solutions { get; set; } = new();
//     }
// }