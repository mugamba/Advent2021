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
        public static List<Cuboid> _cuboids = new List<Cuboid>();
        public static Cuboid _totalCubo;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
            {

                var cubo = new Cuboid();
                var splits = line.Split(" ");
                cubo.opp = splits[0];

                 var nextssplits = splits[1].Split(",");

                var stringx = nextssplits[0].Replace("x=", "");
                var splitx = stringx.Split("..");
                cubo.leftx = int.Parse(splitx[0]);
                cubo.rightx = int.Parse(splitx[1]);
                var stringy =  nextssplits[1].Replace("y=", "");
                var splity = stringy.Split("..");
                cubo.lefty = int.Parse(splity[0]);
                cubo.righty = int.Parse(splity[1]);
                var stringz =  nextssplits[2].Replace("z=", "");
                var splitz = stringz.Split("..");
                cubo.leftz = int.Parse(splitz[0]);
                cubo.rightz = int.Parse(splitz[1]);


                _cuboids.Add(cubo);

            }


            _totalCubo = _cuboids.First();

            foreach (var cubo in _cuboids)
            {
                _totalCubo = Operate(_totalCubo, cubo);
            }

        }

        public static Cuboid Operate(Cuboid input, Cuboid next)
        {
            var ret = new Cuboid();
            ret.leftx = input.leftx;
            ret.rightx = input.rightx;
            ret.lefty = input.lefty;
            ret.righty = input.righty;
            ret.leftz = input.leftz;
            ret.rightz = input.rightz;


            if (next.opp == "on")
            {
                if (next.leftx < input.leftx)
                    ret.leftx = next.leftx;

                if (next.lefty < input.lefty)
                    ret.lefty = next.lefty;

                if (next.leftz < input.leftz)
                    ret.leftz = next.leftz;

                if (next.rightx > input.rightx)
                    ret.rightx = next.rightx;

                if (next.righty > input.righty)
                    ret.righty = next.righty;

                if (next.rightz > input.rightz)
                    ret.rightz = next.rightz;
            }

            if (next.opp == "off")
            {
                if (next.leftx <= input.leftx && next.rightx >= input.rightx)                    
                {
                    ret.leftx = 0;
                    ret.rightx = 0;
                }

                if (next.leftx <= input.leftx && next.rightx < input.rightx)
                    ret.leftx = next.rightx + 1;

                if (next.leftx > input.leftx && next.rightx >= input.rightx)
                    ret.rightx = next.leftx -1;


                if (next.lefty <= input.lefty && next.righty >= input.righty)                    
                {
                    ret.lefty = 0;
                    ret.righty = 0;
                }

                if (next.lefty <= input.lefty && next.righty < input.righty)
                    ret.lefty = next.righty + 1;

                if (next.lefty > input.lefty && next.righty >= input.righty)
                    ret.righty = next.lefty - 1;

                if (next.leftz <= input.leftz && next.rightz >= input.rightz)                    
                {
                    ret.leftz = 0;
                    ret.rightz = 0;
                }

                if (next.leftz <= input.leftz && next.rightz < input.rightz)
                    ret.leftz = next.rightz + 1;

                if (next.leftz > input.leftz && next.rightz >= input.rightz)
                    ret.rightz = next.leftz - 1;

            }




            return ret;
        }



       



    }

    public class Cuboid
    {
        public string opp;
        public int leftx;
        public int rightx;
        public int lefty;
        public int righty;
        public int leftz;
        public int rightz;


        public string ToString()
        {
            return String.Format("x={0}..{1}, y={2}..{3}, z={4}..{5}", leftx, rightx, lefty, righty, leftz, rightz);
        
        }

    }



}
