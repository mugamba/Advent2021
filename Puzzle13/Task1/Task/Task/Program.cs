using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static bool[,] _transparentPaper;
        public static int _x;
        public static int _y;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            _x = 655*2+1;
            _y = 447*2+1;
            _transparentPaper = new bool[_x, _y];


            foreach (var line in lines.Where(o => !o.Contains("fold")))
            {
                if (line == String.Empty)
                    continue;


                var splits = line.Split(",");

                var x = int.Parse(splits[0]);
                var y = int.Parse(splits[1]);

                _transparentPaper[x, y] = true; 

            }

            var foldLine = lines.Where(o => o.Contains("fold")).First();

            if (foldLine.Contains("x"))
            {
                var result = FoldOnXLine(int.Parse(foldLine.Split("=")[1]), _transparentPaper);
                Console.WriteLine("Result is {0}", CountHashes(result));
            }
            else
            {
                var result = FoldOnYLine(int.Parse(foldLine.Split("=")[1]), _transparentPaper);
                Console.WriteLine("Result is {0}", CountHashes(result));

            }

            Console.ReadKey();
        }


        public static void PrintPaper(bool[,] paper)
        {

            for (int j = 0; j < paper.GetLength(1); j++)
                {
                for (int i = 0; i < paper.GetLength(0); i++)
                {

                    if (paper[i, j])
                        Console.Write("#");
                    else
                        Console.Write(".");

                }
                Console.WriteLine();

            }

        }

        public static Int32 CountHashes(bool[,] paper)
        {
            var count = 0;
            for (int j = 0; j < paper.GetLength(1); j++)
            {
                for (int i = 0; i < paper.GetLength(0); i++)
                {

                    if (paper[i, j])
                        count++;
                 

                }
                
            }
            return count;
        }


        public static bool[,] FoldOnYLine(int line, bool[,] paper)
        {
            var x = paper.GetLength(0);
            var y = paper.GetLength(1);

            bool[,] newArray = new bool[x, line];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < line; j++)
                {
                    newArray[i, j] = paper[i, j] || paper[i, y - 1 - j];         
                }
            }

            return newArray;
        }

        public static bool[,] FoldOnXLine(int line, bool[,] paper)
        {
            var x = paper.GetLength(0);
            var y = paper.GetLength(1);

            bool[,] newArray = new bool[line, y];

            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    newArray[i, j] = paper[i, j] || paper[x-1-i, j];
                }
            }

            return newArray;
        }






    }
}
