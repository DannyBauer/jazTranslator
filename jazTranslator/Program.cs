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
            for (int i = 0; i < inputLines.Count(); i++) //Loop through each line of the inputLines
            {
                //Break up the line into instructions
                List<string> instructionList = inputLines[i].Split().ToList();
                iterator = 0;
                //Look for the instruction in the line
                while (instructionList[iterator] == "" && instructionList.Count > 1)
                    iterator++;                
                instruction = instructionList[iterator]; //Instruction found

                //Based on the instruction found, go to its corresponding function
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
                        if (inputLines[i + 1].Split().ToList()[0] == "" && inputLines[i].Split().ToList()[0] != "")
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
                    case "*":
                        multiply(instructionList);
                        break;
                    case "/":
                        divide(instructionList);
                        break;
                    case "div":
                        remainder(instructionList);
                        break;
                    case "&":
                        and(instructionList);
                        break;
                    case "|":
                        or(instructionList);
                        break;
                    case "!":
                        negate(instructionList);
                        break;
                    case "<>":
                        compare(instructionList);
                        break;
                    case "<=":
                        lessThanOrEqual(instructionList);
                        break;
                    case ">=":
                        greaterThanOrEqual(instructionList);
                        break;
                    case "<":
                        lessThan(instructionList);
                        break;
                    case ">":
                        greaterThan(instructionList);
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

        //Create a cout statement
        static void show(List<string> instruction)
        {
            string newInstruction = "";
            int wordCount = instruction.Count;
            string newInstructionBeg = newInstruction + "cout << \""; //Start of the line
            //Add the rest of the line to the cout statement
            for (int i = iterator + 1; i < wordCount; i++)
            {
                newInstruction = newInstruction + " " + instruction[i];
            }

            //Deal with the escape characters
            if (newInstruction.Contains("\""))
            {
                newInstruction = newInstruction.Replace("\"", "\\" + "\"");
            }

            newInstruction = newInstruction + "\" << endl;"; //end the cout statement
            newInstruction = newInstructionBeg + newInstruction;

            if (outputListTracker == 0) 
                outputLines.Add(newInstruction);
            else
                functionLines.Add(newInstruction);
        }
        static void print()
        {
            if (outputListTracker == 0)
                outputLines.Add("cout << int_stack.back() << endl;");
            else
                functionLines.Add("cout << int_stack.back() << endl;");
        }

        //Pushes the value onto the int stack
        static void push(List<string> instruction)
        {
            string value = instruction[iterator + 1]; //Get the value from the line
            
            //Add to output
            if (outputListTracker == 0)
                outputLines.Add("int_stack.push_back(" + value + ");");
            else
                functionLines.Add("int_stack.push_back(" + value + ");");
        }

        //Pushes a variable onto the int stack
        static void rvalue(List<string> instruction)
        {
            string variable = instruction[iterator + 1]; //Get the variable from the line

            //Add to output
            if (outputListTracker == 0)
                outputLines.Add("int_stack.push_back(" + variable + ");");
            else
                functionLines.Add("int_stack.push_back(" + variable + ");");
        }

        //Pushes a variable onto the pointer stack
        static void lvalue(List<string> instruction)
        {           
            string variable = instruction[iterator + 1]; //Get the variable from the line
           
            //Add to output
            if (outputListTracker == 0)
                outputLines.Add("pointer_stack.push_back(&" + variable + ");");
            else
                functionLines.Add("pointer_stack.push_back(&" + variable + ");");
        }

        //Sets a value equal to another
        static void equals()
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("*(int *)pointer_stack.back() = int_stack.back();");
                outputLines.Add("pointer_stack.pop_back();");
                outputLines.Add("int_stack.pop_back();");
            }
            else
            {
                functionLines.Add("*(int *)pointer_stack.back() = int_stack.back();");
                functionLines.Add("pointer_stack.pop_back();");
                functionLines.Add("int_stack.pop_back();");
            }
        }

        //Pops the int stack
        static void pop()
        {           
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.pop_back();"); //Adds the pop command to the output
            }
            else
            {
                functionLines.Add("int_stack.pop_back();");
            }
        }

        //Copies the top value on the stack and adds it to the stack
        static void copy()
        {       
            if (outputListTracker == 0)
            {
                outputLines.Add("int copy = int_stack.back();");
                outputLines.Add("int_stack.push_back(copy);");
            }
            else
            {
                functionLines.Add("int copy = int_stack.back();");
                functionLines.Add("int_stack.push_back(copy);");
            }
        }

        //Adds a label
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
            functionLines.Add("int " + instruction[iterator + 1] + "() {");
            outputListTracker = 1;
        }

        //goto a specified label
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
                outputLines.Add("if (int_stack.back() == 0) {");
                outputLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                outputLines.Add("int_stack.pop_back();");

            }
            else
            {
                functionLines.Add("if (int_stack.back() == 0) {");
                functionLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                functionLines.Add("int_stack.pop_back();");

            }
        }
        static void gotrue(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("if (int_stack.back() != 0) {");
                outputLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                outputLines.Add("int_stack.pop_back();");
            }
            else
            {
                functionLines.Add("if (int_stack.back() != 0) {");
                functionLines.Add("goto label_" + instruction[iterator + 1] + "; }");
                functionLines.Add("int_stack.pop_back();");
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
            functionLines.Add("return 0;");
            functionLines.Add("}");
            outputListTracker = 0;
        }
        static void plus(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] + int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] + int_stack[int_stack.size() - 1]);");
            }
        }

        static void minus(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] - int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] - int_stack[int_stack.size() - 1]);");
            }
        }

        static void multiply(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] * int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] * int_stack[int_stack.size() - 1]);");
            }
        }

        static void divide(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] / int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] / int_stack[int_stack.size() - 1]);");
            }
        }

        static void remainder(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] % int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] % int_stack[int_stack.size() - 1]);");
            }
        }

        static void and(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] && int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] && int_stack[int_stack.size() - 1]);");
            }
        }

        static void negate(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(!int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(!int_stack[int_stack.size() - 1]);");
            }
        }

        static void or(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] || int_stack[int_stack.size() - 1]);");
            }
            else
            {
                functionLines.Add("int_stack.push_back(int_stack[int_stack.size() - 2] || int_stack[int_stack.size() - 1]);");
            }
        }

        static void compare(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("if(int_stack[int_stack.size() - 2] == int_stack[int_stack.size() - 1])");
                outputLines.Add("int_stack.push_back(0);");
                outputLines.Add("else");
                outputLines.Add("int_stack.push_back(1);");
            }
            else
            {
                functionLines.Add("if(int_stack[int_stack.size() - 2] == int_stack[int_stack.size() - 1])");
                functionLines.Add("int_stack.push_back(0);");
                functionLines.Add("else");
                functionLines.Add("int_stack.push_back(1);");
            }
        }

        static void lessThanOrEqual(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("if(int_stack[int_stack.size() - 2] <= int_stack[int_stack.size() - 1])");
                outputLines.Add("int_stack.push_back(1);");
                outputLines.Add("else");
                outputLines.Add("int_stack.push_back(0);");
            }
            else
            {
                functionLines.Add("if(int_stack[int_stack.size() - 2] <= int_stack[int_stack.size() - 1])");
                functionLines.Add("int_stack.push_back(1);");
                functionLines.Add("else");
                functionLines.Add("int_stack.push_back(0);");
            }
        }

        static void greaterThanOrEqual(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("if(int_stack[int_stack.size() - 2] >= int_stack[int_stack.size() - 1])");
                outputLines.Add("int_stack.push_back(1);");
                outputLines.Add("else");
                outputLines.Add("int_stack.push_back(0);");
            }
            else
            {
                functionLines.Add("if(int_stack[int_stack.size() - 2] >= int_stack[int_stack.size() - 1])");
                functionLines.Add("int_stack.push_back(1);");
                functionLines.Add("else");
                functionLines.Add("int_stack.push_back(0);");
            }
        }

        static void lessThan(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("if(int_stack[int_stack.size() - 2] < int_stack[int_stack.size() - 1])");
                outputLines.Add("int_stack.push_back(1);");
                outputLines.Add("else");
                outputLines.Add("int_stack.push_back(0);");
            }
            else
            {
                functionLines.Add("if(int_stack[int_stack.size() - 2] < int_stack[int_stack.size() - 1])");
                functionLines.Add("int_stack.push_back(1);");
                functionLines.Add("else");
                functionLines.Add("int_stack.push_back(0);");
            }
        }

        static void greaterThan(List<string> instruction)
        {
            if (outputListTracker == 0)
            {
                outputLines.Add("if(int_stack[int_stack.size() - 2] > int_stack[int_stack.size() - 1])");
                outputLines.Add("int_stack.push_back(1);");
                outputLines.Add("else");
                outputLines.Add("int_stack.push_back(0);");
            }
            else
            {
                functionLines.Add("if(int_stack[int_stack.size() - 2] > int_stack[int_stack.size() - 1])");
                functionLines.Add("int_stack.push_back(1);");
                functionLines.Add("else");
                functionLines.Add("int_stack.push_back(0);");
            }
        }

        /// <summary>
        /// Sets up the beginning of the cpp file
        /// </summary>
        static void initialzeProgram()
        {
            //Remove if it exists
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
                if (instruction == "label" && inputLines[i + 1].Split().ToList()[0] == "" && inputLines[i].Split().ToList()[0] != "")
                {
                    functions.Add(instructionList[iterator + 1]);
                }
            }

            using (StreamWriter sw = File.CreateText("output.cpp"))
            {
                //Setup the includes
                sw.WriteLine("# include \"stdafx.h\"");
                sw.WriteLine("# include <iostream>");
                sw.WriteLine("# include <string>");
                sw.WriteLine("# include <vector>");
                sw.WriteLine("using namespace std;");

                //Add the variables
                for (int i = 0; i < variables.Count; i++)
                {
                    sw.WriteLine("int " + variables[i] + ";");
                }                
                for (int i = 0; i < functions.Count; i++)
                {
                    sw.WriteLine("int " + functions[i] + "();");
                }
                //Add the vectors
                sw.WriteLine("vector<int> int_stack;");
                sw.WriteLine("vector<int*>pointer_stack;");
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
   
        /// <summary>
        /// Reads in all lines of the jaz file and stores them in a list
        /// </summary>
        static void readJazLines()
        {
            string line;
            StreamReader file = new StreamReader(@"jaz specs and examples (1)\operatorsTest.jaz");
            while ((line = file.ReadLine()) != null) //Loop through all lines
            { 
                inputLines.Add(line); //Add the lines to the list
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