using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day09
    {
        private HashSet<ValueTuple<int, int>> indexHash = new HashSet<ValueTuple<int, int>>();

        public static int Part1(string filename, int rows, int columns)
        {
            // init
            var matrix = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new int[columns];
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = 0;
                }
            }

            var rowcounter = 0;
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var input = line.Select(c => c - '0').ToArray();

                for (int i = 0; i < columns; i++)
                {
                    matrix[rowcounter][i] = input[i];
                }
                rowcounter++;
            }

            var result = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    var output = HasMinimumAt(matrix, i, j);
                    if (output != -1)
                    {
                        result += (output + 1);
                    }
                }
            }

            return result;
        }

        private static int HasMinimumAt(int[][] matrix, int i, int j)
        {
            var debugOutput = "";
            var value = matrix[i][j];

            if (i - 1 >= 0)
            {
                if (matrix[i - 1][j] <= value)
                {
                    return -1;
                }
            }

            if (j - 1 >= 0)
            {
                if (matrix[i][j - 1] <= value)
                {
                    return -1;
                }
            }

            if (i + 1 < matrix.Length)
            {
                if (matrix[i + 1][j] <= value)
                {
                    return -1;
                }
            }

            if (j + 1 < matrix[i].Length)
            {
                if (matrix[i][j + 1] <= value)
                {
                    return -1;
                }
            }

            return value;
        }

        public int Part2(string filename, int rows, int columns)
        {
            // init
            var matrix = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new int[columns];
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = 0;
                }
            }

            var rowcounter = 0;
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var input = line.Select(c => c - '0').ToArray();

                for (int i = 0; i < columns; i++)
                {
                    matrix[rowcounter][i] = input[i];
                }
                rowcounter++;
            }

            var result = new List<int>();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    var output = HasMinimumAt(matrix, i, j);
                    if (output != -1)
                    {
                        result.Add(FindBassin(matrix, i, j, i, j) + 1);
                    }
                }
            }

            result.Sort();
            return result.GetRange(result.Count - 3, result.Count - 1).Aggregate((total, next) => total * next);
        }

        private int FindBassin(int[][] matrix, int i, int j, int initialI, int initialJ)
        {
            var result = 0;

            if ((i - 1 >= 0) && i - 1 != initialI)
            {
                if (matrix[i - 1][j] != 9 && matrix[i - 1][j] > matrix[i][j])
                {
                    if (!indexHash.Contains((i - 1, j)))
                    {
                        result += FindBassin(matrix, i - 1, j, i, j) + 1;
                        indexHash.Add((i - 1, j));
                    }
                }
            }

            if (((i + 1) < matrix.Length) && i + 1 != initialI)
            {
                if (matrix[i + 1][j] != 9 && matrix[i + 1][j] > matrix[i][j])
                {
                    if (!indexHash.Contains((i + 1, j)))
                    {
                        result += FindBassin(matrix, i + 1, j, i, j) + 1;
                        indexHash.Add((i + 1, j));
                    }
                }
            }

            if (((j - 1) >= 0) && j - 1 != initialJ)
            {
                if (matrix[i][j - 1] != 9 && matrix[i][j - 1] > matrix[i][j])
                {
                    if (!indexHash.Contains((i, j - 1)))
                    {
                        result += FindBassin(matrix, i, j - 1, i, j) + 1;
                        indexHash.Add((i, j - 1));
                    }
                }
            }

            if (((j + 1) < matrix[i].Length) && j + 1 != initialJ)
            {
                if (matrix[i][j + 1] != 9 && matrix[i][j + 1] > matrix[i][j])
                {
                    if (!indexHash.Contains((i, j + 1)))
                    {
                        result += FindBassin(matrix, i, j + 1, i, j) + 1;
                        indexHash.Add((i, j + 1));
                    }
                }
            }

            return result;
        }
    }
}

