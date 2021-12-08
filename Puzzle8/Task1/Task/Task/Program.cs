using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {

        public static List<String> _output = new List<String>();
        public static List<String> _signals = new List<String>();

        public static char[] _charOrder = new char[7];

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
                ParseLine(line);
             

            Console.WriteLine("Result is {0}", _output.Where(o => o.Length == 2 ||
                                                                  o.Length == 4 ||
                                                                  o.Length == 3 ||
                                                                  o.Length == 7).Count());
            Console.ReadKey();
        }

        private static void  ParseLine(String line)
        {

            var splits = line.Split("|");

            _signals.AddRange(splits[0].Trim().Split(" ").ToArray());
            _output.AddRange(splits[1].Trim().Split(" ").ToArray());


        }

        private static void DecodeOutput(String outpu)
        {

            var splits = line.Split("|");

            _signals.AddRange(splits[0].Trim().Split(" ").ToArray());
            _output.AddRange(splits[1].Trim().Split(" ").ToArray());


        }



    }
}
