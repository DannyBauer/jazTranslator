using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jazTranslator
{
    class Program
    {
        static List<string> outputLines = new List<string>();
        static List<string> variables = new List<string>();
        static int variableCount = 0;
        static int iterator;
        private static Random random = new Random();
        static void Main(string[] args)
        {
            initialzeProgram();
            string line;
            string instruction;
            StreamReader file = new StreamReader(@"jaz specs and examples (1)\demo.jaz");
            while ((line = file.ReadLine()) != null)
            {
                List<string> instructionList = line.Split().ToList();
                iterator = 0;
                while (instructionList[iterator] == "" && instructionList.Count > 1)
                    iterator++;
                instruction = instructionList[iterator];
                switch (instruction)
                {
                    case "show":
                        show(instructionList);
                        break;
                    case "push":
                        push(instructionList);
                        break;
                    case "rvalue":
                        rvalue(instructionList);
                        break;
                    case "lvalue":
                        lvalue(instructionList);
                        break;
                    case ":=":
                        equals();
                        break;
                    case "pop":
                        pop();
                        break;
                    case "copy":
                        copy();
                        break;
                    case "label":
                        label(instructionList);
                        break;
                    case "goto":
                        go_to(instructionList);
                        break;
                    case "gofalse":
                        gofalse(instructionList);
                        break;
                    case "gotrue":
                        gotrue(instructionList);
                        break;
                    case "halt":
                        halt();
                        break;
                    case "+":
                        plus(instructionList);
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
            for (int i = iterator + 1; i < wordCount; i++)
            {
                newInstruction = newInstruction + " " + instruction[i];
            }
            newInstruction = newInstruction + "\" << endl;";
            outputLines.Add(newInstruction);
        }
        static void push(List<string> instruction)
        {
            string value = instruction[iterator + 1];
            outputLines.Add("int_stack.push(" + value + ");");
        }
        static void rvalue(List<string> instruction)
        {
            string variable = instruction[iterator + 1];
            outputLines.Add("int_stack.push(" + variable + ");");
        }
        static void lvalue(List<string> instruction)
        {
            string variable = instruction[iterator + 1];
            outputLines.Add("pointer_stack.push(&" + variable + ");");
        }
        static void equals()
        {
            outputLines.Add("*(int *)pointer_stack.top() = int_stack.top();");
            outputLines.Add("pointer_stack.pop();");
            outputLines.Add("int_stack.pop();");
        }
        static void pop()
        {
            outputLines.Add("int_stack.pop();");
        }
        static void copy()
        {
            outputLines.Add("int copy = int_stack.top();");
            outputLines.Add("int_stack.push(copy);");
        }
        static void label(List<string> instruction)
        {
            outputLines.Add("label_" + instruction[iterator + 1] + ":");
        }
        static void go_to(List<string> instruction)
        {
            outputLines.Add("goto label_" + instruction[iterator + 1] +";");
        }
        static void gofalse(List<string> instruction)
        {
            outputLines.Add("if (int_stack.top() == 0) {");
            outputLines.Add("goto label_" + instruction[iterator + 1] + "; }");
            outputLines.Add("int_stack.pop();");
        }
        static void gotrue(List<string> instruction)
        {
            outputLines.Add("if (int_stack.top() != 0) {");
            outputLines.Add("goto label_" + instruction[iterator + 1] + "; }");
            outputLines.Add("int_stack.pop();");
        }
        static void halt()
        {
            outputLines.Add("return 0;");
        }
        static void plus(List<string> instruction)
        {
            string variable1 = variables[variableCount];
            variableCount++;
  
            outputLines.Add( variable1 + " = int_stack.top();");
            outputLines.Add("int_stack.pop();");

            string variable2 = variables[variableCount];
            variableCount++;

            outputLines.Add( variable2 + " = int_stack.top();");
            outputLines.Add("int_stack.pop();");
            outputLines.Add("int_stack.push(" + variable1 + " + " + variable2 + ");");

        }
        static void initialzeProgram()
        {
            if (File.Exists("output.cpp"))
            {
                File.Delete("output.cpp");
            }

            string line;
            string instruction;
            StreamReader file = new StreamReader(@"jaz specs and examples (1)\demo.jaz");
            while ((line = file.ReadLine()) != null)
            {
                List<string> instructionList = line.Split().ToList();
                iterator = 0;
                while (instructionList[iterator] == "" && instructionList.Count > 1)
                    iterator++;
                instruction = instructionList[iterator];

                if (instruction == "lvalue" && !variables.Contains(instructionList[iterator + 1]))
                {
                    variables.Add(instructionList[iterator + 1]);
                }
            }

                using (StreamWriter sw = File.CreateText("output.cpp"))
            {
                sw.WriteLine("# include \"stdafx.h\"");
                sw.WriteLine("# include <iostream>");
                sw.WriteLine("# include <string>");
                sw.WriteLine("# include <stack>");
                sw.WriteLine("using namespace std;");
                sw.WriteLine("int main() {");

                for (int i  = 0; i < variables.Count; i++)
                {
                    sw.WriteLine("int " + variables[i] + ";");
                }
                sw.WriteLine("stack<int> int_stack;");
                sw.WriteLine("stack<int*> pointer_stack;");
            }
        }
        static void finishProgram()
        {
            using (StreamWriter sw = new StreamWriter("output.cpp", true))
            {
                for (int i = 0; i < outputLines.Count; i++)
                {
                    sw.WriteLine(outputLines[i]);
                }
                sw.WriteLine("}");
                sw.Close();
            }
        }
       
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}