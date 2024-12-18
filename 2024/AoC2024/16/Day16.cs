
using System.Diagnostics;

namespace AoC2024._16
{
    public class Day16
    {
        public static int Part1()
        {
            var result = 0;
            var lines = File.ReadAllLines(@"16\test16-1.txt");
            var matrix = new char[lines.Length][];
            var guard = new Guard();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                matrix[i] = line.ToCharArray();
                if (line.Contains('S'))
                {
                    guard.X = line.IndexOf('S');
                    guard.Y = i;
                    guard.Orientation = Orientation.West;
                }
            }

            result = FindCheapestPath(matrix, guard, 0);

            return result;
        }

        private static int FindCheapestPath(char[][] matrix, Guard guard, int count)
        {
            if (matrix[guard.Y][guard.X] == '#' || matrix[guard.Y][guard.X] == 'x'
                || guard.Y < 0 || guard.Y >= matrix.Length
                || guard.X < 0 || guard.X >= matrix[guard.Y].Length)
            {
                return int.MaxValue - 1010;
            }

            if (matrix[guard.Y][guard.X] == 'E')
            {
                Debug.WriteLine(count);
                return count;
            }

            var m = CopyMatrix(matrix);
            m[guard.Y][guard.X] = 'x';

            var p1 = FindCheapestPath(m, MakeAStepInDirection(guard, guard.Orientation), count + 1);
          //  var p2 = FindCheapestPath(m, new Guard(guard.X, guard.Y, (Orientation)mod((int)guard.Orientation - 2, 3)), count + 1);
            var p3 = FindCheapestPath(matrix, new Guard(guard.X, guard.Y, (Orientation)mod((int)guard.Orientation - 1, 3)), count + 1000);
            var p4 = FindCheapestPath(matrix, new Guard(guard.X, guard.Y, (Orientation)mod((int)guard.Orientation + 1, 3)), count + 1000);

            return Math.Min(p1, Math.Min(p3, p4));
        }

        private static char[][] CopyMatrix(char[][] matrix)
        {
            var res = new char[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                res[i] = new char[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    res[i][j] = matrix[i][j];
                }
            }

            return res;
        }

        private static Guard MakeAStepInDirection(Guard guard, Orientation orientation)
        {
            var res = new Guard();
            switch (orientation)
            {
                case Orientation.North:
                    res.X = guard.X;
                    res.Y = guard.Y - 1;
                    break;
                case Orientation.West:
                    res.X = guard.X + 1;
                    res.Y = guard.Y;
                    break;
                case Orientation.South:
                    res.Y = guard.Y + 1;
                    res.X = guard.X;
                    break;
                case Orientation.East:
                    res.Y = guard.Y;
                    res.X = guard.X - 1;
                    break;
                default:
                    break;
            }

            res.Orientation = orientation;
            return res;
        }
        static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }

    internal struct Guard
    {
        public int X;
        public int Y;
        public Orientation Orientation;

        public Guard() { }

        public Guard(int x, int y, Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }
    }

    internal enum Orientation
    {
        North = 0,
        West = 1,
        South = 2,
        East = 3
    }
}
