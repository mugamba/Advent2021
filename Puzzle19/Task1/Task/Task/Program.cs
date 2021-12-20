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

        public static List<Scanner> _scanners = new List<Scanner>();
        public static Dictionary<Scanner, Tuple<int, int, int>> _offsets = new Dictionary<Scanner, Tuple<int, int, int>>();
        public static List<Tuple<int, int, int>> _beacons = new List<Tuple<int, int, int>>();
        

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            Scanner scanner = null;
            foreach (var line in lines)
            {

                if (line.StartsWith("---"))
                {
                    scanner = new Scanner();
                    scanner._name = line;
                    continue;
                
                }

                if (String.IsNullOrEmpty(line))
                {
                    _scanners.Add(scanner);
                    continue;
                }

                var splits =  line.Split(",");
                scanner._beaconsList.Add(new Tuple<int, int, int>(int.Parse(splits[0]), int.Parse(splits[1]), int.Parse(splits[2])));
                //_testList



            }

            if (scanner != null)
                 _scanners.Add(scanner);
            
            var scannerSource =_scanners.Where(o => o._name == "--- scanner 0 ---").First();
            _offsets.Add(scannerSource, new Tuple<int, int, int>(0, 0, 0));


           
            var compareList =  _scanners.ToList();

            while (scannerSource != null)
            {

                scannerSource = _offsets.OrderBy(o => o.Key._comparedWith.Count).Select(o=>o.Key).First();
                var toCompareList = compareList.Where(o => o != scannerSource && !scannerSource._comparedWith.Contains(o)).ToList();

                if (toCompareList.Count == 0)
                    break;

                foreach (var toCompare in toCompareList)
                {
                    var isMetched = IsMatch(scannerSource, toCompare);
                    if (isMetched != null)
                    {
                        scannerSource = isMetched;
                        break;
                    }
                }

            }

            PrintList(_beacons);
            Console.ReadKey();
                //("Result is {0}", _beacons.Count());
            
        }

        private static Scanner IsMatch(Scanner scannerSource, Scanner toCompare)
        {
            scannerSource._comparedWith.Add(toCompare);

            List<int> validofsetsX = new List<int>();
            for (int offX = -4000; offX < 4000; offX++)
                for (int i = 0; i < 24; i++)
                {
                    var rotatedList = toCompare._beaconsList.Select(o => RotatePointsInList(o, i))
                         .Select(o => o.Item1 + offX).ToList();

                    /*winner*/
                    var list = rotatedList.Where(o => scannerSource._beaconsList.Any(g => g.Item1 == o)).ToList();
                    if (list.Count >= 12)
                    {
                        validofsetsX.Add(offX);
                        break;
                    }
                }

            List<int> validofsetsY = new List<int>();
            for (int offY = -4000; offY < 4000; offY++)
                for (int i = 0; i < 24; i++)
                {
                    var rotatedList = toCompare._beaconsList.Select(o => RotatePointsInList(o, i))
                         .Select(o => o.Item2 + offY).ToList();

                    /*winner*/
                    var list = rotatedList.Where(o => scannerSource._beaconsList.Any(g => g.Item2 == o)).ToList();
                    if (list.Count >= 12)
                    {
                        validofsetsY.Add(offY);
                        break;
                    }
                }

            List<int> validofsetsZ = new List<int>();
            for (int offZ = -4000; offZ < 4000; offZ++)
                for (int i = 0; i < 24; i++)
                {
                    var rotatedList = toCompare._beaconsList.Select(o => RotatePointsInList(o, i))
                         .Select(o => o.Item3 + offZ).ToList();

                    /*winner*/
                    var list = rotatedList.Where(o => scannerSource._beaconsList.Any(g => g.Item3 == o)).ToList();
                    if (list.Count >= 12)
                    {
                        validofsetsZ.Add(offZ);
                        break;
                    }
                }


            foreach (var offX in validofsetsX.Distinct())
                foreach (var offY in validofsetsY.Distinct())
                    foreach (var offZ in validofsetsZ.Distinct())
                    {
                        for (int i = 0; i < 24; i++)
                        {

                            var rotatedList = toCompare._beaconsList.Select(o => RotatePointsInList(o, i))
                             .Select(o => new Tuple<int, int, int>(o.Item1+offX, o.Item2+offY, o.Item3+offZ)).ToList();

                            /*winner*/
                            var list = rotatedList.Where(o => scannerSource._beaconsList.Contains(o));
                            if (list.Count() >= 12)
                            {
                                _offsets.Add(toCompare, new Tuple<int, int, int>(offX, offY, offZ));
                                /*we set the rotated list to scanner and real coordinates*/
                                toCompare._beaconsList = rotatedList;
                                toCompare._comparedWith.Add(scannerSource);

                                _beacons.AddRange(list);
                                return toCompare;
                            }
                        }
                    }

            return null;
        }

        public static Tuple<int, int, int>  RotatePointsInList(Tuple<int, int, int> point, int typeofrotaion)
        {

            if (typeofrotaion == 0)
                return new Tuple<int, int, int>(point.Item1, point.Item2, point.Item3);
            if (typeofrotaion == 1)
                return new Tuple<int, int, int>(point.Item1, point.Item3, -point.Item2);
            if (typeofrotaion == 2)
                return new Tuple<int, int, int>(point.Item1, -point.Item2, -point.Item3);
            if (typeofrotaion == 3)
                return new Tuple<int, int, int>(point.Item1, -point.Item3, point.Item2);
            if (typeofrotaion == 4)
                return new Tuple<int, int, int>(-point.Item1, point.Item3, point.Item2);
            if (typeofrotaion == 5)
                return new Tuple<int, int, int>(-point.Item1, point.Item2, -point.Item3);
            if (typeofrotaion == 6)
                return new Tuple<int, int, int>(-point.Item1, -point.Item3, -point.Item2);
            if (typeofrotaion == 7)
                return new Tuple<int, int, int>(-point.Item1, -point.Item2, point.Item3);
            if (typeofrotaion == 8)
                return new Tuple<int, int, int>(point.Item2, point.Item3, point.Item1);
            if (typeofrotaion == 9)
                return new Tuple<int, int, int>(point.Item2, point.Item1, -point.Item3);
            if (typeofrotaion == 10)
                return new Tuple<int, int, int>(point.Item2, -point.Item3, -point.Item1);
            if (typeofrotaion == 11)
                return new Tuple<int, int, int>(point.Item2, -point.Item1, point.Item3);
            if (typeofrotaion == 12)
                return new Tuple<int, int, int>(-point.Item2, point.Item1, point.Item3);
            if (typeofrotaion == 13)
                return new Tuple<int, int, int>(-point.Item2, point.Item3, -point.Item1);
            if (typeofrotaion == 14)
                return new Tuple<int, int, int>(-point.Item2, -point.Item1, -point.Item3);
            if (typeofrotaion == 15)
                return new Tuple<int, int, int>(-point.Item2, -point.Item3, point.Item1);
            if (typeofrotaion == 16)
                return new Tuple<int, int, int>(point.Item3, point.Item1, point.Item2);
            if (typeofrotaion == 17)
                return new Tuple<int, int, int>(point.Item3, point.Item2, -point.Item1);
            if (typeofrotaion == 18)
                return new Tuple<int, int, int>(point.Item3, -point.Item1, -point.Item2);
            if (typeofrotaion == 19)
                return new Tuple<int, int, int>(point.Item3, -point.Item2, point.Item1);
            if (typeofrotaion == 20)
                return new Tuple<int, int, int>(-point.Item3, point.Item2, point.Item1);
            if (typeofrotaion == 21)
                return new Tuple<int, int, int>(-point.Item3, point.Item1, -point.Item2);
            if (typeofrotaion == 22)
                return new Tuple<int, int, int>(-point.Item3, -point.Item2, -point.Item1);
            if (typeofrotaion == 23)
                return new Tuple<int, int, int>(-point.Item3, -point.Item1, point.Item2);

            return point;

        }

        public static void PrintScanner(Scanner scanner)
        {
            Console.WriteLine("scanner print");
            foreach (var beacon in scanner._beaconsList)
                Console.WriteLine(beacon);

            Console.WriteLine("--------------");
        }

        public static void PrintList(IList<Tuple<int, int, int>> list)
        {
            Console.WriteLine("list print");
            foreach (var beacon in list.OrderByDescending(o=>o.Item1))
                Console.WriteLine(beacon);

            Console.WriteLine("--------------");

        }

    }

    public class Scanner
    {
        public String _name;
        public List<Tuple<int, int, int>> _beaconsList = new List<Tuple<int, int, int>>();
        public List<Scanner> _comparedWith = new List<Scanner>();

    }



}
