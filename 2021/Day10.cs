using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day10
    {

        public static int Part1(string filename)
        {
            var result = 0;
            var stack = new Stack<string>();
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var input = line.ToCharArray().Select(c => c.ToString()).ToArray();
                foreach (var item in input)
                {
                    if (item.Equals("[") || item.Equals("(") || item.Equals("{") || item.Equals("<"))
                    {
                        stack.Push(item);
                    }
                    else
                    {
                        var res = stack.Pop();
                        if (item.Equals("]") && res != "[")
                        {
                            result += 57;
                            break;
                        }
                        if (item.Equals(")") && res != "(")
                        {
                            result += 3;
                            break;
                        }
                        if (item.Equals("}") && res != "{")
                        {
                            result += 1197;
                            break;
                        }
                        if (item.Equals(">") && res != "<")
                        {
                            result += 25137;
                            break;
                        }
                    }

                }
            }


            return result;
        }

        public static double Part2(string filename)
        {
            var result = new List<double>();
            var stack = new Stack<string>();
            var lines = System.IO.File.ReadAllLines(filename);

            var validLines = new List<string[]>();

            foreach (var line in lines)
            {
                var isvalid = true;
                var input = line.ToCharArray().Select(c => c.ToString()).ToArray();
                foreach (var item in input)
                {
                    if (item.Equals("[") || item.Equals("(") || item.Equals("{") || item.Equals("<"))
                    {
                        stack.Push(item);
                    }
                    else
                    {
                        var res = stack.Pop();
                        if (item.Equals("]") && res != "[")
                        {
                            isvalid = false;
                            break;
                        }
                        if (item.Equals(")") && res != "(")
                        {
                            isvalid = false;
                            break;
                        }
                        if (item.Equals("}") && res != "{")
                        {
                            isvalid = false;
                            break;
                        }
                        if (item.Equals(">") && res != "<")
                        {
                            isvalid = false;
                            break;
                        }
                    }
                }
                if (isvalid)
                {
                    validLines.Add(input);
                }
            }

            foreach (var line in validLines)
            {
                double score = 0;
                stack.Clear();
                foreach (var item in line)
                {
                    if (item.Equals("[") || item.Equals("(") || item.Equals("{") || item.Equals("<"))
                    {
                        stack.Push(item);
                    }
                    else
                    {
                        stack.Pop();
                    }
                }

                foreach (var item in stack)
                {
                    if (item.Equals("["))
                    {
                        score *= 5;
                        score += 2;
                    }
                    if (item.Equals("("))
                    {
                        score *= 5;
                        score += 1;
                    }
                    if (item.Equals("{"))
                    {
                        score *= 5;
                        score += 3;
                    }
                    if (item.Equals("<"))
                    {
                        score *= 5;
                        score += 4;
                    }
                }
                result.Add(score);
            }

            result.Sort();

            return result[result.Count / 2];
        }
    }
}

