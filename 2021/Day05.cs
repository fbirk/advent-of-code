using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    internal class Day05
    {
        static readonly int fieldsize = 1000;

        static int Part1(string filename)
        {
            // init
            var matrix = new int[fieldsize][];
            for (int i = 0; i < fieldsize; i++)
            {
                matrix[i] = new int[fieldsize];
                for (int j = 0; j < fieldsize; j++)
                {
                    matrix[i][j] = 0;
                }
            }

            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var elements = line.Split(' ');
                var start = elements.First().Split(',');
                var end = elements.Last().Split(',');

                int x1 = int.Parse(start.First());
                int y1 = int.Parse(start.Last());
                int x2 = int.Parse(end.First());
                int y2 = int.Parse(end.Last());
                if (x1 == x2 || y1 == y2)
                {
                    FillMatrix(matrix, Math.Min(x1,x2), Math.Min(y1,y2), Math.Max(x1,x2), Math.Max(y1,y2));
                } else
                {
                    if((x1 < x2 && y1 < y2) || (x1 > x2 && y1 > y2))
                    {
                        FillMatrixDiagonalNWtoSE(matrix, Math.Min(x1, x2), Math.Min(y1, y2), Math.Max(x1, x2), Math.Max(y1, y2));
                    }
                    else if ((x1 < x2 && y1 > y2) || (x1 > x2 && y1 < y2))
                    {
                        FillMatrixDiagonalSWtoNE(matrix, Math.Min(x1, x2), Math.Max(y1, y2), Math.Max(x1, x2), Math.Min(y1, y2));
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return CountOverlap(matrix);
        }

        private static void FillMatrixDiagonalSWtoNE(int[][] matrix, int x1, int y1, int x2, int y2)
        {
            int i = y1;
            for (int j = x1; j <= x2 || i >= y2; j++, i--)
            {
                matrix[i][j] += 1;
            }
        }

        private static void FillMatrixDiagonalNWtoSE(int[][] matrix, int x1, int y1, int x2, int y2)
        {
            int i = y1;
            for (int j = x1; j <= x2 || i <= y2; i++, j++)
            {
                matrix[i][j] += 1;
            }
        }

        static void FillMatrix(int[][] matrix, int x1, int y1, int x2, int y2)
        {
            for (int i = y1; i <= y2; i++)
            {
                for (int j = x1; j <= x2; j++)
                {
                    matrix[i][j] += 1;
                }
            }
        }

        static int CountOverlap(int[][] matrix)
        {
            var result = 0;
            for (int i = 0; i < fieldsize; i++)
            {
                for (int j = 0; j < fieldsize; j++)
                {
                    if (matrix[i][j] > 1)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        //static void Main(string[] args)
        //{
        //    Debug.WriteLine($"Part 2: {Part1("Data05.txt")}");
        //}
    }
}
