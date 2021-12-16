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

        public static int versionSum = 0;
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            string binarystring = String.Join(String.Empty,
              "8A004A801A8002F478".Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
);

            ParsePacket(binarystring);

            Console.WriteLine("Result is {0}", binarystring);
            Console.ReadKey();
        }


        private static void ParsePacket(String packet)
        {
            var version = packet.Substring(0, 3);
            var versionDec =Convert.ToInt32(version, 2);

            versionSum += versionDec;

            var type = packet.Substring(3, 3);
            var typeDec = Convert.ToInt32(type, 2);

            if (typeDec == 4)
            {

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

                }
                else {
                    var l = packet.Substring(7, 11);
                    numofsegments = Convert.ToInt32(l, 2);
                    subpackets = packet.Substring(18, packet.Length - 18);
                }

                if (numofsegments == 0)


                

            }


         
        }

    }
}
