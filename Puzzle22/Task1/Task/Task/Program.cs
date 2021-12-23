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




          

            //on x = -18..26, y = -33..15, z = -7..46
            //off x = -40..-22, y = -38..-28, z = 23..41
            //-18..26 -27..15 -7..22 


            Console.WriteLine("Result is {0}", _totalCubo.PointsInCube());
        }

       
          

            
            Console.WriteLine("Result {0}", ret.ToString());
            return ret;
        }





        public static IList<Cuboid> OperateOnX(Cuboid input, Cuboid next)
        { 
            if (input.leftx >= next.leftx && input.rightx
                
                
                )

        
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

        public long PointsInCube()
        {
            return Math.Abs(leftx - rightx) * Math.Abs(lefty - righty) * Math.Abs(leftz - rightz);

        }



    }



}
