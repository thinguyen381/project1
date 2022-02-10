using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SQLDataSeedGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] to = new string[] { "1", "2", "3" };

            string[] from = new string[] { "1", "2", "3" };

            Random random = new Random();

            DateTime now = DateTime.Now;

            int j = 9;
            string result = "";

            foreach (string t in to)

            {

                foreach (string f in from)

                {

                    if (t == f) continue;

                    for (int i = 9; i <= 109; i++)

                    {

                        now = now.AddDays(1);

                        string s = $"insert into [flight] values ({j++}, '{now.Year}-{now.Month}-{now.Day}', {random.Next(500, 1500)}, {t}, {f})";

                        result += $"\n{s}";

                    }

                }

            }

            string fileName = "C:/temp/result.txt";
            File.WriteAllText(fileName, result);
            Process.Start("notepad.exe", fileName);



        }
    }
}
