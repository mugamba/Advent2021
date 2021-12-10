using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static List<char> openings = new List<char> { '(', '[', '<', '{' };
        public static List<char> closings = new List<char> { ')', ']', '>', '}' };
     
        public static List<int> wrongLines = new List<int>();
        public static List<char[]> _endings = new List<char[]>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            
            for (int i = 0; i < lines.Length; i++)
            {
                /*last in first out*/
                var stack = new Stack<char>();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var current = lines[i].ToCharArray()[j];
                    if (openings.Contains(current))
                        stack.Push(current);
                    else
                    {
                        if (stack.Peek() == openings[closings.IndexOf(current)])
                            stack.Pop();
                        else
                        {
                            wrongLines.Add(i);
                            break;
                        }

                    }
                    if (j == lines[i].Length - 1)
                        _endings.Add(stack.ToArray<char>().Select(o=> closings[openings.IndexOf(o)]).ToArray());
                }
            }

            var list = new List<long>();
            foreach (var ending in _endings)
                list.Add(ScoreLine(ending));
    
            var orderedList =  list.OrderBy(o => o).ToList();
            var count = orderedList.Count;
            var median = count / 2;

            Console.WriteLine("Result is {0}", orderedList[median]);
            Console.ReadKey();
        }

        public static long ScoreLine(char[] ending)
        {
            long sum = 0;
            for (int i = 0; i < ending.Length; i++)
            {
                sum = sum * 5;
                if (ending[i] == ')')
                    sum += 1;

                if (ending[i] == ']')
                    sum += 2;

                if (ending[i] == '}')
                    sum += 3;

                if (ending[i] == '>')
                    sum += 4;
            }
            return sum;
        }
    }
}
