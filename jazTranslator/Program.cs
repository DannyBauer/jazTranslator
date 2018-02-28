using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jazTranslator
{
    class Program
    {
        static List<string> inputLines = new List<string>();
        static List<string> outputLines = new List<string>();
        static List<string> functionLines = new List<string>();
        static List<string> variables = new List<string>();
        static List<string> functions = new List<string>();
        static int variableCount = 0;
        static int iterator;
        static int outputListTracker = 0; //0 for output lines and 1 for function lines
        private static Random random = new Random();
        static void Main(string[] args)
        {
            readJazLines();
            initialzeProgram();       
            string instruction;
            for (int i = 0; i < inputLines.Count(); i++)
            {
                List<string> instructionList = inputLines[i].Split().ToList();
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
                        if (inputLines[i + 1].Split().ToList()[0] == "")
                        {
                            label_function(instructionList);
                        }
                        else
                        {
                            label(instructionList);
                        }
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
                    case "-":
                        minus(instructionList);
                        break;
                    case "print":
                        print();
                        break;
                    case "call":
                        call(instructionList);
                        break;
                    case "return":
                        return_();
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
            if (outputListTracker == 0)
                outputLines.Add(newInstruction);
            else
                functionLines.Add(newInstruction);
        }
        static void print()
        {
            if (outputListTracker == 0)
                outputLines.Add("cout << int_stack.top() << endl;");
            else
                functionLines.Add("cout << int_stack.top() << endl;");
        }
        static void push(List<string> instruction)
        {
            string value = instruction[iterator + 1];
            

            if (outputListTracker == 0)
                outputLines.Add("int_stack.push(" + value + ");");
            else
                functionLines.Add("int_stack.push(" + value + ");");
        }
        static void rvalue(List<string> instruction)
        {
            string variable = instruction[iterator + 1];

            if (outputListTracker == 0)
                outputLines.Add("int_stack.push(" + variable + ");");
            else
                functionLines.Add("int_stack.push(" + variable + ");");

        }
        static void lvalue(List<string> instruction)
        {
            string variable = instruction[iterator + 1];
           
            if (outputListTracker == 0)
                outputLines.Add("pointer_stack.push(&" + variable + ");");
            else
                functionLines.Add("pointer_stack.push(&" + variable + ");");
        }
        static void equals()
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("*(int *)pointer_stack.top() = int_stack.top();");
                outputLines.Add("pointer_stack.pop();");
                outputLines.Add("int_stack.pop();");
            }
            else
            {
                functionLines.Add("*(int *)pointer_stack.top() = int_stack.top();");
                functionLines.Add("pointer_stack.pop();");
                functionLines.Add("int_stack.pop();");
            }
        }
        static void pop()
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.pop();");
            }
            else
            {
                functionLines.Add("int_stack.pop();");
            }
        }
        static void copy()
        {
        

            if (outputListTracker == 0)
            {
                outputLines.Add("int copy = int_stack.top();");
                outputLines.Add("int_stack.push(copy);");
            }
            else
            {
                functionLines.Add("int copy = int_stack.top();");
                functionLines.Add("int_stack.push(copy);");
            }
        }
        static void label(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("label_" + instruction[iterator + 1] + ":");
            }
            else
            {
                functionLines.Add("label_" + instruction[iterator + 1] + ":");
            }
        }
        static void label_function(List<string> instruction)
        {
            functionLines.Add("void " + instruction[iterator + 1] + "() {");
            outputListTracker = 1;
        }
        static void go_to(List<string> instruction)
        {           
            if (outputListTracker == 0)
            {
                outputLines.Add("goto label_" + instruction[iterator + 1] + ";");
            }
            else
            {
                functionLines.Add("goto label_" + instruction[iterator + 1] + ";");
            }

        }
        static void call(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add(instruction[iterator + 1] + " ();");
            }
            else
            {
                functionLines.Add(instruction[iterator + 1] + " ();");
            }
        }
        static void gofalse(List<string> instruction)
        {        
            if (outputListTracker == 0)
            {
                outputLines.Add("if (int_stack.top() == 0) {");
                outputLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                outputLines.Add("int_stack.pop();");

            }
            else
            {
                functionLines.Add("if (int_stack.top() == 0) {");
                functionLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                functionLines.Add("int_stack.pop();");

            }
        }
        static void gotrue(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("if (int_stack.top() != 0) {");
                outputLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                outputLines.Add("int_stack.pop();");
            }
            else
            {
                functionLines.Add("if (int_stack.top() != 0) {");
                functionLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                functionLines.Add("int_stack.pop();");
            }
        }
        static void halt()
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("return 0;");
            }
            else
            {
                functionLines.Add("return 0;");
            }
        }
        static void return_()
        {
            functionLines.Add("}");
            outputListTracker = 0;
        }
        static void plus(List<string> instruction)
        {
            string variable1, variable2;
            if (variableCount < variables.Count)
            {
                variable1 = variables[variableCount];
                variableCount++;
            }
            else
            {
                variable1 = "int " + RandomString(10);
            }

            if (outputListTracker == 0)
            {
                outputLines.Add(variable1 + " = int_stack.top();");
                outputLines.Add("int_stack.pop();");
            }
            else
            {
                functionLines.Add(variable1 + " = int_stack.top();");
                functionLines.Add("int_stack.pop();");
            }

            if (variableCount < variables.Count)
            {
                variable2 = variables[variableCount];
                variableCount++;
            }
            else
            {
                variable2 = "int " + RandomString(10);
            }

            if (outputListTracker == 0)
            {
                outputLines.Add(variable2 + " = int_stack.top();");
                outputLines.Add("int_stack.pop();");
                outputLines.Add("int_stack.push(" + variable1 + " + " + variable2 + ");");
            }
            else
            {
                functionLines.Add(variable2 + " = int_stack.top();");
                functionLines.Add("int_stack.pop();");
                functionLines.Add("int_stack.push(" + variable1 + " + " + variable2 + ");");
            }
        }

        static void minus(List<string> instruction)
        {
            string variable1, variable2;
            if (variableCount < variables.Count)
            {
                variable1 = variables[variableCount];
                variableCount++;
            }
            else
            {
                variable1 = RandomString(10);
            }

            if (outputListTracker == 0)
            {
                outputLines.Add(variable1 + " = int_stack.top();");
                outputLines.Add("int_stack.pop();");
            }
            else
            {
                functionLines.Add(variable1 + " = int_stack.top();");
                functionLines.Add("int_stack.pop();");
            }

            if (variableCount < variables.Count)
            {
                variable2 = variables[variableCount];
                variableCount++;
            }
            else
            {
                variable2 = "int " + RandomString(10);
            }

            if (outputListTracker == 0)
            {
                outputLines.Add(variable2 + " = int_stack.top();");
                outputLines.Add("int_stack.pop();");
                outputLines.Add("int_stack.push(" + variable1 + " - " + variable2 + ");");
            }
            else
            {
                functionLines.Add(variable2 + " = int_stack.top();");
                functionLines.Add("int_stack.pop();");
                functionLines.Add("int_stack.push(" + variable1 + " - " + variable2 + ");");
            }
        }
        static void initialzeProgram()
        {
            if (File.Exists("output.cpp"))
            {
                File.Delete("output.cpp");
            }

            string instruction;
            for (int i = 0; i < inputLines.Count(); i++) //this is a loop to check through the lines before compiling
            {
                List<string> instructionList = inputLines[i].Split().ToList();
                iterator = 0;
                while (instructionList[iterator] == "" && instructionList.Count > 1)
                    iterator++;
                instruction = instructionList[iterator];

                if ((instruction == "lvalue" || instruction == "rvalue") && !variables.Contains(instructionList[iterator + 1]))
                {
                    variables.Add(instructionList[iterator + 1]);
                }
                if (instruction == "label" && inputLines[i + 1].Split().ToList()[0] == "")
                {
                    functions.Add(instructionList[iterator + 1]);
                }
            }

            using (StreamWriter sw = File.CreateText("output.cpp"))
            {
                sw.WriteLine("# include \"stdafx.h\"");
                sw.WriteLine("# include <iostream>");
                sw.WriteLine("# include <string>");
                sw.WriteLine("# include <stack>");
                sw.WriteLine("using namespace std;");

                for (int i = 0; i < variables.Count; i++)
                {
                    sw.WriteLine("int " + variables[i] + ";");
                }
                for (int i = 0; i < functions.Count; i++)
                {
                    sw.WriteLine("void " + functions[i] + "();");
                }
                sw.WriteLine("stack<int> int_stack;");
                sw.WriteLine("stack<int*> pointer_stack;");
                sw.WriteLine("int main() {");
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

                for (int i = 0; i < functionLines.Count; i++)
                {
                    sw.WriteLine(functionLines[i]);
                }
                sw.Close();
            }
        }
        static void readJazLines()
        {
            string line;
            StreamReader file = new StreamReader(@"jaz specs and examples (1)\operatorsTest.jaz");
            while ((line = file.ReadLine()) != null)
            {
                inputLines.Add(line);
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