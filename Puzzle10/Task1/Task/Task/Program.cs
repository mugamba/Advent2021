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
        public static List<char> wrongendings = new List<char>();

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
                            wrongendings.Add(current);
                            break;
                        }

                    }
                }
            }

            var sum = 0;
            foreach (var wrong in wrongendings)
            {
                if (wrong == ')')
                    sum += 3;

                if (wrong == ']')
                    sum+=57;

                if (wrong == '}')
                    sum += 1197;

                if (wrong == '>')
                    sum += 25137;

            }

            Console.WriteLine("Result is {0}", sum);
            Console.ReadKey();
        }


       
    }
}
