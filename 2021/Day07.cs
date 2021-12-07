using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day07
    {

        public static int Part1(string filename)
        {
            var elements = new List<int>();
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                elements = line.Split(',').ToList().ConvertAll(x => int.Parse(x));
            }

            elements.Sort();

            var counter = 0;
            var results = new List<int>();

            for (int i = 0; i < elements.Last(); i++)
            {
                results.Add(0);

                foreach (var item in elements)
                {
                    results[counter] += Math.Abs(item - i);
                }

                counter++;
            }

            results.Sort();

            return results.First();
        }

        public static int Part2(string filename)
        {
            var elements = new List<int>();
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                elements = line.Split(',').ToList().ConvertAll(x => int.Parse(x));
            }

            elements.Sort();

            var counter = 0;
            var results = new List<int>();

            for (int i = 0; i < elements.Last(); i++)
            {
                results.Add(0);

                foreach (var item in elements)
                {
                    var n = Math.Abs(item - i);
                    results[counter] += (n * (n + 1)) / 2;
                }

                counter++;
            }

            results.Sort();

            return results.First();
        }
    }
}
