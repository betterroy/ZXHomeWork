using HomeWork.ThreadS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Thread.Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************************Thread************************");
            ThreadTest threadTest = new ThreadTest();
            threadTest.show();
            Console.WriteLine("************************Thread************************");
            printNullLine();
        }
        static void printNullLine()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
