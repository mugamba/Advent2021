using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static int[,] _chitonMap;
        public static int _x;
        public static int _y;
        public static long _minsum = int.MaxValue;

        public static Dictionary<Tuple<int, int>, int> _memo = new Dictionary<Tuple<int, int>, int>();


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var linelength = lines.First().Length;
            var numofLines = lines.Length;

            _x = lines.Length*5;
            _y = lines.First().Length*5;
            _chitonMap = new int[_x, _y];


            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                {
                    var num = (int.Parse(lines[i % numofLines].ToCharArray()[j % linelength].ToString()));
                    num = num + (i / linelength) + (j / linelength);
                    num = num % 9 == 0 ? 9 : num % 9;
                    _chitonMap[j, i] = num;
                }


            FindChitons(new Tuple<int, int>(0, 0), 0);
            Console.WriteLine("Result is {0}", _minsum - _chitonMap[0, 0]);
            Console.ReadKey();
         
        }


        public static void PrintArray()
        {
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                    Console.Write(_chitonMap[j, i]);

                Console.WriteLine();
            }
        }

        public static void FindChitons(Tuple<int, int> currentBasin,  int sum)
        {
            sum += _chitonMap[currentBasin.Item1, currentBasin.Item2];

            if (sum > _minsum)
                return;

            if (!_memo.ContainsKey(currentBasin))
                _memo.Add(currentBasin, sum);

            else
            {
                if (sum <_memo[currentBasin])
                    _memo[currentBasin] = sum;
                else
                    return; 
            }
          
            if (currentBasin.Item1 == _x-1 && currentBasin.Item2 == _y-1)
            {
                if (sum < _minsum)
                    _minsum = sum;

                return;
            }

            var downC = new Tuple<int, int>(currentBasin.Item1, currentBasin.Item2+1);
            var rightC = new Tuple<int, int>(currentBasin.Item1+1, currentBasin.Item2);
            var upC = new Tuple<int, int>(currentBasin.Item1, currentBasin.Item2 - 1);
            var leftC = new Tuple<int, int>(currentBasin.Item1 -1, currentBasin.Item2);

            var down = int.MaxValue;
            var right = int.MaxValue;
            var up = int.MaxValue;
            var left = int.MaxValue;

            if (currentBasin.Item2 + 1 >= 0 && currentBasin.Item2+1 < _y)
                down = _chitonMap[currentBasin.Item1, currentBasin.Item2+1];
            if (currentBasin.Item1 + 1 >= 0 && currentBasin.Item1 + 1 < _x )
                right = _chitonMap[currentBasin.Item1+1, currentBasin.Item2];
            if (currentBasin.Item2 - 1 >= 0 && currentBasin.Item2 - 1 < _y)
                up = _chitonMap[currentBasin.Item1, currentBasin.Item2 - 1];
            if (currentBasin.Item1 - 1 >= 0 && currentBasin.Item1 - 1 < _x)
                left = _chitonMap[currentBasin.Item1 -  1, currentBasin.Item2];




            if (down != int.MaxValue)
            {
                FindChitons(downC, sum);
            }

            if (right != int.MaxValue)
            {
                FindChitons(rightC, sum);
            }

            if (up != int.MaxValue)
            {
                FindChitons(upC, sum);
            }

            if (left != int.MaxValue)
            {
                FindChitons(leftC, sum);
            }

        }

        private static int MinimalDistanceSum(Tuple<int, int> currentBasin, Tuple<int, int> lastBasin)
        {
            return (lastBasin.Item1 - currentBasin.Item1) + (lastBasin.Item2 - currentBasin.Item2);
        }
    }
}
