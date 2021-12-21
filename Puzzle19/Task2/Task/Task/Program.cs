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
        public static List<Tuple<Scanner, Scanner>> _alreadyCompared = new List<Tuple<Scanner, Scanner>>();

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
            }

            if (scanner != null)
                 _scanners.Add(scanner);

            foreach (var sc in _scanners)
                sc.Rotate();
            
            var scan0 =_scanners.Where(o => o._name == "--- scanner 0 ---").First();
            _offsets.Add(scan0, new Tuple<int, int, int>(0, 0, 0));
            scan0._offset = new Tuple<int, int, int>(0, 0, 0);
           
            var compareList =  _scanners.ToList();

            while (_offsets.Count != _scanners.Count())
            {
                var copy = _offsets.Select(o => o.Key).ToList();

                foreach (var scannerSource in copy)
                {
                    foreach (var toCompare in compareList)
                    {
                        if (toCompare == scannerSource)
                            continue;

                        if (_offsets.ContainsKey(toCompare))
                            continue;

                        if (_alreadyCompared.Contains(new Tuple<Scanner, Scanner>(scannerSource, toCompare)))
                            continue;

                        if (_alreadyCompared.Contains(new Tuple<Scanner, Scanner>(toCompare, scannerSource)))
                            continue;

                        var isMetched = IsMatch(scannerSource, toCompare);
                        if (isMetched != null)
                        {
                            break;
                        }
                    }
                }
            }

            foreach (var scan in _scanners)
                foreach (var scan1 in _scanners)
                    if (scan != scan1)
                    {
                        scan._menhetanDistances.Add(scan1, Distance(scan, scan1));
                    }


            foreach (var sc in _scanners)
            {
                Console.WriteLine("Result is {0}  maxdis {1}",sc._name, sc._menhetanDistances.Select(o=>o.Value).Max()); 
            }
            Console.ReadKey();
            

        }

        private static int Distance(Scanner scannerSource, Scanner toCompare)
        {
            var x = Math.Abs(scannerSource._offset.Item1 - toCompare._offset.Item1);
            var y = Math.Abs(scannerSource._offset.Item2 - toCompare._offset.Item2);
            var z = Math.Abs(scannerSource._offset.Item3 - toCompare._offset.Item3);


            return x + y + z;
        }

        private static Scanner IsMatch(Scanner scannerSource, Scanner toCompare)
        {
            _alreadyCompared.Add(new Tuple<Scanner, Scanner>(scannerSource, toCompare));

            List<int> validofsetsX = new List<int>();
            IList<int> rotationx = new List<int>();
            for (int i=0;i<24;i++)
            {
                var list = toCompare._rotatedList.Where(o => o.Item4 == i).Select(o => o.Item1)
                    .SelectMany(o => scannerSource._beaconsList, (o, g) => g.Item1 - o).GroupBy(o => o)
                    .Where(o => o.Count() >= 12).Select(o=>o.Key).ToList();

                if (list.Count() > 0)
                {
                    rotationx.Add(i);
                    validofsetsX = validofsetsX.Union(list).ToList();
                }
            }

            List<int> validofsetsY = new List<int>();
            IList<int> rotationy = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                var list = toCompare._rotatedList.Where(o => o.Item4 == i).Select(o => o.Item2)
                    .SelectMany(o => scannerSource._beaconsList, (o, g) => g.Item2 - o).GroupBy(o => o)
                    .Where(o => o.Count() >= 12).Select(o => o.Key).ToList();

                if (list.Count() > 0)
                {
                    rotationy.Add(i);
                    validofsetsY = validofsetsY.Union(list).ToList();
                }
            }

            List<int> validofsetsZ = new List<int>();
            IList<int> rotationz = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                var list = toCompare._rotatedList.Where(o => o.Item4 == i).Select(o => o.Item3)
                    .SelectMany(o => scannerSource._beaconsList, (o, g) => g.Item3 - o).GroupBy(o => o)
                    .Where(o => o.Count() >= 12).Select(o => o.Key).ToList();

                if (list.Count() > 0)
                {
                    rotationz.Add(i);
                    validofsetsZ = validofsetsZ.Union(list).ToList();
                }
            }

            var rotation = rotationx.Intersect(rotationy).Intersect(rotationz);


            if (rotation != null)
            {
                if (rotation.Count() == 1)
                {

                    Console.WriteLine("Match found {0}-{1}", scannerSource._name, toCompare._name);
                    var offX = validofsetsX.First(); var offY = validofsetsY.First(); var offZ = validofsetsZ.First();
                    var rotatedList = toCompare._rotatedList.Where(o => o.Item4 == rotation.First())
                    .Select(o => new Tuple<int, int, int>(o.Item1 + offX, o.Item2 + offY, o.Item3 + offZ));
                    /*winner*/
                    _offsets.Add(toCompare, new Tuple<int, int, int>(offX, offY, offZ));
                    /*we set the rotated list to scanner and real coordinates*/
                    toCompare._beaconsList = rotatedList.ToList();
                    toCompare._comparedWith.Add(scannerSource);
                    toCompare._offset = new Tuple<int, int, int>(offX, offY, offZ);
                    return toCompare;
                }
            }


            return null;
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
        public Tuple<int, int, int> _offset;
        public List<Tuple<int, int, int, int>>_rotatedList = new ();
        public Dictionary<Scanner, int> _menhetanDistances = new Dictionary<Scanner, int>();

        public void Rotate()
        {
            for (int i = 0; i < 24; i++)
                _rotatedList.AddRange(_beaconsList.Select(o =>
                {
                    var tup = RotatePointsInList(o, i);
                    return new Tuple<int, int, int, int>(tup.Item1, tup.Item2, tup.Item3, i);
                }).ToList());
        
        }

        public static Tuple<int, int, int> RotatePointsInList(Tuple<int, int, int> point, int typeofrotaion)
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



    }



}
