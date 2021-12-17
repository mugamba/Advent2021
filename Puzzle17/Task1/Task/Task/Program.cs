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


        public static Tuple<int, int> _validRangeX;
        public static Tuple<int, int> _validRangeY;
        public static int _maxHeight = 0;

        static void Main(string[] args)
        {
            var line = File.ReadAllLines("input.txt").First();
            var splits = line.Split(":");
            var cLine = splits.Last().Trim();
            var cLineSplits = cLine.Split(",");
            var xline = cLineSplits.First().Trim();
            var yline = cLineSplits.Last().Trim();
            _validRangeX = new Tuple<int, int>(int.Parse(xline.Replace("x=", "").Split("..")[0]), int.Parse(xline.Split("..")[1]));
            _validRangeY = new Tuple<int, int>(int.Parse(yline.Replace("y=", "").Split("..")[0]), int.Parse(yline.Split("..")[1]));


            for (int x = 0; x < _validRangeX.Item1+1; x++)
                for (int y = _validRangeY.Item1; y < Math.Abs(_validRangeX.Item1)+1; y++)
                {
                    var nextX = 0; var nextY = 0; var height = 0;
                    var stepX = x; 
                    var stepY = y;
                    while (true)
                    {
                        if (_validRangeX.Item1 <= nextX && _validRangeX.Item2 >= nextX
                            && _validRangeY.Item1 <= nextY && _validRangeY.Item2 >= nextY)
                        {
                              if (height > _maxHeight)
                                _maxHeight = height;

                        }

                        if (nextY < _validRangeY.Item1 || nextX > _validRangeX.Item2)
                            break;

                        nextX += stepX; nextY += stepY;
                        if (stepY > 0)
                        {
                            height += stepY;
                        }
                        stepY--;
                        if ( stepX > 0)
                            stepX--;
                    }
                }
            Console.WriteLine("Result is {0} ", _maxHeight);
            Console.ReadKey();
        }
    }
}
