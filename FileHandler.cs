using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FuelConsumption
{
    class FileHandler
    {
        public string[] Reader(string path)
        {
            string[] readLines = new string[3];
            using (StreamReader sr = new StreamReader(path))
            {
                int i = 0;
                while(!sr.EndOfStream)
                {
                    readLines[i] = sr.ReadLine();
                    i++;
                }
            }

            return readLines;
        }

        public void Writer(string path, decimal[] prices)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("ET {0}", prices[0]);
                sw.WriteLine("ON {0}", prices[1]);
                sw.WriteLine("LPG {0}", prices[2]);
            }
        }
    }
}
