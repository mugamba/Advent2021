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
        public static char[,] _inputImage;
        public static char[] _inputFilter;
        public static int _x;
        public static int _y;


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var drawnlines = lines.First();
            _inputFilter = drawnlines.ToCharArray();


            var inputimageLines = lines.Skip(2).ToList();
            _x = inputimageLines.First().Length;
            _y = inputimageLines.Count;

            _inputImage = new char[_x, _y];

            for (int j = 0; j < _y; j++)
                for (int i = 0; i < _x; i++)
                    _inputImage[i, j] = inputimageLines[j].ToCharArray()[i];

            //  PrintImage(_inputImage);




            var inputArray = _inputImage;
            for (int i = 0; i < 50; i++)
            {
                inputArray = FilterArray(inputArray, i);
                //PrintImage(inputArray);
            }

            

            var matches = 0;
            for (int j = 0; j < inputArray.GetLength(1); j++)
                for (int i = 0; i < inputArray.GetLength(0); i++)
                {
                    if (inputArray[i, j] == '#')
                    {
                        matches++;
                    }
                
                }

            Console.WriteLine("Result is {0}", matches);

        }


        public static char[,] FilterArray(char[,] inputArray, int step)
        {
            var expandedArray = new char[inputArray.GetLength(0) + 4, inputArray.GetLength(1) + 4];

            var charInit = '.';
            if (step > 0)
            {
                charInit = inputArray[0, 0];
            }

            for (int j = 0; j < expandedArray.GetLength(1); j++)
                for (int i = 0; i < expandedArray.GetLength(0); i++)
                    expandedArray[i, j] = charInit;

            for (int j = 0; j < inputArray.GetLength(1); j++)
                for (int i = 0; i < inputArray.GetLength(0); i++)
                    expandedArray[2 + i, 2 + j] = inputArray[i, j];

            var bordl = 1;
            var bordr = 4 + inputArray.GetLength(0) - 1;

            var newArray = (char[,])expandedArray.Clone();
            for (int j = bordl; j < bordr; j++)
                for (int i = bordl; i < bordr; i++)
                    newArray[i, j] = GetRealSign(i, j, expandedArray);


            charInit = newArray[0, 0] == '.' ? _inputFilter[0] : _inputFilter[511];

            for (int j = 0; j < newArray.GetLength(1); j++)
                for (int i = 0; i < newArray.GetLength(0); i++)
                    if (i == 0 || j == 0 || i == newArray.GetLength(0) - 1 || j == newArray.GetLength(0) - 1)
                        newArray[i, j] = charInit;

            return newArray;

        }

        private static char GetRealSign(int i, int j, char[, ] array)
        {
            var builder = new StringBuilder();
            builder.Append(array[i - 1, j - 1] == '.' ? '0' : '1');
            builder.Append(array[i , j - 1] == '.' ? '0' : '1');
            builder.Append(array[i +1, j - 1] == '.' ? '0' : '1');
            builder.Append(array[i - 1, j] == '.' ? '0' : '1');
            builder.Append(array[i, j] == '.' ? '0' : '1');
            builder.Append(array[i+1, j] == '.' ? '0' : '1');
            builder.Append(array[i-1, j+1] == '.' ? '0' : '1');
            builder.Append(array[i, j + 1] == '.' ? '0' : '1');
            builder.Append(array[i+1, j+1] == '.' ? '0' : '1'); 
            var index = Convert.ToInt32(builder.ToString(), 2);

            return _inputFilter[index];
        }

        public static void PrintImage(char[,] image)
        {
            var x = image.GetLength(0);
            var y = image.GetLength(1);

            for (int j = 0; j < x; j++)
            {
                for (int i = 0; i < y; i++)
                {
                    Console.Write(image[i, j]);
                }

                Console.WriteLine();


            }

        }

    }
}
