﻿using System;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var counter = 0;



            for (int i = 0; i < lines.Length; i++)
            {
                if (i > 0)
                {
                    if (int.Parse(lines[i - 1]) < int.Parse(lines[i]))
                        counter++;
                
                }

            }


            Console.WriteLine("Result is {0}", counter);
            Console.ReadKey();

        }
    }
}
