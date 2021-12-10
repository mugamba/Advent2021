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
        public static List<Tuple<long,long>> _fishTank = new List<Tuple<long, long>>();
        
        static void Main(string[] args)
        {
            var line = File.ReadAllLines("input.txt").First();


            _fishTank.AddRange(FishGroup(line));
            var toAdd = new List<long>();

            for (int i = 0; i < 256; i++)
            {

               var toadd = _fishTank.Where(o => o.Item1 == 0).Select(o => o.Item2).FirstOrDefault();

                for (int j = 0; j < _fishTank.Count; j++)
                {

                    if (_fishTank[j].Item1 == 0)
                        _fishTank[j] = new Tuple<long, long>(6, _fishTank[j].Item2);
                    else
                        _fishTank[j] = new Tuple<long, long>(_fishTank[j].Item1 - 1, _fishTank[j].Item2);

                }



                _fishTank = _fishTank.GroupBy(o=>o.Item1).Select(g => new Tuple<long, long>(g.Key, g.Sum(o=>o.Item2))).ToList();


                if (toadd > 0)
                    _fishTank.Add(new Tuple<long, long>(8, toadd));


             

            }

            Console.WriteLine("Result is {0}", _fishTank.Sum(o=>o.Item2));
            Console.ReadKey();
        }

        /*Spašava tuple, treba sam brojiti grupe*/
        public static Tuple<long, long>[] FishGroup(String line)
        {
            var array = line.Split(",");
            return array.Select(o => int.Parse(o)).GroupBy(o => o).Select(g => new Tuple<long, long>(g.Key, g.Count())).ToArray();

        }


     

           
        
    }
}
