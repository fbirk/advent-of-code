namespace AoC2024._02
{
    public class Day02
    {
        public static int Part1()
        {
            var lines = File.ReadAllLines(@"02\data02.txt");

            var safeReports = 0;

            foreach (var line in lines)
            {
                var levels = line.Split(' ').Select(int.Parse).ToArray();
                safeReports += ValidateLine(levels);
            }

            return safeReports;
        }

        public static int Part2()
        {
            var lines = File.ReadAllLines(@"02\data02.txt");

            var safeReports = 0;

            foreach (var line in lines)
            {
                var levels = line.Split(' ').Select(int.Parse).ToArray();
                var mutations = GetVariationsForLine(levels);
                var results = new List<int>();
                foreach (var mutation in mutations)
                {
                    results.Add(ValidateLine(mutation));
                }
                safeReports += results.Max();
            }

            return safeReports;
        }

        private static List<int[]> GetVariationsForLine(int[] levels)
        {
            var results = new List<int[]>();
            for (int i = 0; i < levels.Length; i++)
            {
                int[] levelsClone = new int[levels.Length];
                levels.CopyTo(levelsClone, 0);
                levelsClone[i] = 0;
                results.Add(levelsClone);
            }

            return results;
        }

        private static int ValidateLine(int[] levels)
        {
            var previous = -1;
            var upDown = 0; // -1 == down; 1 == up

            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i] == 0)
                {
                    if (i == levels.Length - 1)
                    {
                        // if 0 is the last value in this line and we have not skipped so far --> return success
                        return 1;
                    }

                    // skip values with 0 for Part 2
                    continue;
                }

                var val = levels[i];
                if (previous != -1)
                {
                    // set phase
                    if (upDown == 0)
                    {
                        upDown = val > previous ? 1 : -1;
                        if (val == previous)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (upDown == -1 && val >= previous)
                        {
                            break;
                        }
                        if (upDown == 1 && val <= previous)
                        {
                            break;
                        }
                    }

                    if (Math.Abs(previous - val) > 3 || Math.Abs(previous - val) < 1)
                    {
                        break;
                    }
                }

                if (i == levels.Length - 1)
                {
                    return 1;
                }
                previous = val;
            }

            return 0;
        }
    }
}