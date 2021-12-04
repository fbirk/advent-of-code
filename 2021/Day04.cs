using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day04
    {
        static int Part1(string filename)
        {
            var drawnNumbers = new List<int>();
            var matrixes = new List<List<List<Tuple<int, bool>>>>();

            {
                var lines = System.IO.File.ReadAllLines(filename);
                var matrix = 0;
                var matrixLine = 0;

                drawnNumbers = lines.First().Split(',').ToList().ConvertAll(x => int.Parse(x));

                matrixes.Add(new List<List<Tuple<int, bool>>>());
                for (int i = 2; i < lines.Length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i]))
                    {
                        matrixes.Add(new List<List<Tuple<int, bool>>>());
                        matrix++;
                        matrixLine = 0;
                    }
                    else
                    {
                        matrixes[matrix].Add(new List<Tuple<int, bool>>());

                        var line = lines[i].Split(' ');
                        foreach (var item in line)
                        {
                            if (!string.IsNullOrWhiteSpace(item))
                            {
                                matrixes[matrix][matrixLine].Add(Tuple.Create(int.Parse(item), false));
                            }
                        }
                        matrixLine++;
                    }
                }
            }

            foreach (var number in drawnNumbers)
            {
                SetNextNumber(matrixes, number);
                var winner = CheckForWinner(matrixes);

                if (winner != -1)
                {
                    var sum = GetBoardSum(matrixes[winner]);
                    return sum * number;
                }
            }

            return 0;
        }

        static int GetBoardSum(List<List<Tuple<int, bool>>> rows)
        {
            int sum = 0;

            foreach (var column in rows)
            {
                foreach (var item in column)
                {
                    if (!item.Item2)
                    {
                        sum += item.Item1;
                    }
                }
            }

            return sum;
        }

        static void SetNextNumber(List<List<List<Tuple<int, bool>>>> matrixes, int number)
        {
            foreach (var row in matrixes)
            {
                foreach (var column in row)
                {
                    for (int i = 0; i < column.Count; i++)
                    {
                        if (column[i].Item1 == number)
                        {
                            column[i] = Tuple.Create(column[i].Item1, true);
                        }
                    }
                }
            }
        }

        static int CheckForWinner(List<List<List<Tuple<int, bool>>>> matrixes)
        {
            var boardCounter = 0;

            foreach (var bingoBoard in matrixes)
            {
                var rowCounter = 0;

                // Zeilen in einem Bingofeld durchlaufen
                foreach (var row in bingoBoard)
                {
                    // Spalten in einem Bingofeld durchlaufen
                    var columnCounter = 0;
                    foreach (var column in row)
                    {
                        if (column.Item2)
                        {
                            columnCounter++;

                            if (columnCounter == row.Count)
                            {
                                return boardCounter;
                            }
                        }
                    }
                }

                // Spalten durchlaufen
                for (int j = 0; j < bingoBoard.Count; j++)
                {
                    // Zeilen durchlaufen
                    for (int i = 0; i < bingoBoard.Count; i++)
                    {
                        rowCounter = 0;
                        if (bingoBoard[i][j].Item2)
                        {
                            rowCounter++;
                            if (rowCounter == bingoBoard.Count)
                            {
                                return boardCounter;
                            }
                        }
                    }
                }

                boardCounter++;
            }

            return -1;
        }

        static void Main(string[] args)
        {
            Debug.WriteLine($"Part 1: {Part1("Data04.txt")}");

            // Lösung zu niedrig --> schon vorher valide Lösung die übersehen wurde??
        }
    }
}
