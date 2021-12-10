using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static List<Tuple<Point, Point>> _coordinatePairs = new List<Tuple<Point, Point>>();
        public static int[,] _plotinglinesField = new int[1000, 1000];

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
                _coordinatePairs.Add(ParsePair(line));


            //no filters
            var filteredCoordinates = _coordinatePairs;
            PlotLines(filteredCoordinates);

            Console.Out.WriteLine("Result is {0}", CountOverlapinField());
            Console.ReadKey();

        }


        private static Tuple<Point, Point> ParsePair(String line)
        {
            var linesplited = line.Split("->");
            var cOne = linesplited[0];
            var cTwo = linesplited[1];

            var cOneSplited = cOne.Split(",");
            var cTwoSplited = cTwo.Split(",");

            var pointOne = new Point(int.Parse(cOneSplited[0]), int.Parse(cOneSplited[1]));
            var pointTwo = new Point(int.Parse(cTwoSplited[0]), int.Parse(cTwoSplited[1]));

            return new Tuple<Point, Point>(pointOne, pointTwo);

        }

        private static void PlotLines(List<Tuple<Point, Point>> pairsToPlot)
        {

            foreach (var pair in pairsToPlot )
            {
                var pairFromX = pair.Item1.X < pair.Item2.X ? pair.Item1.X : pair.Item2.X;
                var pairFromY = pair.Item1.Y < pair.Item2.Y ? pair.Item1.Y : pair.Item2.Y;
                var pairToX = pair.Item1.X > pair.Item2.X ? pair.Item1.X : pair.Item2.X;
                var pairToY = pair.Item1.Y > pair.Item2.Y ? pair.Item1.Y : pair.Item2.Y;

                //Crtanje horizontala i vertikala
                if (pairFromX == pairToX || pairFromY == pairToY)
                {
                    for (int i = pairFromX; i < pairToX + 1; i++)
                        for (int j = pairFromY; j < pairToY + 1; j++)
                            _plotinglinesField[i, j]++;
                }
                else
                {
                    //Crtanje diajgonale
                    var distance = Math.Abs(pair.Item1.X - pair.Item2.X);
                    for (int i = 0; i < distance+1; i++)
                    {
                        var stepX = pair.Item1.X < pair.Item2.X ? i : -i;
                        var stepY = pair.Item1.Y < pair.Item2.Y ? i : -i;

                        _plotinglinesField[pair.Item1.X + stepX, pair.Item1.Y + stepY]++;

                    }
                }
            }
        }

        private static int CountOverlapinField()
        {
            var counter = 0;
            for (int i = 0; i < 1000; i++)
                for (int j = 0; j < 1000; j++)
                    if (_plotinglinesField[i, j] >= 2 )
                        counter++;
            return counter;
        }
    }
}
