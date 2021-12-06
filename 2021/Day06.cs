using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    internal class Day06
    {
        static int Part1(string filename, int cycles)
        {
            var elements = new List<int>();
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                elements = line.Split(',').ToList().ConvertAll(x => int.Parse(x));
            }

            var values = elements;
            for (int i = 0; i < cycles; i++)
            {
                values = ProcessList(values);
            }

            return values.Count;
        }

        static double Part2(string filename, int cycles)
        {
            var register = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0};
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var elements = line.Split(',').ToList().ConvertAll(x => int.Parse(x));

                foreach (var item in elements)
                {
                    register[item]++;
                }
            }

            var values = register;
            for (int i = 0; i < cycles; i++)
            {
                values = ProcessRegister(values);
            }

            return values.Sum();
        }

        private static double[] ProcessRegister(double[] register)
        {
            var values = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0};

            values[0] += register[1];
            values[1] += register[2];
            values[2] += register[3];
            values[3] += register[4];
            values[4] += register[5];
            values[5] += register[6];
            values[6] += (register[7] + register[0]);
            values[7] += register[8];
            values[8] += register[0];

            return values;
        }

        private static List<int> ProcessList(List<int> values)
        {
            var temp = new List<int>();
            foreach (var item in values)
            {
                if (item == 0)
                {
                    temp.Add(8);
                    temp.Add(6);
                }
                else
                {
                    temp.Add(item - 1);
                }
            }
            return temp;
        }

        static void Main(string[] args)
        {
            Debug.WriteLine($"Part 1: {Part1("Data06.txt", 80)}");
            Debug.WriteLine($"Part 2: {Part2("Data06.txt", 256)}");
        }
    }
}
