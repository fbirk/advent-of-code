using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024._10
{
    public class Day10
    {
        public static long Part1()
        {
            long result = 0;
            var lines = File.ReadAllLines(@"10\data10.txt");
            var matrix = new int[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                matrix[i] = line.ToArray().Select(x => int.Parse(x.ToString())).ToArray();
            }


            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        var trailheads = new List<(int, int)>();
                        var score = MakeAStep(matrix, i, j, -1, 0, trailheads);
                        result += score;
                    }
                }
            }

            return result;
        }

        private static int MakeAStep(int[][] matrix, int i, int j, int previous, int depth, List<(int, int)> trailheads)
        {


            if (i < 0
                || j < 0
                || i >= matrix.Length
                || j >= matrix[i].Length)
            {
                return 0;
            }

            if (matrix[i][j] <= previous
                || Math.Abs(matrix[i][j] - previous) != 1)
            {
                return 0;
            }

            if (matrix[i][j] == 9 && depth == 9 && !trailheads.Contains((i, j)))
            {
                trailheads.Add((i, j));
                return 1;
            }

            var prev = matrix[i][j];
            var nextDepth = depth + 1;
            return 0
                + MakeAStep(matrix, i - 1, j, prev, nextDepth, trailheads)
                + MakeAStep(matrix, i + 1, j, prev, nextDepth, trailheads)
                + MakeAStep(matrix, i, j - 1, prev, nextDepth, trailheads)
                + MakeAStep(matrix, i, j + 1, prev, nextDepth, trailheads);
        }
    }
}
