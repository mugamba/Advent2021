using System;
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
                if (i + 1 > lines.Length - 1)
                    break;

                var iznosi = int.Parse(lines[i]);
                var iznosi1 = int.Parse(lines[i + 1]);

                if (iznosi1 > iznosi)
                    counter++;
            }

            Console.WriteLine("Result is {0}", counter);
            Console.ReadKey();
        }
    }
}
