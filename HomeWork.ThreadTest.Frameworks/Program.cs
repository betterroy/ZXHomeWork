using HomeWork.ThreadS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.ThreadTest.Frameworks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************************Thread************************");
            ThreadFistTest threadTest = new ThreadFistTest();
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
