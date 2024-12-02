using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024._03
{
    public class Day03
    {
        public static int Part1()
        {
            var lines = File.ReadAllLines(@"03\data03.txt");

            var safeReports = 0;

            foreach (var line in lines)
            {
                var levels = line.Split(' ').Select(int.Parse).ToArray();
                safeReports += ValidateLine(levels);
            }

            return safeReports;
        }
    }
}
