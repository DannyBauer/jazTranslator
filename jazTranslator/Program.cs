using System;
using System.IO;

namespace jazTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile();
        }

        static void ReadFile()
        {
            string line;
            StreamReader file = new StreamReader(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\jaz specs and examples (1)\demo.jaz");
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            file.Close();
            Console.ReadLine();
        }
    }
}
