namespace AoC2024._01
{
    public class Day01
    {
        public static int Part1()
        {
            var left = new List<int>();
            var right = new List<int>();

            var lines = File.ReadAllLines(@"01\data01.txt");

            foreach (var line in lines)
            {
                var elements = line.Split(' ');
                left.Add(int.Parse(elements[0]));
                right.Add(int.Parse(elements[3]));
            }

            left.Sort();
            right.Sort();

            var diff = 0;
            for (var i = 0; i < left.Count; i++)
            {
                diff += Math.Abs(right[i] - left[i]);
            }

            return diff;
        }

        public static int Part2()
        {
            var left = new List<int>();
            var right = new int[100000];

            var lines = File.ReadAllLines(@"01\data01.txt");
            foreach (var line in lines) {
                var elements = line.Split(' ');
                left.Add(int.Parse(elements[0]));
                right[int.Parse(elements[3])]++;
            }

            var similarityScore = 0;

            for (var i = 0; i < left.Count; i++)
            {
                similarityScore += right[left[i]] * left[i];
            }

            return similarityScore;
        }
    }
}
