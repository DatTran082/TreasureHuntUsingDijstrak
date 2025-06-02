namespace TreasureHunt.Application.Services;

using System.Text.Json;
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
    // public List<MapCell> FindShortestPathBackup(TreasureInput map)
    // {

    //     try
    //     {
    //         int n = map.N, m = map.M, p = map.P;
    //         int[][] matrix = map.Matrix;

    //         var positions = new List<(int x, int y)>[p + 1];
    //         for (int i = 0; i <= p; i++)
    //         {
    //             positions[i] = new List<(int x, int y)>();
    //         }

    //         // for (int i = 0; i < n; i++)
    //         // {
    //         //     for (int j = 0; j < m; j++)
    //         //     {
    //         //         positions[matrix[i][j]].Add((i, j));
    //         //     }

    //         // }

    //         for (int i = 0; i < n; i++)
    //         {
    //             for (int j = 0; j < m; j++)
    //             {
    //                 int level = matrix[i][j];
    //                 if (level >= 0 && level <= p)
    //                 {
    //                     positions[level].Add((i, j));
    //                 }
    //             }
    //         }

    //         double[,] cost = new double[n, m];
    //         (int x, int y)[,] prev = new (int, int)[n, m];
    //         for (int i = 0; i < n; i++)
    //         {
    //             for (int j = 0; j < m; j++)
    //             {
    //                 cost[i, j] = double.MaxValue;
    //                 prev[i, j] = (-1, -1);
    //             }
    //         }

    //         foreach (var (x, y) in positions[1])
    //         {
    //             cost[x, y] = Distance(0, 0, x, y);
    //             prev[x, y] = (0, 0);
    //         }

    //         for (int num = 2; num <= p; num++)
    //         {
    //             foreach (var (x2, y2) in positions[num])
    //             {
    //                 double minCost = double.MaxValue;
    //                 (int x, int y) bestPrev = (-1, -1);
    //                 foreach (var (x1, y1) in positions[num - 1])
    //                 {
    //                     double dist = Distance(x1, y1, x2, y2);
    //                     double total = cost[x1, y1] + dist;
    //                     if (total < minCost)
    //                     {
    //                         minCost = total;
    //                         bestPrev = (x1, y1);
    //                     }
    //                 }
    //                 cost[x2, y2] = minCost;
    //                 prev[x2, y2] = bestPrev;
    //             }
    //         }

    //         // Tìm điểm kết thúc tối ưu
    //         double finalCost = double.MaxValue;
    //         (int x, int y) end = (-1, -1);
    //         foreach (var (x, y) in positions[p])
    //         {
    //             if (cost[x, y] < finalCost)
    //             {
    //                 finalCost = cost[x, y];
    //                 end = (x, y);
    //             }
    //         }

    //         // Truy ngược đường đi thành List<MapCell>
    //         var resultPath = new List<MapCell>();
    //         var current = end;

    //         while (current != (0, 0))
    //         {
    //             resultPath.Add(new MapCell
    //             {
    //                 // MapId = map.Id,
    //                 RowIndex = current.x,
    //                 ColIndex = current.y,
    //                 Value = matrix[current.x][current.y]
    //             });
    //             current = prev[current.x, current.y];
    //         }

    //         // Thêm điểm bắt đầu (0,0)
    //         resultPath.Add(new MapCell
    //         {
    //             // MapId = map.Id,
    //             RowIndex = 0,
    //             ColIndex = 0,
    //             Value = matrix[0][0]
    //         });

    //         resultPath.Reverse();
    //         return resultPath;


    //         // while (current != (0, 0) && current.x != -1 && current.y != -1)
    //         // {
    //         //     resultPath.Add(new MapCell
    //         //     {
    //         //         RowIndex = current.x,
    //         //         ColIndex = current.y,
    //         //         Value = matrix[current.x][current.y]
    //         //     });
    //         //     current = prev[current.x, current.y];
    //         // }

    //         // // chi add path neu tim thay duong di
    //         // if (current == (0, 0))
    //         // {
    //         //     resultPath.Add(new MapCell
    //         //     {
    //         //         RowIndex = 0,
    //         //         ColIndex = 0,
    //         //         Value = matrix[0][0]
    //         //     });
    //         //     resultPath.Reverse();
    //         //     return resultPath;
    //         // }
    //         // else
    //         // {
    //         //     return new List<MapCell>();
    //         // }
    //     }
    //     catch (Exception ex)
    //     {
    //         // Console.WriteLine($"FindShortestPath: {JsonSerializer.Serialize(ex)}");
    //         Console.WriteLine($"Error in FindShortestPath:{ex.Message}: {ex.StackTrace}");
    //         return new List<MapCell>();
    //     }
    // }

    public List<MapCell> FindShortestPath(TreasureInput map)
    {
        int n = map.N;
        int m = map.M;
        int p = map.P;
        int[][] matrix = map.Matrix;

        // Khởi tạo danh sách vị trí của từng giá trị 1 → p
        var positions = new List<(int row, int col)>[p + 1];
        for (int i = 0; i <= p; i++)
            positions[i] = new List<(int, int)>();

        // Thu thập vị trí các giá trị từ 1 → p (bỏ qua 0 và > p)
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int value = matrix[i][j];
                if (value >= 1 && value <= p)
                {
                    positions[value].Add((i, j));
                }
            }
        }

        // Validate: Phải có đủ các giá trị 1 đến p
        for (int i = 1; i <= p; i++)
        {
            if (positions[i].Count == 0)
            {
                throw new Exception($"Missing required number {i} in map.");
            }
        }

        // Khởi tạo ma trận chi phí và điểm trước đó
        double[,] cost = new double[n, m];
        (int x, int y)[,] prev = new (int, int)[n, m];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                cost[i, j] = double.MaxValue;
                prev[i, j] = (-1, -1);
            }

        // Đi từ (0,0) đến tất cả ô chứa rương số 1
        foreach (var (x, y) in positions[1])
        {
            cost[x, y] = Distance(0, 0, x, y);
            prev[x, y] = (0, 0);
        }

        // Lặp từ rương số 2 đến p để cập nhật chi phí ngắn nhất
        for (int num = 2; num <= p; num++)
        {
            foreach (var (x2, y2) in positions[num])
            {
                foreach (var (x1, y1) in positions[num - 1])
                {
                    double fuel = Distance(x1, y1, x2, y2);
                    double total = cost[x1, y1] + fuel;
                    if (total < cost[x2, y2])
                    {
                        cost[x2, y2] = total;
                        prev[x2, y2] = (x1, y1);
                    }
                }
            }
        }

        // Tìm điểm kết thúc gaanf nhất ở rương số p
        double minFuel = double.MaxValue;
        (int x, int y) end = (-1, -1);
        foreach (var (x, y) in positions[p])
        {
            if (cost[x, y] < minFuel)
            {
                minFuel = cost[x, y];
                end = (x, y);
            }
        }

        if (end == (-1, -1))
        {
            throw new Exception("Khong co path dan den treasure");
        }

        // Truy vết đường đi từ rương p về 0,0
        var path = new List<MapCell>();
        var current = end;
        while (current != (0, 0))
        {
            path.Add(new MapCell
            {
                // MapId = map.Id,
                RowIndex = current.x,
                ColIndex = current.y,
                Value = matrix[current.x][current.y]
            });
            current = prev[current.x, current.y];
        }

        // Thêm ô xuất phát (0,0)
        path.Add(new MapCell
        {
            // MapId = map.Id,
            RowIndex = 0,
            ColIndex = 0,
            Value = matrix[0][0]
        });

        path.Reverse();
        return path;
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