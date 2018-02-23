using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    class Commands
    {
        public static void echo(String input)
        {
            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
        public static void echoVar(String name, int input)
        {
            Console.Write("Variable " + name + ": ");
            Console.WriteLine(input);
        }

        public static void add(int a, int b, String c)
        {
            Console.WriteLine(c + " = " + a + "+" + b);
            Console.WriteLine(c + " = " + (a + b));
        }
        public static void sub(int a, int b, String c)
        {
            Console.WriteLine(c + " = " + (a - b));
        }
        public static void mul(int a, int b, String c)
        {
            Console.WriteLine(c + " = " + (a * b));
        }
        public static void div(int a, int b, String c)
        {
            Console.WriteLine(c + " = " + (a / b));
        }
    }
}
