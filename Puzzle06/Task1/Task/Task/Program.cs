using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static List<int> _fishTank = new List<int>();
        
        static void Main(string[] args)
        {
            var line = File.ReadAllLines("input.txt").First();

            var array = line.Split(",");

            _fishTank.AddRange(array.Select(o => int.Parse(o)));

            for (int i = 0; i < 256; i++)
            {
               var toAdd = _fishTank.Where(o => o == 0).Select(o=>8).ToArray();


                for (int j=0;j<_fishTank.Count;j++)
                {
                    if (_fishTank[j] == 0)
                        _fishTank[j] = 6;
                    else
                        _fishTank[j]--;

                }

                _fishTank.AddRange(toAdd);               

            }

            Console.WriteLine("Result is {0}", _fishTank.Count);
            Console.ReadKey();
        }


     

           
        
    }
}
