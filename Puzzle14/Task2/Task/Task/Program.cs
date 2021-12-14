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
        public static StringBuilder _builder = new StringBuilder();
        public static StringBuilder _builderChar = new StringBuilder();
        public static int _depth =0;

        public static Dictionary<char, long> _counter = new Dictionary<char, long>();
        public static Dictionary<Tuple<string, int>, Dictionary<char, long>> _memo 
            = new Dictionary<Tuple<string, int>, Dictionary<char, long>>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            _polymerTemplate = lines.Where(o => !o.Contains("->")).First().ToCharArray();
            foreach (var line in lines.Where(o => o.Contains("->")))
            {
                var pairs = line.Split("->");
                var letter = pairs[1].Trim();

                if (!_pairList.ContainsKey(pairs[0].Trim()))
                    _pairList.Add(pairs[0].Trim(), letter[0]);

            }

            //for (int i = 0; i < _polymerTemplate.Length; i++)
            //{

            //    if (i < _polymerTemplate.Length - 1)
            //        PreformStep(_polymerTemplate[i], _polymerTemplate[i + 1], 0);
            //}

            //for (int i = 0; i < _polymerTemplate.Length; i++)
            //{
            //    var c = _polymerTemplate[i];
            //    if (_counter.ContainsKey(c))
            //        _counter[c]++;
            //    else
            //    {
            //        _counter.Add(c, 1);
            //    }
            //}
            PreformStep('C', 'B', 30);
            Console.ReadKey();
        }


        public static Dictionary<Tuple<string, int>, Dictionary<char, long>> PreformStep(char previous, char next, int depth)
        {

            if ()

            if (depth == 0)
            {
                var s = new string(new char[] { previous, next });
                var key = new Tuple<string, int>(s, depth);
                
                 


                _memo.Add(key,  )
            }


            if (depth < 10)
            {

              

                if (!_counter.ContainsKey(k))
                        
            }
            

            var c = _pairList[new string(new char[] { previous, next })];

            if (_counter.ContainsKey(c))
                _counter[c]++;
            else
            {
                _counter.Add(c, 1);
            }


            if (!_memo.ContainsKey(key))
            {
                    
            }
            { 
                var p = PreformStep(previous, c, depth)

                _memo.Add
            
            }


            PreformStep(previous, c, depth++) + PreformStep(c, next, depth++);
            
            if (depth < 10)
                Memo()


        }
    }
}
