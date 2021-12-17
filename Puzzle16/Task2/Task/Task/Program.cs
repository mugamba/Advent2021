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

        public static int _versionSum = 0;
        public static IList<long> _literals = new List<long>();
        public static String _packet = "";
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").First();

            _packet = String.Join(String.Empty,lines
             .Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
);

            var res = ParsePacket();
            Console.WriteLine("Result is {0}", res);
            Console.ReadKey();
        }


        private static long ParsePacket()
        {
            if (_packet == "")
                return 1;

            if (_packet.Length < 8 && _packet.ToCharArray().All(o => o == '0'))
                return 1;

            var version = _packet.Substring(0, 3);
            var versionDec =Convert.ToInt32(version, 2);

            _versionSum += versionDec;

            var type = _packet.Substring(3, 3);
            var typeDec = Convert.ToInt32(type, 2);
          
            if (typeDec == 4)
            {
                _packet = _packet.Substring(6, _packet.Length - 6);
                return ParseLitreal();
               
            }
            else
            {
                var ltype = _packet.Substring(6, 1);
                Int32 ldecimal = 0;
                int numofsegments = 0;
                String subpackets = null;

                if (ltype == "0")
                {
                    var l = _packet.Substring(7, 15);
                    ldecimal = Convert.ToInt32(l, 2);
                    _packet = _packet.Substring(22, _packet.Length - 22);
                    var counter = 0; long result = 0;
                    while (ldecimal > 0)
                    {
                        var bbefore = _packet.Length;
                        if (counter == 0)
                            result = ParsePacket();
                        else
                            result = DoFunc(result, ParsePacket(), typeDec);
                        ldecimal = ldecimal - (bbefore - _packet.Length);

                        counter++;
                    }
                    return result;
                }
                else
                {
                    var l = _packet.Substring(7, 11);
                    numofsegments = Convert.ToInt32(l, 2);
                    _packet = _packet.Substring(18, _packet.Length - 18);
                    string rest = subpackets; long result = 0;
                    for (int i = 0; i < numofsegments; i++)
                    {
                        if (i == 0)
                            result = ParsePacket();
                        else
                            result = DoFunc(result, ParsePacket(), typeDec);

                    }

                    return result;
                }

            }

            return 1;

         
        }

        private static long ParseLitreal()
        {
            var literals = new StringBuilder();
            while (true)
            {

                var literal = _packet.Substring(0, 5);
                literals.Append(literal.Substring(1, 4));
                _packet = _packet.Substring(5, _packet.Length-5);

                if (literal.StartsWith("0"))
                    break;
            }

            return Convert.ToInt64(literals.ToString(), 2);
        }

        private static long DoFunc(long input1, long input2, int masterType)
        {
            if (masterType == 0)
                return input1 + input2;
            if (masterType == 1)
                return input1 * input2;
            if (masterType == 2)
                return input1 < input2 ? input1 : input2;
            if (masterType == 3)
                return input1 > input2 ? input1 : input2;
            if (masterType == 5)
                return input1 > input2 ? 1 : 0;
            if (masterType == 6)
                return input1 < input2 ? 1 : 0;
            if (masterType == 7)
                return input1 == input2 ? 1 : 0;
            
            return 1;
        }
    }
}
