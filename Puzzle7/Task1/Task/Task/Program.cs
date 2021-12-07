using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {

        public static List<int> _list = new List<int>();
        public static List<long> _sums = new List<long>();
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").First();
            var splitted = lines.Split(",");
            _list.AddRange(splitted.Select(o => int.Parse(o)).ToArray());


            var max = _list.Max();
            var min = _list.Min();

            for (int i = min; i < max + 1; i++)
            {
                _sums.Add(SumPathsToNumber(i));

            }

            Console.WriteLine("Result is {0}", _sums.Min());
            Console.ReadKey();
        }

        private static long SumPathsToNumber(Int32 number)
        {
            long sum = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                sum += Math.Abs(number-_list[i]);

            }
            return sum;
        
        }
    }
}
