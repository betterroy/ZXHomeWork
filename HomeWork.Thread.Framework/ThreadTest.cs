using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork.ThreadS.Framework
{
    public class ThreadTest
    {
        public void show()
        {
            Console.WriteLine($"****************Thread Begining{Thread.CurrentThread.ManagedThreadId}+****************{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            Thread thread = new Thread(() => { Console.WriteLine("Normal Thread"); });//线程开始创建一个空方法
            thread.Start();     //线程开始

            Action<string, int> action = (a, b) => { Console.WriteLine($"{a + b}"); };  //action委托
            action.BeginInvoke("roy wang", 25, null, null);     //记一个线程调用委托
            Console.WriteLine($"****************Thread ended{Thread.CurrentThread.ManagedThreadId}+****************{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
