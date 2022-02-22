using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] to = new string[] { "1", "2", "3" };

            string[] from = new string[] { "1", "2", "3" };

            Random random = new Random();

            

            int j = 615;
            string result = "";

            foreach (string t in to)

            {
                DateTime now = DateTime.Now;
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
