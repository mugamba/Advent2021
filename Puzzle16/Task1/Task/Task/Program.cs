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
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").First();

            string binarystring = String.Join(String.Empty,lines
             .Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
);

            ParsePacket(binarystring);

            
            Console.WriteLine("Result is {0}", binarystring);
            Console.ReadKey();
        }


        private static String ParsePacket(String packet)
        {
            if (packet == "")
                return "";

            if (packet.Length < 8 && packet.ToCharArray().All(o => o == '0'))
                return "";

            var version = packet.Substring(0, 3);
            var versionDec =Convert.ToInt32(version, 2);

            _versionSum += versionDec;

            var type = packet.Substring(3, 3);
            var typeDec = Convert.ToInt32(type, 2);

            if (typeDec == 4)
            {
                var input = packet.Substring(6, packet.Length - 6);

                input = ParseLitreal(input);
                return ParsePacket(input);
            }
            else
            {
                var ltype = packet.Substring(6, 1);
                Int32 ldecimal = 0;
                int numofsegments = 0;
                String subpackets = null;

                if (ltype == "0")
                {
                    var l = packet.Substring(7, 15);
                    ldecimal = Convert.ToInt32(l, 2);
                    subpackets = packet.Substring(22, packet.Length - 22);
                    string rest = "";
                    while (ldecimal > 0)
                    {
                        var bbefore = subpackets.Length;
                        rest = ParsePacket(subpackets);
                        ldecimal = ldecimal - (bbefore - rest.Length);
                    }
                    return ParsePacket(rest);
                }
                else
                {
                    var l = packet.Substring(7, 11);
                    numofsegments = Convert.ToInt32(l, 2);
                    subpackets = packet.Substring(18, packet.Length - 18);
                    string rest = subpackets;
                    for (int i = 0; i < numofsegments; i++)
                    {
                        rest = ParsePacket(rest);
                    
                    }

                }

            }

            return "";

         
        }

        private static string ParseLitreal(string input)
        {
            var literals = new StringBuilder();

            while (true)
            {

                var literal = input.Substring(0, 5);
                literals.Append(literal.Substring(1, 4));
                input = input.Substring(5, input.Length-5);

                if (literal.StartsWith("0"))
                    break;
            }

            _literals.Add(Convert.ToInt64(literals.ToString(), 2));

            return input;
        }
    }
}
