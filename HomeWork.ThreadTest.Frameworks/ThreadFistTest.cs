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
        public void NormalThreadShow()
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
        public void ThreadWithReturnShow()
        {
            Console.WriteLine($"****************btnThread_Click Start   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            {
                //{
                //    ParameterizedThreadStart mehtod = s =>
                //    {
                //        Console.WriteLine(s.ToString());
                //        this.DoSomethingLong("btnThread_Click");
                //    };
                //    Thread thread1 = new Thread(mehtod);
                //    thread1.Start("Hugh");
                //}

                //如果想要控制线程顺序呢？回调；其实是第一个线程里的动作执行完毕以后，去执行第二个动作呗；
                //1.其实是第一个线程里的动作执行完毕以后，去执行第二个动作呗；
                //2.既然是子线程来执行：必然是异步的，不能卡界面 ；
                //扩展封装一个； 
                //{
                //    ThreadStart method = () =>
                //    {
                //        Thread.Sleep(3000);
                //        Console.WriteLine("欢迎大家来到.Net高级班的VIP课程。。");
                //        Thread.Sleep(3000);
                //    };

                //    Action actionCallBack = () =>
                //    {
                //        Console.WriteLine("今晚都不错，大家来的挺齐；");
                //    };

                //    actionCallBack += () =>
                //    {
                //        Console.WriteLine("这是多播委托的。。。");
                //    };
                //    this.ThreadWithCallBack(method, actionCallBack);
                //}
                //那如果需要返回值呢？
                {
                    //2020
                    Func<int> func = () =>
                    {
                        Thread.Sleep(3000);
                        return DateTime.Now.Year;
                    };
                    //int iResult = this.ThreadWithReturen<int>(func);
                    //能得到2020这个结果吗？ 
                    Func<int> func1Rusult = this.ThreadWithReturen<int>(func); //不卡界面 
                    {
                        Console.WriteLine("");
                        Console.WriteLine("这里的执行也需要3秒钟。。。");
                    }
                    int iResult = func1Rusult.Invoke(); //这里会卡界面
                    Console.WriteLine($"这里是返回值：{iResult}");

                }
            }
            Console.WriteLine($"****************btnThread_Click End   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
        }
        public void DoSomethingLong(string s)
        {
            Console.WriteLine("这里是输出的："+s);
            Thread.Sleep(3);
        }
        /// <summary>
        /// 控制线程顺序
        /// //两个委托封装回调
        /// </summary>
        /// <param name="threadStart"></param>
        /// <param name="actionCallBack"></param>
        private void ThreadWithCallBack(ThreadStart threadStart, Action actionCallBack)
        {
            //1.While 死循环 判断状态 等待
            //2.Join 
            //Thread thread = new Thread(threadStart);
            //thread.Start();
            ////{
            ////    while (thread.ThreadState != ThreadState.Stopped)
            ////    {
            ////        Thread.Sleep(100);
            ////    }
            ////    actionCallBack.Invoke();
            ////}
            //{
            //    thread.Join(); //卡界面
            //    actionCallBack.Invoke();
            //} 
            ThreadStart method = () =>
            {
                threadStart.Invoke();
                actionCallBack.Invoke();
            };

            Thread thread = new Thread(method);
            thread.Start();
        }
        /// <summary>
        /// 一个新的线程来执行委托里的动作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private Func<T> ThreadWithReturen<T>(Func<T> func)
        {
            #region MyRegion
            ////1.既需要异步执行，
            ////2.又需要有得到计算结果；
            ////根本做不到； 
            //T t = default(T);
            //ThreadStart threadStart = () =>
            //{
            //    t = func.Invoke();
            //};
            //Thread thread = new Thread(threadStart);
            //thread.Start();
            //thread.Join();//如果在这里Join，卡界面了呢？
            //return t; 
            ////1. 回调？
            ////2.Join、

            #endregion
            //花式玩法：
            //返回一个委托 
            T t = default(T);
            ThreadStart threadStart = () =>
            {
                t = func.Invoke();
            };
            Thread thread = new Thread(threadStart);
            thread.Start();//不卡界面

            return new Func<T>(() =>
            {
                thread.Join();
                return t;
            });
        }
        public void ThreadPoolShow()
        {

            Console.WriteLine($"****************btnThreadpool_Click Start   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            //1.如何分配一个线程；
            //{
            //    WaitCallback waitCallback = o =>
            //    {
            //        Console.WriteLine(o);
            //    };
            //    ThreadPool.QueueUserWorkItem(waitCallback);
            //    ThreadPool.QueueUserWorkItem(waitCallback, "欢迎大家来到roy的直播专场。。。");
            //}
            {
                ////2.设置线程池中线程的数量
                //Console.WriteLine("*********************************************"); 
                //ThreadPool.SetMinThreads(2, 2); 
                //ThreadPool.SetMaxThreads(4, 4); //设置的最大值，不能小于计算机的逻辑线程数
                //ThreadPool.GetMinThreads(out int minworkerThreads, out int mincompletionPortThreads);
                //Console.WriteLine($"Min  this  minworkerThreads={minworkerThreads}    this mincompletionPortThreads={mincompletionPortThreads}"); 
                //ThreadPool.GetMaxThreads(out int maxworkerThreads, out int maxcompletionPortThreads);
                //Console.WriteLine($"Min  this  maxworkerThreads={maxworkerThreads}    this maxcompletionPortThreads={maxcompletionPortThreads}");
                ////线程池里的线程我们可以设置，但是不要随便折腾；因为设置以后，线程相当于当前进程而言，是全局的；
                ////Task Parallel都是来自于线程池； 但是new Thread 又可以新开一个线程；会占一个线程池的位置；
            }
            {
                //3.线程等待； 观望式； 
                //ManualResetEvent 默认要求参数状态false-----关闭-----mre.Set();--打开
                //ManualResetEvent 状态参数true----打开---mre.Reset();-关闭
                //ManualResetEvent mre = new ManualResetEvent(false);
                //ThreadPool.QueueUserWorkItem(o =>
                //{
                //    Console.WriteLine(o);
                //    this.DoSomethingLong("欢迎大家来到.Net高级班的VIP课程。。。。");
                //    mre.Set();//状态false----ture
                //    mre.Reset();
                //});
                //Console.WriteLine("do some thing else");
                //Console.WriteLine("do some thing else");
                //Console.WriteLine("do some thing else");
                //Console.WriteLine("do some thing else");
                //mre.WaitOne();//只要是mre状态编程true;就继续往后执行； 
                //Console.WriteLine("任务全部完成了。。。。。");
            }
                {
                    ThreadPool.SetMaxThreads(16, 16);
                    ManualResetEvent mre = new ManualResetEvent(false);
                    for (int i = 0; i < 18; i++)
                    {
                        int k = i;
                        ThreadPool.QueueUserWorkItem(t =>
                        {
                            Console.WriteLine($"ThreadId={Thread.CurrentThread.ManagedThreadId.ToString("00")} {k}");
                            if (k == 17)
                            {
                                mre.Set();
                            }
                            else
                            {
                                mre.WaitOne();
                            }
                        });
                    }
                    if (mre.WaitOne())   //只有在mre.Set();执行以后，状态值为true,才能往后执行；
                    {
                        Console.WriteLine("所有任务执行完成。。。。。。");
                    }
                }


            Console.WriteLine($"****************btnThreadpool_Click End   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
        }
    }
}
