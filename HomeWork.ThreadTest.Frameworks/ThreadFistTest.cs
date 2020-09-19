using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork.ThreadS.Framework
{
    public class ThreadFistTest
    {
        public void show()
        {
            Console.WriteLine($"****************Thread Begining{Thread.CurrentThread.ManagedThreadId}+****************{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            Thread thread = new Thread(() => { Console.WriteLine("********************Normal Thread********************"); });//线程开始创建一个空方法
            thread.Start();     //线程开始

            Action<string, int> action = (a, b) => { Console.WriteLine($"********************Action:{a + b}********************");
                Console.WriteLine("");
                Console.WriteLine("");
            };  //action委托

            AsyncCallback callBack = (IAsyncResult ar) => 
            {
                IAsyncResult arNew = ar;
                Console.WriteLine("****************call Back Method****************");
                Console.WriteLine("");
                Console.WriteLine("");
            };
            action.BeginInvoke(" action roy wang", 25, callBack, "action testss");     //记一个线程调用委托




            Func<string, int> func = s => { Console.WriteLine(s); return DateTime.Now.Year; };
            IAsyncResult funcArNew = func.BeginInvoke(" func roy wang", callBack, "func testss");     //记一个线程调用委托
            int iResult = func.EndInvoke(funcArNew); //主线程会等待；到这儿来要拿结果的；要拿结果，必须只要执行完毕；


            funcArNew.AsyncWaitHandle.WaitOne();//可以做到等待异步委托执行完毕；
            funcArNew.AsyncWaitHandle.WaitOne(-1);//一直等待任务完成
            funcArNew.AsyncWaitHandle.WaitOne(2000); //限时等待：最多等待2秒，过时不候；会阻塞主线程；
            Console.WriteLine($"****************Thread ended{Thread.CurrentThread.ManagedThreadId}+****************{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
