using System.Text.RegularExpressions;

namespace AoC2024._03
{
    public class Day03
    {
        public static int Part1()
        {
            var multResult = 0;
            var lines = File.ReadAllLines(@"03\data03.txt");

            foreach (var line in lines)
            {
                var pattern = "(mul\\(\\d+,\\d+\\))";
                var matches = Regex.Matches(line, pattern);
                if (matches != null)
                {
                    foreach (var match in matches.Select(x => x.Value))
                    {
                        var res = TryGetValue(match);
                        if (res != -1)
                        {
                            multResult += res;
                        }
                    }
                }
            }

            return multResult;
        }

        public static int Part2()
        {
            var multResult = 0;
            var lines = File.ReadAllLines(@"03\data03.txt");

            var doIsActive = true;

            foreach (var line in lines)
            {
                var pattern = "(mul\\(\\d+,\\d+\\))|(don't\\(\\))|(do\\(\\))";
                var matches = Regex.Matches(line, pattern);
                if (matches != null)
                {
                    foreach (var match in matches.Select(x => x.Value))
                    {
                        var res = TryGetValue(match);
                        if (res != -1 && doIsActive)
                        {
                            multResult += res;
                        }
                        else if (match == "don't()")
                        {
                            doIsActive = false;
                        }
                        else if (match == "do()")
                        {
                            doIsActive = true;
                        }
                    }
                }
            }

            return multResult;
        }

        private static int TryGetValue(string multResult)
        {
            var pattern = "(\\d+,\\d+)";
            if (Regex.IsMatch(multResult, pattern))
            {
                var match = Regex.Match(multResult, pattern).Groups[1].Value;
                var values = match.Split(',');
                var a = int.Parse(values[0]);
                var b = int.Parse(values[1]);
                //Console.WriteLine($"{a} * {b}");
                return a * b;
            }
            return -1;
        }
    }
}
