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
            ThreadFistTest threadTest = new ThreadFistTest();
            //Console.WriteLine("************************NormalThreadShow************************");
            //threadTest.NormalThreadShow();
            //Console.WriteLine("************************NormalThreadShow************************");
            //printNullLine();

            //Console.WriteLine("************************ThreadWithReturnShow************************");
            //threadTest.ThreadWithReturnShow();
            //Console.WriteLine("************************ThreadWithReturnShow************************");
            //printNullLine();


            Console.WriteLine("************************ThreadPoolShow************************");
            threadTest.ThreadPoolShow();
            Console.WriteLine("************************ThreadPoolShow************************");
            printNullLine();


            Console.ReadLine();
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
