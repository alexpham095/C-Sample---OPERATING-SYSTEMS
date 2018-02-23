using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace P2
{
    public class Kernel : Sys.Kernel
    {
        const int DIR_SIZE = 100;
        String[] filenames;
        String[] filedata;
        String[] fileext;
        int[] filesize;
        String[] variable;
        int[] variableValue;
        String[] concatVariable;
        String[] concatValue;
        bool stringConcat;

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully.");
            filenames = new String[DIR_SIZE];
            filedata = new String[DIR_SIZE];
            fileext = new String[DIR_SIZE];
            filesize = new int[DIR_SIZE];
            variable = new String[DIR_SIZE];
            variableValue = new int[DIR_SIZE];
            concatVariable = new String[DIR_SIZE];
            concatValue = new String[DIR_SIZE];

        }

        protected override void Run()
        {
            int numFiles = 0;
            Console.Write("$ ");
            var input = Console.ReadLine();
            Char[] inputC = input.ToCharArray();
            Boolean space = false;
            for (int i = 0; i < inputC.Length; i++)
            {
                if (inputC[i] == ' ')
                {
                    space = true;
                    break;
                }
            }
            if (space)
            {
                String command = "";
                Int32 index = 0;
                while (inputC[index] != ' ')
                {
                    command += inputC[index];
                    index++;
                }
                while (inputC[index] == ' ' || inputC[index] == '=')
                {
                    index++;
                }
                Char[] sub = new Char[inputC.Length - index];
                for (int i = 0; i < sub.Length; i++)
                {
                    sub[i] = inputC[index];
                    index++;
                }

                string[] com = input.Split(new char[] { ' ' });
                if (command == "echo")
                {
                    string test = com[1];
                    Boolean printed = false;
                    for (int i = 0; i < DIR_SIZE; i++)
                    {
                        if (variable[i] == test)
                        {
                            printed = true;
                            Commands.echoVar(variable[i], variableValue[i]);
                            break;
                        }

                    }
                    if(printed == false)
                        Commands.echo(test);
                }
                else if (command == "create") //create
                {
                    String name = new string(sub);

                    int runningsize = 0;
                    String inputdata = "";
                    String fullFileName = name;
                    
                    Console.Write("Enter command or enter save to save file\n> ");
                    input = Console.ReadLine();
                    while (input.ToLower() != "save") //gathering each lines input data
                    {
                        runningsize += input.Length;
                        inputdata += input + "~";
                        Console.Write("> ");
                        input = Console.ReadLine();
                    }

                    int a = -1;

                    for (int i = 0; i < DIR_SIZE; i++) //finds first avaible slot for a new file
                        if (filenames[i] == null)
                        {
                            a = i;
                            break;
                        }
                    string fName = "";
                    string eName = "";
                    int b = 0;
                    Char[] full = fullFileName.ToCharArray();
                    while (full[b] != '.')
                    {
                       fName += full[b];
                        b++;
                    }
                    b++;
                    while((full[b] >= 'A' && full[b] <= 'Z') || (full[b] >= 'a' && full[b] <= 'z'))
                    {

                        eName += full[b];
                        b++;

                       
                    }

                    filenames[a] = fName;
                    fileext[a] = eName;
                    filesize[a] = 20 + (runningsize / 2) * 4; //"In the current implementation at least, strings take up 20+(n/2)*4 bytes"
                    filedata[a] = inputdata;
                    numFiles++;

                    Console.WriteLine("*** File Saved ***");



                }
                else if (command == "set")
                {
                    set(com[1], com[2]);

                }
                else if (command == "add")
                {
                    Char[] var1 = com[1].ToCharArray();
                    Char[] var2 = com[2].ToCharArray();
                    int a = 0;
                    int b = 0;
                    if ((var1[0] >= 'A' && var1[0] <= 'Z') || (var1[0] >= 'a' && var1[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[1])
                            {
                                a = variableValue[i];
                            }
                        }
                    }
                    else
                    {
                        a = Utilities.ReadNum(0, 100000000, com[1]);
                       
                    }
                    if ((var2[0] >= 'A' && var2[0] <= 'Z') || (var2[0] >= 'a' && var2[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[2])
                            {
                                b = variableValue[i];
                            }
                        }
                    }
                    else
                    {
                        
                        b = Utilities.ReadNum(0, 100000000, com[2]);
                    }
                    Commands.add(a, b, com[3]);
                    mathSet(com[3], (a + b));
                }
                else if (command == "sub")
                {
                    Char[] var1 = com[1].ToCharArray();
                    Char[] var2 = com[2].ToCharArray();
                    int a = 0;
                    int b = 0;
                    if ((var1[0] >= 'A' && var1[0] <= 'Z') || (var1[0] >= 'a' && var1[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[1])
                            {
                                a = variableValue[i];
                            }
                        }
                    }
                    else
                    {
                        a = Utilities.ReadNum(0, 100000000, com[1]);

                    }
                    if ((var2[0] >= 'A' && var2[0] <= 'Z') || (var2[0] >= 'a' && var2[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[2])
                            {
                                b = variableValue[i];
                            }
                        }
                    }
                    else
                    {

                        b = Utilities.ReadNum(0, 100000000, com[2]);
                    }
                    Commands.sub(a, b, com[3]);
                    mathSet(com[3], (a-b));
                }
                else if (command == "mul")
                {
                    Char[] var1 = com[1].ToCharArray();
                    Char[] var2 = com[2].ToCharArray();
                    int a = 0;
                    int b = 0;
                    if ((var1[0] >= 'A' && var1[0] <= 'Z') || (var1[0] >= 'a' && var1[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[1])
                            {
                                a = variableValue[i];
                            }
                        }
                    }
                    else
                    {
                        a = Utilities.ReadNum(0, 100000000, com[1]);

                    }
                    if ((var2[0] >= 'A' && var2[0] <= 'Z') || (var2[0] >= 'a' && var2[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[2])
                            {
                                b = variableValue[i];
                            }
                        }
                    }
                    else
                    {

                        b = Utilities.ReadNum(0, 100000000, com[2]);
                    }
                    Commands.mul(a, b, com[3]);
                    mathSet(com[3], (a*b));
                }
                else if (command == "div")
                {
                    Char[] var1 = com[1].ToCharArray();
                    Char[] var2 = com[2].ToCharArray();
                    int a = 0;
                    int b = 0;
                    if ((var1[0] >= 'A' && var1[0] <= 'Z') || (var1[0] >= 'a' && var1[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[1])
                            {
                                a = variableValue[i];
                            }
                        }
                    }
                    else
                    {
                        a = Utilities.ReadNum(0, 100000000, com[1]);

                    }
                    if ((var2[0] >= 'A' && var2[0] <= 'Z') || (var2[0] >= 'a' && var2[0] <= 'z'))
                    {
                        for (int i = 0; i < DIR_SIZE; i++)
                        {
                            if (variable[i] == com[2])
                            {
                                b = variableValue[i];
                            }
                        }
                    }
                    else
                    {

                        b = Utilities.ReadNum(0, 100000000, com[2]);
                    }
                    Commands.div(a, b, com[3]);
                    mathSet(com[3], (a/b));
                }
            }
            else if (input == "save")
            {
                Console.WriteLine("ERROR: No open file to save");
            }
            else if (input == "dir")
            {
                Console.WriteLine("Filename           \tExtension      \tSize");
                Console.WriteLine("--------------------------------------------------------");
                for (int i = 0; i < DIR_SIZE; i++)
                {
                    if (filenames[i] == null)
                        continue;

                    Console.WriteLine(filenames[i] + "               \t" + fileext[i] + "            \t" + filesize[i] + " b");
                }

            }

        }

        public void set(string a, string b)
        {
            string varName = a;
            int varVal = Utilities.ReadNum(0, 100000, b);
            for (int i = 0; i < DIR_SIZE; i++)
            {
                if (variable[i] == varName)
                {
                    variable[i] = varName;
                    variableValue[i] = varVal;
                    break;
                }
                else if (variable[i] == null)
                {
                    variable[i] = varName;
                    variableValue[i] = varVal;
                    break;
                }
            }
        }
        public void mathSet(string a, int b)
        {
            string varName = a;
            int varVal = b;
            for (int i = 0; i < DIR_SIZE; i++)
            {
                if (variable[i] == varName)
                {
                    variable[i] = varName;
                    variableValue[i] = varVal;
                    break;
                }
                else if (variable[i] == null)
                {
                    variable[i] = varName;
                    variableValue[i] = varVal;
                    break;
                }
            }
        }
    }
}