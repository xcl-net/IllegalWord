using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrimEmptyString
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader file = new StreamReader("smsBannedWords.txt", Encoding.UTF8, true))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        File.AppendAllText("smsBanned1.txt",line + "\r\n", Encoding.UTF8);
                    }
                }
            }
            Console.WriteLine("ok");
            Console.ReadKey();
			
        }
    }
}



