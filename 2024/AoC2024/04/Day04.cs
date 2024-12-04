namespace AoC2024._04
{
    public class Day04
    {
        public static int Part1()
        {
            var result = 0;
            var lines = File.ReadAllLines(@"04\data04.txt");
            var matrix = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                matrix[i] = line.ToCharArray();
            }

            result += GetHorizontalFindings(matrix);
            result += GetVerticalFindings(matrix);
            result += GetDiagonalFindings(matrix);

            return result;
        }

        private static int GetDiagonalFindings(char[][] matrix)
        {
            return GetDiagonalFindingsLeft(matrix) + GetDiagonalFindingsRight(matrix);
        }

        private static int GetDiagonalFindingsRight(char[][] matrix)
        {
            var count = 0;

            for (int j = 0; j < matrix[0].Length; j++)
            {
                // SE to NW
                for (int i = matrix.Length - 1; i >= 0; i--)
                {
                    if (i >= 3 && j < matrix[i].Length - 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i - 1][j + 1], matrix[i - 2][j + 2], matrix[i - 3][j + 3]))
                        {
                            count++;
                        }
                    }
                }
            }

            for (int j = matrix[0].Length - 1; j >= 0; j--)
            {
                // SW to NE
                for (int i = 0; i < matrix.Length; i++)
                {
                    if (i < matrix.Length - 3 && j >= 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i + 1][j - 1], matrix[i + 2][j - 2], matrix[i + 3][j - 3]))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private static int GetDiagonalFindingsLeft(char[][] matrix)
        {
            var count = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                // forward
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (j < matrix[i].Length - 3 && i < matrix.Length - 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i + 1][j + 1], matrix[i + 2][j + 2], matrix[i + 3][j + 3]))
                        {
                            count++;
                        }
                    }
                }

                // backward
                for (int j = matrix[i].Length - 1; j >= 0; j--)
                {
                    if (j >= 3 && i >= 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i - 1][j - 1], matrix[i - 2][j - 2], matrix[i - 3][j - 3]))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private static int GetVerticalFindings(char[][] matrix)
        {
            var count = 0;

            for (int j = 0; j < matrix[0].Length; j++)
            {
                // forward
                for (int i = 0; i < matrix.Length; i++)
                {
                    if (i < matrix[j].Length - 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i + 1][j], matrix[i + 2][j], matrix[i + 3][j]))
                        {
                            count++;
                        }
                    }
                }

                // backward
                for (int i = matrix.Length - 1; i >= 0; i--)
                {
                    if (i >= 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i - 1][j], matrix[i - 2][j], matrix[i - 3][j]))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private static int GetHorizontalFindings(char[][] matrix)
        {
            var count = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                // forward
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (j < matrix[i].Length - 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i][j + 1], matrix[i][j + 2], matrix[i][j + 3]))
                        {
                            count++;
                        }
                    }
                }

                // backward
                for (int j = matrix[i].Length - 1; j >= 0; j--)
                {
                    if (j >= 3)
                    {
                        if (CheckForXMAS(matrix[i][j], matrix[i][j - 1], matrix[i][j - 2], matrix[i][j - 3]))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private static bool CheckForXMAS(char one, char two, char three, char four)
        {
            return one == 'X' && two == 'M' && three == 'A' && four == 'S';
        }
    }
}
