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

            Console.WriteLine("Result is {0}", CountRiskPoints());
            Console.ReadKey();
        }


        private static int CountRiskPoints()
        {

            var counter = 0;

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
                        counter += current + 1;


                }
            }

            return counter;
        }


    }
}
