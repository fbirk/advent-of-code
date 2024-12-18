namespace AoC2024._13
{
    public class Day13
    {
        public static long Part1()
        {
            long result = 0;
            var lines = File.ReadAllLines(@"13\data13.txt");
            var clawProperties = new List<ClawProperty>();
            var clawProp = new ClawProperty();

            for (long i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (line.Contains("A"))
                    {
                        clawProp = new ClawProperty();
                        var term = line.Split(':');
                        var x = term[1].Trim().Split(',')[0];
                        var y = term[1].Trim().Split(",")[1];
                        clawProp.VectorA.X = long.Parse(x.Split("+")[1]);
                        clawProp.VectorA.Y = long.Parse(y.Split("+")[1]);
                    }

                    if (line.Contains("B"))
                    {
                        var term = line.Split(':');
                        var x = term[1].Trim().Split(',')[0];
                        var y = term[1].Trim().Split(",")[1];
                        clawProp.VectorB.X = long.Parse(x.Split("+")[1]);
                        clawProp.VectorB.Y = long.Parse(y.Split("+")[1]);
                    }

                    if (line.Contains("Prize"))
                    {
                        var term = line.Split(':');
                        var x = term[1].Trim().Split(',')[0];
                        var y = term[1].Trim().Split(",")[1];
                        clawProp.Prize.X = long.Parse(x.Split("=")[1]) + 10000000000000;
                        clawProp.Prize.Y = long.Parse(y.Split("=")[1]) + 10000000000000;

                        clawProperties.Add(clawProp);
                        result += CalculateNumberOfTokens(clawProp);
                    }
                }
            }

            return result;
        }

        private static long CalculateNumberOfTokens(ClawProperty clawProp)
        {
            var equations = new[] {
            new [] { clawProp.VectorA.X, clawProp.VectorB.X,  clawProp.Prize.X},
            new [] { clawProp.VectorA.Y, clawProp.VectorB.Y, clawProp.Prize.Y}};
            var res = SolveCramer(equations);
            var a = res[0];
            var b = res[1];

            if (a < 0 || b < 0)
            {
                return 0;
            }

            // Check for valid solutions
            if (a * clawProp.VectorA.X + b * clawProp.VectorB.X != clawProp.Prize.X || a * clawProp.VectorA.Y + b * clawProp.VectorB.Y != clawProp.Prize.Y)
            {
                return 0;
            }

            return (3 * res[0]) + res[1];
        }

        /// <summary>
        /// Using the cramers rule to solve the linear equation system.
        /// Algorithm copied from rosetta code.
        /// </summary>
        private static long[] SolveCramer(long[][] equations)
        {
            long size = equations.Length;
            if (equations.Any(eq => eq.Length != size + 1)) throw new ArgumentException($"Each equation must have {size + 1} terms.");
            long[,] matrix = new long[size, size];
            long[] column = new long[size];
            for (long r = 0; r < size; r++)
            {
                column[r] = equations[r][size];
                for (long c = 0; c < size; c++)
                {
                    matrix[r, c] = equations[r][c];
                }
            }
            return Solve(new SubMatrix(matrix, column));
        }

        private static long[] Solve(SubMatrix matrix)
        {
            long det = matrix.Det();
            if (det == 0) throw new ArgumentException("The determinant is zero.");

            long[] answer = new long[matrix.Size];
            for (long i = 0; i < matrix.Size; i++)
            {
                matrix.ColumnIndex = i;
                answer[i] = matrix.Det() / det;
            }
            return answer;
        }

        private class SubMatrix
        {
            private long[,] source;
            private SubMatrix prev;
            private long[] replaceColumn;

            public SubMatrix(long[,] source, long[] replaceColumn)
            {
                this.source = source;
                this.replaceColumn = replaceColumn;
                this.prev = null;
                this.ColumnIndex = -1;
                Size = replaceColumn.Length;
            }

            private SubMatrix(SubMatrix prev, long deletedColumnIndex = -1)
            {
                this.source = null;
                this.prev = prev;
                this.ColumnIndex = deletedColumnIndex;
                Size = prev.Size - 1;
            }

            public long ColumnIndex { get; set; }
            public long Size { get; }

            public long this[long row, long column]
            {
                get
                {
                    if (source != null) return column == ColumnIndex ? replaceColumn[row] : source[row, column];
                    return prev[row + 1, column < ColumnIndex ? column : column + 1];
                }
            }

            public long Det()
            {
                if (Size == 1) return this[0, 0];
                if (Size == 2) return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
                SubMatrix m = new SubMatrix(this);
                long det = 0;
                long sign = 1;
                for (long c = 0; c < Size; c++)
                {
                    m.ColumnIndex = c;
                    long d = m.Det();
                    det += this[0, c] * d * sign;
                    sign = -sign;
                }
                return det;
            }
        }

        internal class ClawProperty
        {
            public Coordinate VectorA;
            public Coordinate VectorB;
            public Coordinate Prize;

            public double SlopeVectorA => VectorA.Y / VectorA.X;
            public double SlopeVectorB => VectorB.Y / VectorB.X;
        }

        internal struct Coordinate
        {
            public long X;
            public long Y;
        }
    }
}