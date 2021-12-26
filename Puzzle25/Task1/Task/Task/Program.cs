using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task
{
    public class Program
    {

        public static Tuple<char, bool> [,] _seaCucMap;
        public static Tuple<char, bool>[,] _emptyArray;

        public static int _x;
        public static int _y;
       
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            _y = lines.Length;
            _x = lines.First().Length;
            _seaCucMap = new Tuple<char, bool>[_x, _y];
            _emptyArray = new Tuple<char, bool>[_x, _y];

            for (int j = 0; j < _y; j++)
                for (int i = 0; i < _x; i++)
                {
                    _seaCucMap[i, j] = new Tuple<char, bool>(lines[j].ToCharArray()[i], false);
                    _emptyArray[i, j] = new Tuple<char, bool>('.', false);
                }
            //            Console.WriteLine("Result is {0}", _minsum - _chitonMap[0, 0]);

            PrintArray();

            var moveSouth = int.MaxValue; var moveEast = int.MaxValue; var counter = 0;
            while (moveSouth != 0 || moveEast != 0)
            {

                moveEast = CalcualteMovesGoingEast();
                MakeMoveEast();
                moveSouth = CalcualteMovesGoingSouth();
                MakeMoveSouth();
                counter++;
                //PrintArray();

            }

            Console.WriteLine("Result is {0} ", counter.ToString());
            Console.ReadKey();
        }


        public static void PrintArray()
        {
            var x = _seaCucMap.GetLength(1);
            var y = _seaCucMap.GetLength(0);

            for (int j = 0; j < x; j++)
            {
                for (int i = 0; i < y; i++)
                {
                    Console.Write(_seaCucMap[i, j].Item1);
                }

                Console.WriteLine();
               
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        public static int CalcualteMovesGoingEast()
        {
            var counter = 0;
            for (int j = 0; j < _y; j++)
                for (int i = 0; i < _x; i++)
                {

                    if (_seaCucMap[i, j].Item1 == '>')
                    {

                        var nextField = new Tuple<char, bool>('.', false);
                        if (i + 1 >= _x)
                        {
                            nextField = _seaCucMap[0, j];

                        }
                        else
                            nextField = _seaCucMap[i + 1, j];
                        if (nextField.Item1 == '.')
                        {
                            _seaCucMap[i, j] = new Tuple<char, bool>(_seaCucMap[i, j].Item1, true);
                            counter++;
                        }
                    }

                }

            return counter;
        }

        public static void MakeMoveEast()
        {

            for (int j = 0; j < _y; j++)
                for (int i = 0; i < _x; i++)
                {
                    if (_seaCucMap[i, j].Item1 == '>' && _seaCucMap[i, j].Item2 == true)
                    {
                        var nextField = new Tuple<char, bool>('.', false);
                        if (i + 1 >= _x)
                        {
                            _seaCucMap[0, j] = new Tuple<char, bool>(_seaCucMap[i, j].Item1, false);
                            _seaCucMap[i, j] = new Tuple<char, bool>('.', false);
                        }
                        else
                        {
                            _seaCucMap[i + 1, j] = new Tuple<char, bool>(_seaCucMap[i, j].Item1, false);
                            _seaCucMap[i, j] = new Tuple<char, bool>('.', false);

                        }
                    }
                   
                }          
        }

        public static int CalcualteMovesGoingSouth()
        {
            var counter = 0;
            for (int j = 0; j < _y; j++)
                for (int i = 0; i < _x; i++)
                {

                    if (_seaCucMap[i, j].Item1 == 'v')
                    {

                        var nextField = new Tuple<char, bool>('.', false);
                        if (j + 1 >= _y)
                        {
                            nextField = _seaCucMap[i, 0];

                        }
                        else
                            nextField = _seaCucMap[i, j+1];
                        if (nextField.Item1 == '.')
                        {
                            _seaCucMap[i, j] = new Tuple<char, bool>(_seaCucMap[i, j].Item1, true);
                            counter++;
                        }
                    }
                }
            return counter;
        }

        public static void MakeMoveSouth()
        {

            for (int j = 0; j < _y; j++)
                for (int i = 0; i < _x; i++)
                {
                    if (_seaCucMap[i, j].Item1 == 'v' && _seaCucMap[i, j].Item2 == true)
                    {
                        var nextField = new Tuple<char, bool>('.', false);
                        if (j + 1 >= _y)
                        {
                            _seaCucMap[i, 0] = new Tuple<char, bool>(_seaCucMap[i, j].Item1, false);
                            _seaCucMap[i, j] = new Tuple<char, bool>('.', false);
                        }
                        else
                        {
                            _seaCucMap[i, j+1] = new Tuple<char, bool>(_seaCucMap[i, j].Item1, false);
                            _seaCucMap[i, j] = new Tuple<char, bool>('.', false);

                        }
                    }

                }
        }


    }
}
