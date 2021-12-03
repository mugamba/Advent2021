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
            _lines = lines;
            var linelenght = lines.First().Length;



            


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

                if (_countones>= _countzeroes)
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

            var oxygen = FilterArray(_gammaArray.ToArray());
            var scrubber = FilterArray(_epsilonArray.ToArray());



            var oxygenString = new string(oxygen.ToArray());
            var scrubberString = new string(scrubber.ToArray());

            var oxygenDecimal = Convert.ToInt32(oxygenString, 2);
            var scrubberDecimal = Convert.ToInt32(scrubberString, 2); ;

            Console.WriteLine("Result is gamma {0}, epsilon {1}, total {2} ", oxygenDecimal, scrubberDecimal, oxygenDecimal * scrubberDecimal);
            Console.ReadKey();
        }

        public static Array ParseLine(String line)
        {
            return line.ToCharArray();
        }


        public static String FilterArray(char[] array)
        {


            var newarray = _lines.Select(o=>o.ToCharArray()).ToList();

            for (int i = 0; i < array.Length; i++)
            {
                newarray = newarray.Where(o => o[i] == array[i]).ToList();
                if (newarray.Count == 1)
                {
                    return new String(newarray.FirstOrDefault());
                }
            }

            return null;

        }
    }
}
