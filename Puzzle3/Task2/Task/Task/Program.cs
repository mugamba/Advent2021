using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {
        private static int _countones = 0;
        private static int _countzeroes = 0;

        private static List<char> _gammaArray = new List<char>();
        private static List<char> _epsilonArray = new List<char>();
        private static String[] _lines;



        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            String oxygenString = null; String scrubberString = null;


            var newArray = lines.Select(o => o.ToCharArray()).ToList();
            CalculateGamma(newArray);

            for (int i = 0; i<lines[0].Length;i++)
            {
                newArray = newArray.Where(o => o[i] == _gammaArray[i]).ToList();
                if (newArray.Count == 1)
                {
                    oxygenString = new string(newArray.FirstOrDefault());
                    break;
                }

                CalculateGamma(newArray);
            }

            var newArray1 = lines.Select(o => o.ToCharArray()).ToList();
            CalculateGamma(newArray1);

            for (int i = 0; i < lines[0].Length; i++)
            {
                newArray1 = newArray1.Where(o => o[i] == _epsilonArray[i]).ToList();
                if (newArray1.Count == 1)
                {
                    scrubberString = new string(newArray1.FirstOrDefault());
                    break;
                }

                CalculateGamma(newArray1);
            }
           
            var oxygenDecimal = Convert.ToInt32(oxygenString, 2);
            var scrubberDecimal = Convert.ToInt32(scrubberString, 2); ;

            Console.WriteLine("Result is oxygenDecimal {0}, scrubberDecimal {1}, total {2} ", oxygenDecimal, scrubberDecimal, oxygenDecimal * scrubberDecimal);
            Console.ReadKey();
        }

        private static void CalculateGamma(List<Char[]> lines)
        {
           
            var linelenght = lines.First().Length;
            _gammaArray.Clear();
            _epsilonArray.Clear();

            for (int i = 0; i < linelenght; i++)
            {
                _countones = 0;
                _countzeroes = 0;

                foreach (var line in lines)
                {
                    if (line[i] == '1')
                        _countones++;
                    else
                        _countzeroes++;
                }

                if (_countones >= _countzeroes)
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
        }
    }
}
