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

        public static Dictionary<string, char> _pairList = new Dictionary<string, char>();
        public static char[] _polymerTemplate;
        public Dictionary<string, List<long>> _pairsIndexess = new Dictionary<string, List<long>>();
        public static Dictionary<Tuple<string, int>, Dictionary<char, long>> _memo 
            = new Dictionary<Tuple<string, int>, Dictionary<char, long>>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            _polymerTemplate = lines.Where(o => !o.Contains("->")).First().Trim().ToCharArray();
            foreach (var line in lines.Where(o => o.Contains("->")))
            {
                var pairs = line.Split("->");
                var letter = pairs[1].Trim();

                if (!_pairList.ContainsKey(pairs[0].Trim()))
                    _pairList.Add(pairs[0].Trim(), letter[0]);

            }

            var dictionary = new Dictionary<char, long>();
            for (int i = 0; i < _polymerTemplate.Length; i++)
            {

                if (i < _polymerTemplate.Length - 1)
                {
                    var dictionary1 = PreformStep(_polymerTemplate[i], _polymerTemplate[i + 1], 40, 40);
                    dictionary = dictionary.Concat(dictionary1).GroupBy(
                                kvp => kvp.Key,
                                (key, kvps) => new { Key = key, Value = kvps.Sum(kvp => kvp.Value) })
                            .ToDictionary(x => x.Key, x => x.Value);

                }
            }
            /*adding last because recursion removes it between tokens*/
            dictionary[_polymerTemplate.Last()]++;


            var max = dictionary.Select(o => o.Value).Max();
            var min = dictionary.Select(o => o.Value).Min();
            var result = max - min;

            Console.WriteLine("Max is {0}", max);
            Console.WriteLine("Min is {0}", min);
            Console.WriteLine("Result is {0}", max - min);
            Console.ReadKey();
        }


        public static Dictionary<char, long> PreformStep(char previous, char next, int depth, int maxdepth)
        {
            if (depth == 0)
            {
                var dict = new Dictionary<char, long>();
                dict.Add(previous, 1);
                if (dict.ContainsKey(next))
                    dict[next]++;
                else
                    dict.Add(next, 1);
                return dict;
            }

            var s = new string(new char[] { previous, next });
            var key = new Tuple<string, int>(s, depth);
            var mid = _pairList[new string(new char[] { previous, next })];

            if (_memo.ContainsKey(key))
                return _memo[key];

            var newdepth = depth - 1;

            var dict1 = PreformStep(previous, mid, newdepth, maxdepth);
            var dict2 = PreformStep(mid, next, newdepth, maxdepth);
            var newdict = dict1.Concat(dict2).GroupBy(
                kvp => kvp.Key,
                (key, kvps) => new { Key = key, Value = kvps.Sum(kvp => kvp.Value) })
            .ToDictionary(x => x.Key, x => x.Value);
            /*We added mid tow times*/
            newdict[mid]--;

            /*removing the last because its already in new token*/
            if (depth == maxdepth)
                newdict[next]--;

            _memo.Add(key, newdict);
            return _memo[key];

        }

    }


    
}
