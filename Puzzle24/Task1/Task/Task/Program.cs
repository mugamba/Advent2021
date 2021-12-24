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
        public static int _inputCount = 0;
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


            for (int s = 14; s > 0; s--)
            {
              
                var sLines = _segments[s-1]._lines;
                var validRange = new List<int>();
                validRange.Add(0);
                if (s<14)
                    validRange = _segments[s].combinationswZ.Select(o => o.Item1).ToList();



               int leftz = 0; int rightz = 0;
                if (s < 14)
                {
                    leftz = _segments[s].minz();
                    rightz = _segments[s].maxz();
                }

                for (int z = 0; z < 10000; z++)
                    for (int w = 0; w < 10; w++)
                    {
                        _regiseters["x"] = 0;
                        _regiseters["y"] = 0;
                        _regiseters["z"] = z;
                        _input[0] = w;

                        foreach (var ssLines in sLines)
                        {
                            ParseLine(ssLines);
                        }

                        if ( )
                        {
                            _segments[s-1].combinationswZ.Add(new Tuple<int, int>(z, w));
                        }

                    }
            }
            
        

            Console.WriteLine("Result is {0}", _input);

        }


        


        public static void ParseLine(String line)
        {

            if (line.StartsWith("inp"))
            {
                _regiseters[line.Split(" ")[1]] = _input[_inputCount];
            }

            if (line.StartsWith("add"))
            {
                var value = 0;
                var reg = line.Split(" ")[1];
                var token = line.Split(" ")[2];
                value = int.TryParse(token, out value) ? value : _regiseters[token];
                _regiseters[reg] = _regiseters[reg] + value;
            }

            if (line.StartsWith("mul"))
            {
                var value = 0;
                var reg = line.Split(" ")[1];
                var token = line.Split(" ")[2];
                value = int.TryParse(token, out value) ? value : _regiseters[token];
                _regiseters[reg] = _regiseters[reg] * value;
            }

            if (line.StartsWith("mod"))
            {
                var value = 0;
                var reg = line.Split(" ")[1];
                var token = line.Split(" ")[2];
                value = int.TryParse(token, out value) ? value : _regiseters[token];
                _regiseters[reg] = _regiseters[reg] % value;
            }

            if (line.StartsWith("div"))
            {
                var value = 0;
                var reg = line.Split(" ")[1];
                var token = line.Split(" ")[2];
                value = int.TryParse(token, out value) ? value : _regiseters[token];
                _regiseters[reg] = _regiseters[reg] / value;
            }

            if (line.StartsWith("eql"))
            {
                var value = 0;
                var reg = line.Split(" ")[1];
                var token = line.Split(" ")[2];
                value = int.TryParse(token, out value) ? value : _regiseters[token];
                _regiseters[reg] = _regiseters[reg] == value ? 1 : 0;
            }

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
