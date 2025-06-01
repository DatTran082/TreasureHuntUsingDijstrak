namespace TreasureHunt.Application.Services;

using TreasureHunt.Application.Interfaces;
using TreasureHunt.Application.Models;


public class TreasureSolverService : ITreasureSolverService
{
    public TreasureInput CreateTreasureInputFromMap(TreasureMap map)
    {
        var matrix = new int[map.Rows][];
        for (int i = 0; i < map.Rows; i++)
            matrix[i] = new int[map.Columns];

        foreach (var cell in map.Cells)
        {
            matrix[cell.RowIndex][cell.ColIndex] = cell.Value;
        }

        return new TreasureInput
        {
            N = map.Rows,
            M = map.Columns,
            P = map.TreasureValue,
            Matrix = matrix
        };
    }

    public Solution SolveTreasureMap(TreasureMap mapInput)
    {
        TreasureInput treasureInputFromMap = CreateTreasureInputFromMap(mapInput);
        List<MapCell> path = FindShortestPath(treasureInputFromMap);
        double fuel = CalculateTotalFuel(path);

        return new Solution
        {
            MapId = mapInput.MapId,
            Fuel = fuel,
            Path = path,
            SolvedAt = DateTime.Now
        };
    }
    public List<MapCell> FindShortestPath(TreasureInput map)
    {

        int n = map.N, m = map.M, p = map.P;
        int[][] matrix = map.Matrix;

        var positions = new List<(int x, int y)>[p + 1];
        for (int i = 0; i <= p; i++)
        {
            positions[i] = new List<(int x, int y)>();
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                positions[matrix[i][j]].Add((i, j));
            }

        }

        double[,] cost = new double[n, m];
        (int x, int y)[,] prev = new (int, int)[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                cost[i, j] = double.MaxValue;
                prev[i, j] = (-1, -1);
            }
        }

        foreach (var (x, y) in positions[1])
        {
            cost[x, y] = Distance(0, 0, x, y);
            prev[x, y] = (0, 0);
        }

        for (int num = 2; num <= p; num++)
        {
            foreach (var (x2, y2) in positions[num])
            {
                double minCost = double.MaxValue;
                (int x, int y) bestPrev = (-1, -1);
                foreach (var (x1, y1) in positions[num - 1])
                {
                    double dist = Distance(x1, y1, x2, y2);
                    double total = cost[x1, y1] + dist;
                    if (total < minCost)
                    {
                        minCost = total;
                        bestPrev = (x1, y1);
                    }
                }
                cost[x2, y2] = minCost;
                prev[x2, y2] = bestPrev;
            }
        }

        // Tìm điểm kết thúc tối ưu
        double finalCost = double.MaxValue;
        (int x, int y) end = (-1, -1);
        foreach (var (x, y) in positions[p])
        {
            if (cost[x, y] < finalCost)
            {
                finalCost = cost[x, y];
                end = (x, y);
            }
        }

        // Truy ngược đường đi thành List<MapCell>
        var resultPath = new List<MapCell>();
        var current = end;
        while (current != (0, 0))
        {
            resultPath.Add(new MapCell
            {
                // MapId = map.Id,
                RowIndex = current.x,
                ColIndex = current.y,
                Value = matrix[current.x][current.y]
            });
            current = prev[current.x, current.y];
        }

        // Thêm điểm bắt đầu (0,0)
        resultPath.Add(new MapCell
        {
            // MapId = map.Id,
            RowIndex = 0,
            ColIndex = 0,
            Value = matrix[0][0]
        });

        resultPath.Reverse();
        return resultPath;
    }
    public double CalculateTotalFuel(List<MapCell> path)
    {
        double fuel = 0;
        for (int i = 1; i < path.Count; i++)
        {
            var a = path[i - 1];
            var b = path[i];
            fuel += Distance(a.RowIndex, a.ColIndex, b.RowIndex, b.ColIndex);
        }
        return fuel;
    }
    public double Distance(int x1, int y1, int x2, int y2)
    {
        return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    }

}