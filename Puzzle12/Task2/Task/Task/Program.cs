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

        public static Dictionary<string, List<string>> _cavernMap = new Dictionary<string, List<string>>();
        public static List<List<string>> _paths = new List<List<string>>();
        public static List<string> _forPrints = new List<string>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            /*Parsing*/
            foreach (var line in lines)
            {
                var caves = line.Split("-");

                if (!_cavernMap.ContainsKey(caves[0]))
                    _cavernMap.Add(caves[0], new List<string>());

                if (!_cavernMap[caves[0]].Contains(caves[1]));
                _cavernMap[caves[0]].Add(caves[1]);

                if (!_cavernMap.ContainsKey(caves[1]))
                    _cavernMap.Add(caves[1], new List<string>());

                if (!_cavernMap[caves[1]].Contains(caves[0])) 
                _cavernMap[caves[1]].Add(caves[0]);
            }


            TraversePath(new List<string>(), "start");

            //foreach (var line in _paths)
            //{
            //   _forPrints.Add(String.Join(',', line.ToArray()));
            //}

            //foreach (var line in _forPrints.OrderBy(o => o))
            //{
            //    Console.WriteLine(line);
            //}
               

            Console.WriteLine("Result is {0}", _paths.Count);
            Console.ReadKey();
        }


        public static List<string> TraversePath(List<string> currentlist, String toNode)
        {
            if (currentlist.Contains("start") && toNode == "start")
                return null;

            if (toNode == "end")
            {
                currentlist.Add(toNode);
                return currentlist;
            }

            if (toNode.All(o => char.IsLower(o)))
            {
                var alreadyTwo = currentlist.Where(o=>o.All(c=>char.IsLower(c))).GroupBy(c => c)
                 .Where(grp => grp.Count() > 1)
                 .Select(grp => grp.Key).FirstOrDefault();

                if (alreadyTwo != null && alreadyTwo == toNode)
                    return null;


                if (alreadyTwo != null && alreadyTwo != toNode)
                {
                    if (currentlist.Contains(toNode))
                        return null;
     
                }

            }
            
            currentlist.Add(toNode);
            foreach (var cavern in _cavernMap[toNode])
            {
                var list = TraversePath(currentlist.ToList(), cavern);
                if (list != null)
                {
                    _paths.Add(list);
                } 
            }
            return null;
        }

      

    }
}
