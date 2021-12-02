using System;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {
        private static long _x = 0;
        private static long _y = 0;
        private static long _z = 0;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
                ParseLine(line);

            Console.WriteLine("Result is forward {0}, depth {1}, total {2} ", _x, _y, _x * _y);
            Console.ReadKey();
        }


        public static void ParseLine(String line)
        {
            var splitedLine = line.Split(' ');

            if (splitedLine[0] == "forward")
            {
                _x += int.Parse(splitedLine[1]);
                _y += _z * int.Parse(splitedLine[1]);
            }

            if (splitedLine[0] == "down")
                _z += int.Parse(splitedLine[1]);

            if (splitedLine[0] == "up")
                _z -= int.Parse(splitedLine[1]);

        }
    }
}
