namespace TreasureHunt.Application.Models;

public class TreasureInput
{
    public int N { get; set; }
    public int M { get; set; }
    public int P { get; set; }
    public required int[][] Matrix { get; set; }
}