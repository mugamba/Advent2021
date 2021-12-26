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
        public static Dictionary<string, int> _regiseters = new Dictionary<string, int>();
        public static int[] _input = new int[14];

        public static int[] _x = new int[14];
        public static int[] _y = new int[14];
        public static int[] _z = new int[14];
        public static List<String> _validResults = new List<String>();


        public static List<Segment> _segments = new List<Segment>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            for (int s = 0; s < 14; s++)
            {
                var seg = new Segment();
                seg._lines = lines.Skip(s * 18).Take(18).ToArray();
                _segments.Add(seg);
            }


            _x = _segments.Select(o => int.Parse(o._lines.Skip(5).Take(1).First().Split(" ")[2])).ToArray(); 
            _y = _segments.Select(o => int.Parse(o._lines.Skip(15).Take(1).First().Split(" ")[2])).ToArray();
            _z = _segments.Select(o => int.Parse(o._lines.Skip(4).Take(1).First().Split(" ")[2])).ToArray();


            searchdigits(0, 0, "");

        }


        public static void searchdigits(int depth, long z,  string result)
        {
            Console.WriteLine(depth);

            if (depth == 14)
            {
                if (z == 0)
                {
                    _validResults.Add(result);
                }
                return;
            }
          

            for (int i = 9; i > 0; i--)
            {

                result = result + i.ToString();
                var newz = calculateZ(depth, z, i);
                if (newz == long.MaxValue)
                    continue;

                searchdigits(depth + 1, newz, result);

            }
            return;
        }


        public static long calculateZ(int depth, long z, int w)
        {
            if (_z[depth] == 26)
            {
                if (z % 26 + _x[depth] != w)
                    return long.MaxValue;
            }

            if (_z[depth] == 26)
                return z / 26;
            else
                return z * 26 + _y[depth] + w;
        }

        public class Segment
        {
            public String[] _lines;
            public List<Tuple<int, int>> combinationswZ = new List<Tuple<int, int>>();
            public int minz()
            {
                return combinationswZ.Select(o => o.Item1).Min();
            }

            public int maxz()
            {
                return combinationswZ.Select(o => o.Item1).Max();
            }
        }









    }



}
