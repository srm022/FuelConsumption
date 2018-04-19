using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FuelConsumption
{
    public class FileHandler
    {
        string[] readLines;
        string path;

        public FileHandler(string path)
        {
            this.path = path;
        }

        public void Reader()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                readLines = new string[3];

                int i = 0;
                while(!sr.EndOfStream)
                {
                    readLines[i] = sr.ReadLine();
                    Console.WriteLine(readLines[i]);
                    i++;
                }
            }
        }

        public void Writer(decimal[] prices)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("ET {0}", prices[0]);
                sw.WriteLine("ON {0}", prices[1]);
                sw.WriteLine("LPG {0}", prices[2]);
            }
        }

        public string GetReadLine(int index)
        {
            return readLines[index];
        }

        public decimal GetNPrice(int n)
        {
            return Decimal.Parse(System.Text.RegularExpressions.Regex.Match(readLines[n], @"\d+\,\d+").Value);
        }

        public string GetNLabel(int n)
        {
            return System.Text.RegularExpressions.Regex.Match(readLines[n], "^[A-Z]+").Value;
        }

        //public decimal MainInitPrice(string path)
        //{
        //    return Decimal.Parse(readLines[0]);
        //}

        //public string MainInitLabel(string path)
        //{
        //    return System.Text.RegularExpressions.Regex.Match(readLines[0], "^[A-Z]+").Value;
        //}
    }
}
