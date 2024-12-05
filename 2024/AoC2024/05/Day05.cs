
using System.Collections.Concurrent;

namespace AoC2024._05
{
    public class Day05
    {
        public static int Part1()
        {
            var result = 0;
            var lines = File.ReadAllLines(@"05\data05.txt");
            var isRules = true;

            var rules = new List<(int first, int second)>();
            var updates = new List<int[]>();
            var rulesLookup = new ConcurrentDictionary<int, List<int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == string.Empty)
                {
                    isRules = false;
                    continue;
                }

                if (isRules)
                {
                    var line = lines[i].Split('|');
                    rules.Add(new(int.Parse(line[0]), int.Parse(line[1])));
                }
                else
                {
                    var line = lines[i].Split(',');
                    updates.Add(line.ToList().Select(int.Parse).ToArray());
                }
            }

            rulesLookup = GenerateLookupTable(rules);
            result = ValidateUpdates(rulesLookup, updates).sumOfValidUpdates;

            return result;
        }

        public static int Part2()
        {
            var result = 0;
            var lines = File.ReadAllLines(@"05\data05.txt");
            var isRules = true;

            var rules = new List<(int first, int second)>();
            var updates = new List<int[]>();
            var rulesLookup = new ConcurrentDictionary<int, List<int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == string.Empty)
                {
                    isRules = false;
                    continue;
                }

                if (isRules)
                {
                    var line = lines[i].Split('|');
                    rules.Add(new(int.Parse(line[0]), int.Parse(line[1])));
                }
                else
                {
                    var line = lines[i].Split(',');
                    updates.Add(line.ToList().Select(int.Parse).ToArray());
                }
            }

            rulesLookup = GenerateLookupTable(rules);
            var invalidUpdates = ValidateUpdates(rulesLookup, updates).invalidUpdates;
            result = CorrectInvalidUpdatesAndGetResult(rulesLookup, invalidUpdates);

            return result;
        }

        private static int CorrectInvalidUpdatesAndGetResult(ConcurrentDictionary<int, List<int>> rulesLookup, List<int[]> invalidUpdates)
        {
            var validUpdates = 0;

            foreach (var update in invalidUpdates)
            {
                for (int i = 0; i < update.Length; i++)
                {
                    var index = IsCurrentPageValid(rulesLookup, update[i], update, i - 1);
                    if (index > -1)
                    {
                        // Repair
                        (update[i], update[index]) = (update[index], update[i]);

                        // Start from Beginning
                        i = 0;
                        continue;
                    }

                    if (i == update.Length - 1)
                    {
                        var mid = GetMiddleValue(update);
                        //Console.WriteLine(mid);
                        validUpdates += mid;
                    }
                }
            }

            return validUpdates;
        }

        private static (int sumOfValidUpdates, List<int[]> invalidUpdates) ValidateUpdates(ConcurrentDictionary<int, List<int>> rulesLookup, List<int[]> updates)
        {
            var validUpdates = 0;
            var invalidUpdateIndices = new List<int[]>();

            foreach (var update in updates)
            {
                for (int i = 0; i < update.Length; i++)
                {
                    if (IsCurrentPageValid(rulesLookup, update[i], update, i - 1) > -1)
                    {
                        invalidUpdateIndices.Add(update);
                        break;
                    }

                    if (i == update.Length - 1)
                    {
                        var mid = GetMiddleValue(update);
                        //Console.WriteLine(mid);
                        validUpdates += mid;
                    }
                }
            }

            return (validUpdates, invalidUpdateIndices);
        }

        private static int IsCurrentPageValid(ConcurrentDictionary<int, List<int>> rulesLookup, int key, int[] update, int i)
        {
            for (int j = i; j >= 0; j--)
            {
                if (rulesLookup.TryGetValue(key, out var value))
                {
                    if (value.Contains(update[j]))
                    {
                        return j;
                    }
                }
                else
                {
                    return -1;
                }
            }

            return -1;
        }

        private static int GetMiddleValue(int[] values)
        {
            return values[(int)Math.Floor(values.Length / 2.0)];
        }

        private static ConcurrentDictionary<int, List<int>> GenerateLookupTable(List<(int first, int second)> rules)
        {
            var result = new ConcurrentDictionary<int, List<int>>();
            rules.Sort((x, y) => x.first < y.first ? -1 : (x.first > y.first ? 1 : 0));

            foreach (var rule in rules)
            {
                result.AddOrUpdate(rule.first, new List<int>() { rule.second }, (x, y) =>
                {
                    y.Add(rule.second);
                    return y;
                });
            }

            return result;
        }
    }
}
