using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {
        private static int _countones = 0;
        private static List<char> _gammaArray = new List<char>();
        private static List<char> _epsilonArray = new List<char>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var linelenght = lines.First().Length;



            for (int i = 0; i < linelenght; i++)
            {
                _countones = 0;

                foreach (var line in lines)
                {
                    if (line[i] == '1')
                        _countones++;
                }

                if (_countones > (lines.Length / 2))
                {
                    _gammaArray.Add('1');
                    _epsilonArray.Add('0');
                }
                else
                {
                    _gammaArray.Add('0');
                    _epsilonArray.Add('1');
                }
            }

            var gammaString = new string(_gammaArray.ToArray());
            var epsilonString = new string(_epsilonArray.ToArray());

            var gamma = Convert.ToInt32(gammaString, 2);
            var epsilon = Convert.ToInt32(epsilonString, 2); ;

            Console.WriteLine("Result is gamma {0}, epsilon {1}, total {2} ", gamma, epsilon, gamma * epsilon);
            Console.ReadKey();
        }

        public static Array ParseLine(String line)
        {
            return line.ToCharArray();
        }
    }
}
