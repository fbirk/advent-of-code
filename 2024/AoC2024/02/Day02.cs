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
                var previous = -1;
                var levels = line.Split(' ');
                var upDown = 0; // -1 == down; 1 == up

                for (int i = 0; i < levels.Length; i++)
                {
                    var val = int.Parse(levels[i]);
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
                        safeReports++;
                    }
                    previous = val;
                }
            }

            return safeReports;
        }

        public static int Part2()
        {
            var lines = File.ReadAllLines(@"02\test02.txt");

            var safeReports = 0;

            foreach (var line in lines)
            {
                var previous = -1;
                var levels = line.Split(' ');
                var upDown = 0; // -1 == down; 1 == up
                var invalid = 0;
                var invalidIsSet = false;
                var next = -1;

                for (int i = 0; i < levels.Length; i++)
                {
                    var val = int.Parse(levels[i]);

                    if (i < levels.Length - 2)
                    {
                        next = int.Parse(levels[i + 1]);
                    }

                    if (previous != -1)
                    {
                        // set phase
                        if (upDown == 0)
                        {
                            upDown = val > previous ? 1 : -1;
                            if (val == previous)
                            {
                                if (!invalidIsSet)
                                {
                                    invalid++;
                                    invalidIsSet = true;
                                }
                            }
                        }
                        else
                        {
                            if (upDown == -1 && val >= previous)
                            {
                                if (!invalidIsSet)
                                {
                                    invalid++;
                                    invalidIsSet = true;
                                }
                                if (next != -1 && upDown == -1 && next >= previous)
                                {
                                    invalid = 2;
                                }
                            }
                            if (upDown == 1 && val <= previous)
                            {
                                if (!invalidIsSet)
                                {
                                    invalid++;
                                    invalidIsSet = true;
                                }
                                if (next != -1 && upDown == 1 && next <= previous)
                                {
                                    invalid = 2;
                                }
                            }
                        }

                        if (Math.Abs(previous - val) > 3 || Math.Abs(previous - val) < 1)
                        {

                            if (i < levels.Length - 2)
                            {
                                next = int.Parse(levels[i + 1]);
                                if ((Math.Abs(previous - next) > 3 || Math.Abs(previous - next) < 1))
                                {
                                    invalid = 2;
                                    break;
                                }
                            }
                            else
                            {
                                if (!invalidIsSet)
                                {
                                    invalid++;
                                    invalidIsSet = true;
                                }
                            }
                        }
                    }

                    if (i == levels.Length - 1 && invalid <= 1)
                    {
                        safeReports++;
                    }
                    previous = val;
                    invalidIsSet = false;
                    next = -1;
                }
            }

            return safeReports;
        }
    }
}