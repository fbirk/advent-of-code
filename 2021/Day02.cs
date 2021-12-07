
namespace AdventOfCode
{
    public class Day02
    {
        public static int Part2()
        {
            int x = 0;
            int y = 0;
            int aim = 0;
            var lines = System.IO.File.ReadAllLines(@"C:\Users\Fabian Birk\projects\advent-of-code\2021\AdventOfCode\Data02.txt");

            foreach (var line in lines)
            {
                var elements = line.Split(' ');
                string instruction = elements[0];
                int value = int.Parse(elements[1]);

                if (instruction.Equals("forward"))
                {
                    x += value;
                    y += (value * aim);
                }
                if (instruction.Equals("backward"))
                {
                    x -= value;
                }
                if (instruction.Equals("down"))
                {
                    aim += value;
                }
                if (instruction.Equals("up"))
                {
                    aim -= value;
                }
            }

            return x * y;
        }
    }
}
