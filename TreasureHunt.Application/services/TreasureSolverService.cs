namespace TreasureHunt.Application.Services;

using TreasureHunt.Application.Interfaces;
using TreasureHunt.Application.Models;

public class TreasureSolverService : ITreasureSolverService
{
    public double Solve(TreasureInput input)
    {
        int rows = input.N;
        int cols = input.M;
        int maxLevel = input.P;
        var matrix = input.Matrix;

        // Vị trí của các ô theo từng cấp độ từ 0 đến maxLevel
        var levelPositions = new List<(int row, int col)>[maxLevel + 1];
        for (int level = 0; level <= maxLevel; level++)
            levelPositions[level] = new();

        // Lưu lại tất cả vị trí của từng cấp độ
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int level = matrix[row][col];
                levelPositions[level].Add((row, col));
            }
        }

        // Mảng lưu khoảng cách ngắn nhất từ (0,0) đến từng ô
        var minDistance = new double[rows, cols];
        for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
                minDistance[row, col] = double.PositiveInfinity;

        minDistance[0, 0] = 0; // Bắt đầu từ (0,0)

        // Duyệt qua từng cấp độ để tính khoảng cách tối thiểu
        for (int currentLevel = 1; currentLevel <= maxLevel; currentLevel++)
        {
            foreach (var (prevRow, prevCol) in levelPositions[currentLevel - 1])
            {
                foreach (var (currRow, currCol) in levelPositions[currentLevel])
                {
                    double distance = Math.Sqrt(Math.Pow(prevRow - currRow, 2) + Math.Pow(prevCol - currCol, 2));
                    minDistance[currRow, currCol] = Math.Min(
                        minDistance[currRow, currCol],
                        minDistance[prevRow, prevCol] + distance
                    );
                }
            }
        }

        // Lấy ô đầu tiên có cấp độ cao nhất (p)
        var (finalRow, finalCol) = levelPositions[maxLevel][0];
        return minDistance[finalRow, finalCol];
    }
}