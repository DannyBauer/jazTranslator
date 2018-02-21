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
            initialzeProgram();
            string line;
            string instruction;
            StreamReader file = new StreamReader(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\jaz specs and examples (1)\demo.jaz");
            while ((line = file.ReadLine()) != null)
            {
                List<string> instructionList = line.Split().ToList();
                instruction = instructionList[0];
                switch (instruction)
                {
                    case "show":
                        show(instructionList);
                        break;
                    default:
                        break;

                }
            }

            finishProgram();

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
        static void initialzeProgram()
        {
            if (File.Exists(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\output.cpp"))
            {
                File.Delete(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\output.cpp");
            }

            using (StreamWriter sw = File.CreateText(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\output.cpp"))
            {
                sw.WriteLine("# include \"stdafx.h\"");
                sw.WriteLine("# include <iostream>");
                sw.WriteLine("# include <string>");
                sw.WriteLine("using namespace std;");
                sw.WriteLine("int main() {");
            }
        }
        static void finishProgram()
        {
            using (StreamWriter sw = new StreamWriter(@"E:\Project Files\jazTranslator\jazTranslator\jazTranslator\output.cpp", true))
            {
                for (int i = 0; i < outputLines.Count; i++)
                {
                    sw.WriteLine(outputLines[i]);
                }
                sw.WriteLine("}");
                sw.Close();
            }
        }
    }
}
