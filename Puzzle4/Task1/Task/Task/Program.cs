using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static List<Tuple<int, bool>[,]> _boards = new List<Tuple<int, bool>[,]>();
        public static List<int> _drawn = new List<int>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var drawnlines = lines.First();
            var boardlines = lines.Skip(2).ToList();

            var drawnSplited = drawnlines.Split(',');
            _drawn.AddRange(drawnSplited.Select(o => int.Parse(o)));


            var count = 0;
            var boardString = new List<String>();


            foreach (var line in boardlines)
            {
                if (line.Equals(""))
                {
                    _boards.Add(ParseBoard(boardString));
                    count = 0;
                    boardString.Clear();
                    continue;
                }
                else
                    boardString.Add(line);

            }
            _boards.Add(ParseBoard(boardString));



            for (int i = 0; i < _drawn.Count; i++)
            {

                foreach (var board in _boards)
                {
                    var isWinner = MarkNumberInBoardAndCheckIsWinner(board, _drawn[i]);
                    if (isWinner)
                    {
                        var result = CalculateResultOfWinningBoard(board, _drawn[i]);
                      
                        Console.WriteLine("Result is {0}", result);
                        Console.ReadKey();
                    }
                }

            }

        }


        private static Tuple<int, bool>[,] ParseBoard(List<string> boardLines)
        {
            var array = new Tuple<int, bool>[5, 5];

            for (int i = 0; i < 5; i++)
            {
                var splitedLine = Regex.Split(boardLines[i], @"\s+").Where(s => s != string.Empty).ToArray();

                for (int j = 0; j < 5; j++)
                {
                    array[i, j] = new Tuple<int, bool>(int.Parse(splitedLine[j]), false);

                }
            }

            return array;
        }

        private static Boolean MarkNumberInBoardAndCheckIsWinner(Tuple<int, bool>[,] board, int number)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board[i, j].Item1 == number)
                    {
                        board[i, j] = new Tuple<int, bool>(board[i, j].Item1, true);
                        var columnIsWinner = GetColumn(board, j).Where(o => o.Item2 == true).Count() == 5;
                        var rowIsWinner = GetRow(board, i).Where(o => o.Item2 == true).Count() == 5;
                        if (columnIsWinner || rowIsWinner)
                        {
                            return true;
                        }

                    }

                }
            }
            return false;
        }

        private static Int32 CalculateResultOfWinningBoard(Tuple<int, bool>[,] board, int number)
        {
            var counter = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board[i, j].Item2 == false)
                        counter += board[i, j].Item1;

                }
            }
            return counter * number;
        }



        public static Tuple<int, bool>[] GetColumn(Tuple<int, bool>[,] board, int columnNumber)
        {
            return Enumerable.Range(0, board.GetLength(0))
                    .Select(x => board[x, columnNumber])
                    .ToArray();
        }

        public static Tuple<int, bool>[] GetRow(Tuple<int, bool>[,] board, int rowNumber)
        {
            return Enumerable.Range(0, board.GetLength(1))
                    .Select(x => board[rowNumber, x])
                    .ToArray();
        }

    }
}
