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
        public static char[,] _expandedImage;
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

            
            _expandedImage = new char[_x + 4, _y + 4];

            for (int j = 0; j < _expandedImage.GetLength(1); j++)
                for (int i = 0; i < _expandedImage.GetLength(0); i++)
                    _expandedImage[i, j] = '.';

            for (int j = 0; j < _inputImage.GetLength(1); j++)
                for (int i = 0; i < _inputImage.GetLength(0); i++)
                    _expandedImage[2 + i, 2 + j] = _inputImage[i, j];

            var bordl = 1;
            var bordr = 4 + _inputImage.GetLength(0) - 1;

            var newArray1 = (char[,])_expandedImage.Clone();
            for (int j = bordl; j < bordr; j++)
                for (int i = bordl; i < bordr; i++)
                    newArray1[i, j] = GetRealSign(i, j, _expandedImage);


            var charInit = newArray1[0,0] == '.' ? _inputFilter[0] : _inputFilter[511];

            for (int j = 0; j < newArray1.GetLength(1);j++)
                for (int i = 0; i < newArray1.GetLength(0);i++)
                    if (i == 0 || j == 0 || i == newArray1.GetLength(0) - 1 || j == newArray1.GetLength(0) -1)
                        newArray1[i, j] = charInit;


           var newExpande = new char[newArray1.GetLength(0) + 4, newArray1.GetLength(1) + 4];

            for (int j = 0; j < newExpande.GetLength(1); j++)
                for (int i = 0; i < newExpande.GetLength(0); i++)
                    newExpande[i, j] = charInit;

            for (int j = 0; j < newArray1.GetLength(1); j++)
                for (int i = 0; i < newArray1.GetLength(0); i++)
                    newExpande[2 + i, 2 + j] = newArray1[i, j];

          //  PrintImage(newExpande);

            bordl = 1;
            bordr = 4 + newArray1.GetLength(0) - 1;

            var newArray2 = (char[,])newExpande.Clone();
            for (int j = bordl; j < bordr; j++)
                for (int i = bordl; i < bordr; i++)
                    newArray2[i, j] = GetRealSign(i, j, newExpande);

            charInit = newArray2[0, 0] == '.' ? _inputFilter[0] : _inputFilter[511];
            newExpande = new char[newArray2.GetLength(0) + 4, newArray2.GetLength(1) + 4];
            
            
            for (int j = 0; j < newArray2.GetLength(1); j++)
                for (int i = 0; i < newArray2.GetLength(0); i++)
                    if (i == 0 || j == 0 || i == newArray2.GetLength(0) - 1 || j == newArray2.GetLength(0) - 1)
                        newArray2[i, j] = charInit;




            var matches = 0;
            for (int j = 0; j < newArray2.GetLength(1); j++)
                for (int i = 0; i < newArray2.GetLength(0); i++)
                {
                    if (newArray2[i, j] == '#')
                    {
                        matches++;
                    }
                
                }

            Console.WriteLine("Result is {0}", matches);

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
