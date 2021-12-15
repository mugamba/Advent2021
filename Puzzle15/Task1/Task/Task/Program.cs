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
        public static int _minsum = int.MaxValue;

        public static Dictionary<Tuple<int, int>, int> _memo = new Dictionary<Tuple<int, int>, int>();


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            _x = lines.Length;
            _y = lines.First().Length;
            _chitonMap = new int[_x, _y];

            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                    _chitonMap[i, j] = int.Parse(lines[i].ToCharArray()[j].ToString());

            FindBasins(new Tuple<int, int>(_x-1, _y-1),  0);

            Console.WriteLine("Result is {0}", _minsum - _chitonMap[0, 0]);
            Console.ReadKey();
        }


        public static void FindBasins(Tuple<int, int> currentBasin, int sum)
        {
            sum += _chitonMap[currentBasin.Item1, currentBasin.Item2];


            if (!_memo.ContainsKey(currentBasin))
                _memo.Add(currentBasin, sum);

            else
            {
                if (_memo[currentBasin] > sum)
                    _memo[currentBasin] = sum;
                else
                    return; 
            }
          
            if (sum > _minsum)
                return;


            if (currentBasin.Item1 == 0 && currentBasin.Item2 == 0)
            {
                if (sum < _minsum)
                    _minsum = sum;
            }

            var upCordinate = new Tuple<int, int>(currentBasin.Item1 - 1, currentBasin.Item2);
            var leftCordinate = new Tuple<int, int>(currentBasin.Item1, currentBasin.Item2 - 1);

            var up = int.MaxValue;
            var left = int.MaxValue;

            if (currentBasin.Item1 - 1 >= 0 && currentBasin.Item1-1 < _x )
                up = _chitonMap[currentBasin.Item1 - 1, currentBasin.Item2];
            if (currentBasin.Item2 - 1 >= 0 && currentBasin.Item2 - 1 < _y )
                left = _chitonMap[currentBasin.Item1, currentBasin.Item2 - 1];


            if (up != int.MaxValue)
            {
                FindBasins(upCordinate, sum);
            }

            if (left != int.MaxValue)
            {
                FindBasins(leftCordinate,  sum);
            }

        }


    }
}
