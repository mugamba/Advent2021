using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static int[,] _octopuses;
        public static List<Tuple<int, int>> _forFlashing = new List<Tuple<int, int>>();
        public static List<Tuple<int, int>> _newFlashers = new List<Tuple<int, int>>();
        public static int _x;
        public static int _y;

        public static long flashes = 0;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            _x = lines.Length;
            _y = lines.First().Length;
            _octopuses = new int[_x, _y];

            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                    _octopuses[i, j] = int.Parse(lines[i].ToCharArray()[j].ToString());

            var allFlasshed = false;
            var counter = 1;
            while (allFlasshed == false)
            {

                DoStep();
                DoFlashes();
                allFlasshed = AllFlashed();
                counter++;
            }

            Console.WriteLine("Result is {0}", counter);
            Console.ReadKey();
        }


        private static void DoStep()
        {
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {

                    if (_octopuses[i, j] < 9)
                        _octopuses[i, j]++;
                    else
                    {
                        _octopuses[i, j] = 0;
                        _forFlashing.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

        }


        private static void DoFlashes()
        {
            while (true)
            {

                if (_forFlashing.Count == 0)
                    break;

                foreach (var octo in _forFlashing)
                {
                    var x = octo.Item1;
                    var y = octo.Item2;

                   // Console.WriteLine("Flashing now x={0} y={1} value {2}", x, y, _octopuses[x, y]);

                    DoFlashCoordinate(x - 1, y - 1);
                    DoFlashCoordinate(x, y - 1);
                    DoFlashCoordinate(x + 1, y - 1);
                    DoFlashCoordinate(x - 1, y);
                    DoFlashCoordinate(x + 1, y);
                    DoFlashCoordinate(x - 1, y + 1);
                    DoFlashCoordinate(x, y + 1);
                    DoFlashCoordinate(x + 1, y + 1);

                }

                flashes += _forFlashing.Count;
                _forFlashing.Clear();
                _forFlashing.AddRange(_newFlashers.ToArray());

                Console.Write(_newFlashers.ToArray());
                Console.WriteLine();
                _newFlashers.Clear();

            }

        }


        public static void DoFlashCoordinate(int x, int y)
        {
            if (x >= 0 && x < 10 && y >= 0 && y < 10)
            {
                if (_octopuses[x, y] < 9 && _octopuses[x, y] > 0)
                {
                    _octopuses[x, y]++;
                }
                else
                {
                    if (_octopuses[x, y] == 9)
                    {
                        _octopuses[x, y] = 0;
                        var coord = new Tuple<int, int>(x, y);
                        if (!_newFlashers.Contains(coord))
                            _newFlashers.Add(coord);
                    }
                }

            }

        }

        public static Boolean AllFlashed()
        {
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    if (_octopuses[i, j] != 0)
                    {
                        return false;
                        
                    }
                }

            }

            return true;
        }

        public static void PrintArray()
        {

            var builder = new StringBuilder();
            Console.WriteLine();

            for (int i = 0; i < _x; i++)
            {
                builder.Clear();
                for (int j = 0; j < _y; j++)
                {
                    builder.Append(_octopuses[i, j]);
                }

                Console.WriteLine(builder.ToString());
            }
        }


    }
}
