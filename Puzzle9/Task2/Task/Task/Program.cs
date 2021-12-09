using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static int[,] _heightMap;
        public static int _x;
        public static int _y;

        public static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> _lowHeigtsBasins = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            _x = lines.Length;
            _y = lines.First().Length;
            _heightMap = new int[_x, _y];


            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                {
                    _heightMap[i, j] = int.Parse(lines[i].ToCharArray()[j].ToString());

                }

            GetLowPoints();


            foreach (var position in _lowHeigtsBasins.Keys)
            {

                TraverseTree(position, position);
                    
            }



            var highest3 =_lowHeigtsBasins.Select(o => o.Value.Count).ToList().OrderByDescending(o => o).Take(3).ToArray();



            Console.WriteLine("Result is {0}", highest3[0]* highest3[1]* highest3[2]);
            Console.ReadKey();
        }


        private static void GetLowPoints()
        {

            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    var up = int.MaxValue;
                    var down = int.MaxValue;
                    var left = int.MaxValue;
                    var right = int.MaxValue;

                    var current = _heightMap[i, j];

                    if (i - 1 >= 0)
                        up = _heightMap[i - 1, j];

                    if (i + 1 < _x)
                        down = _heightMap[i + 1, j];


                    if (j - 1 >= 0)
                        left = _heightMap[i, j - 1];

                    if (j + 1 < _y)
                        right = _heightMap[i, j + 1];


                    if (current < up && current < down && current < left && current < right)
                        _lowHeigtsBasins.Add(new Tuple<int, int>(i, j), new List<Tuple<int, int>>());
                }
            }
        }

        public static void TraverseTree(Tuple<int, int> key, Tuple<int, int> nextbasin)
        {

            _lowHeigtsBasins[key].Add(nextbasin);

            var up = 9;
            var upCordinate = new Tuple<int, int>(nextbasin.Item1 - 1, nextbasin.Item2);
            var down = 9;
            var downCordinate = new Tuple<int, int>(nextbasin.Item1 + 1, nextbasin.Item2);
            var left = 9;
            var leftCordinate = new Tuple<int, int>(nextbasin.Item1, nextbasin.Item2 - 1);
            var right = 9;
            var rightCordinate = new Tuple<int, int>(nextbasin.Item1, nextbasin.Item2 + 1);


            if (nextbasin.Item1 - 1 >= 0)
            {
                up = _heightMap[nextbasin.Item1 - 1, nextbasin.Item2];
            }

            if (nextbasin.Item1 + 1 < _x)
                down = _heightMap[nextbasin.Item1 + 1, nextbasin.Item2];


            if (nextbasin.Item2 - 1 >= 0)
                left = _heightMap[nextbasin.Item1, nextbasin.Item2 - 1];

            if (nextbasin.Item2 + 1 < _y)
                right = _heightMap[nextbasin.Item1, nextbasin.Item2 + 1];


            if (up != 9 && !_lowHeigtsBasins[key].Contains(upCordinate))
                TraverseTree(key, upCordinate);

            if (down != 9 && !_lowHeigtsBasins[key].Contains(downCordinate))
                TraverseTree(key, downCordinate);

            if (left != 9 && !_lowHeigtsBasins[key].Contains(leftCordinate))
                TraverseTree(key, leftCordinate);

            if (right != 9 && !_lowHeigtsBasins[key].Contains(rightCordinate))
                TraverseTree(key, rightCordinate);


        }






    }
}
