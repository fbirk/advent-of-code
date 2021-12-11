using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day08
    {

        public static int Part1(string filename)
        {
            var elements = new List<String>();
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var output = line.Split('|').Last();

                elements.AddRange(output.Split(' '));
            }

            var result = 0;

            result = elements.Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7).Count();

            return result;
        }

        public static int Part2(string filename)
        {
            var elements = new List<String>();
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var output = line.Split('|').Last();

                elements.AddRange(output.Split(' '));
            }

            var result = 0;
            foreach (var item in elements)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    result += MapStringToInt(item);
                }
            }

            return result;
        }

        private static int MapStringToInt(string value)
        {
            var elements = value.ToCharArray().ToList();
            elements.Sort();

            var checkString = new string(elements.ToArray());

            if (checkString.Length == 2)
            {
                return 1;
            }
            else if (checkString.Length == 3)
            {
                return 7;
            }
            else if (checkString.Length == 4)
            {
                return 4;
            }
            else if (checkString.Length == 7)
            {
                return 8;
            }
            else if (checkString.Equals("abcdef") || checkString.Equals("abcdfg"))
            {
                return 9;
            }
            else if (checkString.Equals("bcdefg") || checkString.Equals("acdefg") || checkString.Equals("abcefg"))
            {
                return 6;
            }
            else if (checkString.Equals("abcdeg"))
            {
                return 0;
            }
            else if (checkString.Equals("abcdf") || checkString.Equals("abcde") || checkString.Equals("abefg") || checkString.Equals("abcfg"))
            {
                return 3;
            }
            else if (checkString.Equals("bcdef") || checkString.Equals("bcefg") || checkString.Equals("abceg"))
            {
                return 5;
            }
            else if (checkString.Equals("abcdg"))
            {
                return 2;

            }

            throw new ArgumentException($"Nicht erkannte Eingabe: {checkString}");
        }
    }
}

