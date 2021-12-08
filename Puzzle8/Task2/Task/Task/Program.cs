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
        public static long _totalSum = 0;


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
            {
                _totalSum += GetNumberFromDigits(ParseLineGetDigits(line));
            }

            Console.WriteLine("Result is {0}", _totalSum);
            Console.ReadKey();
        }

        private static List<int> ParseLineGetDigits(String line)
        {

            var splits = line.Split("|");


            var numbers = splits[0].Trim().Split(" ");
            List<Tuple<String, int>> list = new List<Tuple<string, int>>();

            list.Add(new Tuple<string, int>(numbers.Where(o => o.Length == 7).First(), 8));
            list.Add(new Tuple<string, int>(numbers.Where(o => o.Length == 2).First(), 1));
            list.Add(new Tuple<string, int>(numbers.Where(o => o.Length == 4).First(), 4));
            list.Add(new Tuple<string, int>(numbers.Where(o => o.Length == 3).First(), 7));

            foreach (var num in numbers.Where(o => o.Length == 6))
            {
                /*Ovo je 9 duzine 6 i sadrzi sve znakove od 1*/
                if (list.Where(o => o.Item2 == 4).First().Item1.All(o => num.Contains(o)))
                {
                    list.Add(new Tuple<string, int>(num, 9));

                }
                /*Ovp je 6 duzine 6 i sadrzi sve iz 1 i ne sadrzi sve iz 4*/
                if (!list.Where(o => o.Item2 == 1).First().Item1.All(o => num.Contains(o)) && !list.Where(o => o.Item2 == 4).First().Item1.All(o => num.Contains(o)))
                {
                    list.Add(new Tuple<string, int>(num, 6));

                }

                //Ovo je 0 duzine 6 i ne sadrzi sve iz 4 a sadrzi sve iz 1
                if (!list.Where(o => o.Item2 == 4).First().Item1.All(o => num.Contains(o)) && list.Where(o => o.Item2 == 1).First().Item1.All(o => num.Contains(o)))
                {
                    list.Add(new Tuple<string, int>(num, 0));
                }

            }

            /*duzina 5, 2,3,5*/
            foreach (var num in numbers.Where(o => o.Length == 5))
            {
                var devetka = list.Where(o => o.Item2 == 9).First().Item1;
                var sadrziznamenki = num.Where(o => devetka.Contains(o)).Count();
                /*9 sadrzi tocno 4 znamenke od broja 2*/
                if (sadrziznamenki == 4)
                    list.Add(new Tuple<string, int>(num, 2));
                else
                {
                    /*3 sadrzi sve znakove od 1*/
                    if (list.Where(o => o.Item2 == 1).First().Item1.All(o => num.Contains(o)))
                        list.Add(new Tuple<string, int>(num, 3));
                    else
                        //nije 2 i nije 3 onda je 5 
                        list.Add(new Tuple<string, int>(num, 5));
                }
            }


            var result = splits[1].Trim().Split(" ");
            List<int> digits = new List<int>();

            foreach (var num in result)
            {

                var number = list.Where(o => o.Item1.Length == num.Length && o.Item1.All(o => num.Contains(o))).Select(o => o.Item2).First();
                digits.Add(number);
            }

            return digits;
        }

        public static long GetNumberFromDigits(List<int> digits)
        {
            long number = 0;

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                    number += 1000 * digits[i];
                if (i == 1)
                    number += 100 * digits[i];
                if (i == 2)
                    number += 10 * digits[i];
                if (i == 3)
                    number += 1 * digits[i];
            }

            return number;
        }

    }

}
