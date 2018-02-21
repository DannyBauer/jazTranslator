using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jazTranslator
{
    class Program
    {
        static List<string> outputLines = new List<string>();
        static void Main(string[] args)
        {
            string line;
            string instruction;
            StreamReader file = new StreamReader(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\jaz specs and examples (1)\demo.jaz");
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                List<string> instructionArrary = line.Split().ToList();
                instruction = instructionArrary[0];
                switch (instruction)
                {
                    case "show":
                        show(instructionArrary);
                        break;
                    default:
                        break;

                }

            }
            if (File.Exists(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\output.cpp"))
            {
                File.Delete(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\output.cpp");
            }

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\output.cpp"))
            {
                for (int i = 0; i < outputLines.Count; i++)
                {
                    sw.WriteLine(outputLines[i]);
                }
                sw.Close();
            }

            Console.ReadLine();
        }
        static void show(List<string> instruction)
        {
            string newInstruction = "";
            int wordCount = instruction.Count;
            newInstruction = newInstruction + "cout << \"";
            for (int i = 1; i < wordCount; i++)
            {
                newInstruction = newInstruction + " " + instruction[i];
            }
            newInstruction = newInstruction + "\" << endl;";
            outputLines.Add(newInstruction);
        }
    }
}
