namespace AoC2024._06
{
    public class Day06
    {
        public static int Part1()
        {
            var result = 0;
            var lines = File.ReadAllLines(@"06\data06.txt");
            var matrix = new char[lines.Length][];
            var guard = new Guard();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                matrix[i] = line.ToCharArray();
                if (line.Contains('^'))
                {
                    guard.X = line.IndexOf('^');
                    guard.Y = i;
                    guard.Orientation = Orientation.Up;
                }
            }

            while (GuardCanMove(ref matrix, guard))
            {
                MoveGuard(ref matrix, ref guard);
            }

            result = CountGuardPositions(matrix);

            return result;
        }

        private static int CountGuardPositions(char[][] matrix)
        {
            var count = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 'X')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static void MoveGuard(ref char[][] matrix, ref Guard guard)
        {
            switch (guard.Orientation)
            {
                case Orientation.Up:
                    if (matrix[guard.Y - 1][guard.X] != '#')
                    {
                        MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                        guard.Y--;
                    }
                    else
                    {
                        guard.Orientation = Orientation.Right;
                    }
                    break;
                case Orientation.Down:
                    if (matrix[guard.Y + 1][guard.X] != '#')
                    {
                        MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                        guard.Y++;
                    }
                    else
                    {
                        guard.Orientation = Orientation.Left;
                    }
                    break;
                case Orientation.Left:
                    if (matrix[guard.Y][guard.X - 1] != '#')
                    {
                        MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                        guard.X--;
                    }
                    else
                    {
                        guard.Orientation = Orientation.Up;
                    }
                    break;
                case Orientation.Right:
                    if (matrix[guard.Y][guard.X + 1] != '#')
                    {
                        MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                        guard.X++;
                    }
                    else
                    {
                        guard.Orientation = Orientation.Down;
                    }
                    break;
                default:
                    break;
            }
        }

        private static void MarkCurrentPosition(ref char[][] matrix, int y, int x)
        {
            matrix[y][x] = 'X';
        }

        private static bool GuardCanMove(ref char[][] matrix, Guard guard)
        {
            if (guard.Orientation == Orientation.Up && guard.Y == 0)
            {
                MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                return false;
            }

            if (guard.Orientation == Orientation.Down && guard.Y == matrix.Length - 1)
            {
                MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                return false;
            }

            if (guard.Orientation == Orientation.Left && guard.X == 0)
            {
                MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                return false;
            }

            if (guard.Orientation == Orientation.Right && guard.X == matrix[guard.Y].Length - 1)
            {
                MarkCurrentPosition(ref matrix, guard.Y, guard.X);
                return false;
            }

            return true;
        }
    }

    public struct Guard
    {
        public int X;
        public int Y;
        public Orientation Orientation;
    }

    public enum Orientation
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
}
