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

        public static Dictionary<string, string> _pairList = new Dictionary<string, string>();
        public static String _polymerTemplate;
        public Dictionary<string, List<long>> _pairsIndexess = new Dictionary<string, List<long>>();
        public static StringBuilder _builder = new StringBuilder();
        public static StringBuilder _builderChar = new StringBuilder();


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            _polymerTemplate = lines.Where(o => !o.Contains("->")).First();
            foreach (var line in lines.Where(o => o.Contains("->")))
            {
                var pairs = line.Split("->");

                var toinseertList = pairs[0].Trim().ToCharArray().ToList();
                toinseertList.RemoveAt(1);
                toinseertList.Add(pairs[1].Trim().First());
                
                if (!_pairList.ContainsKey(pairs[0].Trim()))
                    _pairList.Add(pairs[0].Trim(), String.Join("", toinseertList));

            }

            for (int i = 0; i < 10; i++)
            {
                _polymerTemplate = PreformStep(_polymerTemplate);
            
            
            }

            var groupings = _polymerTemplate.ToCharArray().GroupBy(o => o).Select(n => new
            {
                Sign = n.Key,
                SignCount = n.Count()
            });

            var max = groupings.Select(o => o.SignCount).Max();
            var min = groupings.Select(o => o.SignCount).Min();

            var result = max - min;

            Console.WriteLine("Max is {0}", max);
            Console.WriteLine("Min is {0}", min);
            Console.WriteLine("Result is {0}", result);
            Console.ReadKey();
        }


        public static String PreformStep(String input)
        {

            _builder.Clear();
            var array = input.ToCharArray();
            var last = input.Last();

            for (int i = 0; i < input.Length - 1; i++)
            {
                _builderChar.Clear();
                var letter1 = array[i];
                var letter2 = array[i + 1];
                _builderChar.Append(letter1);
                _builderChar.Append(letter2);

                var key = _builderChar.ToString();
                _builder.Append(_pairList[key]);

            }

            _builder.Append(last);

            return _builder.ToString();

        }

      

    }
}
