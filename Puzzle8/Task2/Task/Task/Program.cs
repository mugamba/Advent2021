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
                ParseLine(line);
             

            Console.WriteLine("Result is {0}", _totalSum);
            Console.ReadKey();
        }

        private static void  ParseLine(String line)
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
                /**/
                if (!list.Where(o => o.Item2 == 1).First().Item1.All(o => num.Contains(o)) && !list.Where(o => o.Item2 == 4).First().Item1.All(o => num.Contains(o)))
                {
                    list.Add(new Tuple<string, int>(num, 6));

                }

                if (!list.Where(o => o.Item2 == 4).First().Item1.All(o => num.Contains(o)) && list.Where(o => o.Item2 == 1).First().Item1.All(o => num.Contains(o)))
                {
                    list.Add(new Tuple<string, int>(num, 0));
                }

            }

            foreach (var num in numbers.Where(o => o.Length == 5))
            {
                var devetka = list.Where(o => o.Item2 == 9).First().Item1;
                var sadrziznamenki = num.Where(o => devetka.Contains(o)).Count();
                if (sadrziznamenki == 4)
                    list.Add(new Tuple<string, int>(num, 2));
                else
                {
                    if (list.Where(o => o.Item2 == 1).First().Item1.All(o => num.Contains(o)))
                        list.Add(new Tuple<string, int>(num, 3));
                    else
                        list.Add(new Tuple<string, int>(num, 5));
                }
            }


            var numbers1 = splits[1].Trim().Split(" ");
            List<int> outt = new List<int>();

            foreach (var num in numbers1)
            {

               var number = list.Where(o => o.Item1.Length == num.Length && o.Item1.All(o => num.Contains(o))).Select(o => o.Item2).First();
               outt.Add(number);
            
            }

            for (int i = 0; i < outt.Count; i++)
            {
                if (i == 0)
                {
                    _totalSum += 1000 * outt[i];
                
                }

                if (i == 1)
                {
                    _totalSum += 100 * outt[i];


                }

                if (i == 2)
                {
                    _totalSum += 10 * outt[i];


                }

                if (i == 3)
                {
                    _totalSum += 1 * outt[i];


                }



            }




        }

        //private static void DecodeOutput(String outpu)
        //{

        //    var splits = line.Split("|");

        //    _signals.AddRange(splits[0].Trim().Split(" ").ToArray());
        //    _output.AddRange(splits[1].Trim().Split(" ").ToArray());


        //}



    }
}
