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
                var i1 = i;
                var i2 = i+1;
                var i3 = i+2;
                var i4 = i+3;

                if (i4 > lines.Length - 1)
                    break;

                var sum1 = int.Parse(lines[i1]) + int.Parse(lines[i2]) + int.Parse(lines[i3]);
                var sum2 = int.Parse(lines[i2]) + int.Parse(lines[i3]) + int.Parse(lines[i4]);

                if (sum2 > sum1) counter++;
            
            }

            Console.WriteLine("Result is {0}", counter);
            Console.ReadKey();

        }
    }
}
