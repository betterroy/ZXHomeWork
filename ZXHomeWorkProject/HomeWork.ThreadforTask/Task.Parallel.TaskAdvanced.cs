using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork.ThreadforTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Private Method
        /// <summary>
        /// 一个比较耗时耗资源的私有方法
        /// </summary>
        /// <param name="name"></param>
        private void DoSomethingLong(string name)
        {
            Console.WriteLine($"****************DoSomethingLong Start  {name}  {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            long lResult = 0;
            //for (int i = 0; i < 1000_000_000; i++)
            //{
            //    lResult += i;
            //}
            Thread.Sleep(2000); //线程是闲置的；
            Console.WriteLine($"****************DoSomethingLong   End  {name}  {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {lResult}***************");
        }

        private void Coding(string v1, string v2)
        {
            Console.WriteLine($"Coding:v1{v1}v2{v2}");
        }

        private void Teach(string v)
        {
            Console.WriteLine("Teach:" + v);
        }
        #endregion
        /// <summary>
        ///1 Task：Waitall WaitAny  Delay
        ///2 TaskFactory:ContinueWhenAny ContinueWhenAll 
        ///3 并行运算Parallel.Invoke/For/Foreach 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTask_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"****************btnTask_Click Start {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            {
                #region 如何启动任务
                //{
                //    Task task = new Task(() =>
                //    {
                //        Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString("00"));
                //    });
                //    task.Start();
                //}
                //{
                //    Task task = Task.Run(() => this.DoSomethingLong("btnTask_Click_2"));
                //}
                //{
                //    TaskFactory taskFactory = Task.Factory;
                //    Task task = taskFactory.StartNew(() => this.DoSomethingLong("btnTask_Click_3"));
                //}
                #endregion

                #region Task分配的任务是来自于线程池
                //{
                //    ThreadPool.SetMaxThreads(18, 18);
                //    //线程池是单例的，全局唯一的
                //    //设置后，同时并发的Task只有8个；而且线程是复用的；
                //    //Task的线程是源于线程池
                //    //全局的，请不要这样设置！！！
                //    List<int> countlist = new List<int>();
                //    List<Task> tasklist = new List<Task>();
                //    for (int i = 0; i < 100; i++)
                //    {
                //        int k = i;
                //        tasklist.Add(Task.Run(() =>
                //        {
                //            Console.WriteLine($"This is {k} running ThreadId={Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                //            Thread.Sleep(2000);
                //            countlist.Add(Thread.CurrentThread.ManagedThreadId);
                //        }));
                //    }
                //    Task.WaitAll(tasklist.ToArray());
                //    Console.WriteLine(countlist.Distinct().Count());
                //    //假如说我想控制下Task的并发数量，该怎么做？
                //}
                #endregion

                //父子线程
                {
                    //Task task = new Task(() =>
                    //{
                    //    Console.WriteLine($"****************task开始了: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //    Task task1 = new Task(() =>
                    //                    {
                    //                        Console.WriteLine($"****************task1: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //                        Thread.Sleep(1000);
                    //                        Console.WriteLine("我是task1线程");
                    //                    });

                    //    Task task2 = new Task(() =>
                    //                    {
                    //                        Console.WriteLine($"****************task2: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //                        Thread.Sleep(1000);
                    //                        Console.WriteLine("我是task2线程");
                    //                    });
                    //    task1.Start();
                    //    task2.Start();
                    //    Console.WriteLine($"****************task结束了: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //});

                    //task.Start();
                    //task.Wait();   //单个线程的等待
                    //Console.WriteLine($"****************UI线程: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                }
                //TaskCreationOptions.None 默认行为
                {
                    //Task task = new Task(() =>
                    //{
                    //    Console.WriteLine($"****************task开始了: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //    Task task1 = new Task(() =>
                    //    {
                    //        Console.WriteLine($"****************task1: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //        Thread.Sleep(1000);
                    //        Console.WriteLine("我是task1线程");
                    //    }, TaskCreationOptions.None);

                    //    Task task2 = new Task(() =>
                    //    {
                    //        Console.WriteLine($"****************task2: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //        Thread.Sleep(1000);
                    //        Console.WriteLine("我是task2线程");
                    //    },TaskCreationOptions.None);
                    //    task1.Start();
                    //    task2.Start();
                    //    Console.WriteLine($"****************task: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //});

                    //task.Start();
                    //task.Wait();   //单个线程的等待
                    //Console.WriteLine($"****************UI线程: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                }
                //TaskCreationOptions.PreferFairness: 以一种尽可能公平的方式安排任务，这意味着较早安排的任务将更可能较早运行，而较晚安排运行的任务将更可能较晚运行。
                {
                    //Task task = new Task(() =>
                    //{
                    //    Console.WriteLine($"****************task: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //}, TaskCreationOptions.PreferFairness);

                    //Task task1 = new Task(() =>
                    //{
                    //    Console.WriteLine($"****************task: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //}, TaskCreationOptions.PreferFairness);

                    //Console.WriteLine($"****************UI线程: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                }
                //TaskCreationOptions.LongRunning: 指定某个任务将是运行时间长、粗粒度的操作。 它会向 System.Threading.Tasks.TaskScheduler 提示，过度订阅可能是合理的。
                {
                    //Task task = new Task(() =>
                    //{ 
                    //    Console.WriteLine($"****************task: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                    //},TaskCreationOptions.LongRunning); 
                    ////Console.WriteLine($"****************UI线程: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                }
                //TaskCreationOptions.AttachedToParent //作用：指定将任务附加到任务层次结构中的某个父级,父任务必须等待所有子任务执行完毕才能执行
                //{
                //    Task task = new Task(() =>
                //    {
                //        Console.WriteLine($"****************task开始了: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                //        Task task1 = new Task(() =>
                //        {
                //            Console.WriteLine($"****************task1: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                //            Thread.Sleep(1000);
                //            Console.WriteLine("我是task1线程");
                //        }, TaskCreationOptions.AttachedToParent);

                //        Task task2 = new Task(() =>
                //        {
                //            Console.WriteLine($"****************task2: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                //            Thread.Sleep(1000);
                //            Console.WriteLine("我是task2线程");
                //        }, TaskCreationOptions.AttachedToParent);
                //        task1.Start();
                //        task2.Start();
                //        Console.WriteLine($"****************task结束了: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                //    });

                //    task.Start();
                //    task.Wait();   //单个线程的等待
                //    Console.WriteLine($"****************UI线程: {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
                //}

            }
            {
                //线程ID和任务ID
                //Task t1 = Task.Run(() =>
                //{
                //    Console.WriteLine($"This is ThreadId={Thread.CurrentThread.ManagedThreadId}");
                //    Console.WriteLine($"this 任务ID={Task.CurrentId}");
                //});

                //t1.Wait();
                //Console.WriteLine($"This is ThreadId={Thread.CurrentThread.ManagedThreadId}");
                //Console.WriteLine($"this 任务ID={t1.Id}");

                ////线程ID和任务ID
                //Task t2 = Task.Run(() =>
                //{
                //    Console.WriteLine($"This is ThreadId={Thread.CurrentThread.ManagedThreadId}");
                //    Console.WriteLine($"this 任务ID={Task.CurrentId}");
                //});

                //t2.Wait();
                //Console.WriteLine($"This is ThreadId={Thread.CurrentThread.ManagedThreadId}");
                //Console.WriteLine($"this 任务ID={t2.Id}");
            }
            #region Sleep 和 Delay 的区别
            {
                //{
                //    Stopwatch stopwatch = new Stopwatch();
                //    stopwatch.Start();
                //    Console.WriteLine("在Sleep之前");
                //    Thread.Sleep(2000);//同步等待--当前线程等待2s 然后继续
                //    Console.WriteLine("在Sleep之后");
                //    stopwatch.Stop();
                //    Console.WriteLine($"Sleep耗时{stopwatch.ElapsedMilliseconds}");
                //}
                //{
                //    Stopwatch stopwatch = new Stopwatch();
                //    stopwatch.Start();
                //    Console.WriteLine("在Delay之前");
                //    Task task = Task.Delay(2000)
                //        .ContinueWith(t =>
                //        {
                //            stopwatch.Stop();
                //            Console.WriteLine("在Delay之后");
                //            Console.WriteLine($"Delay耗时，在延时两秒后{stopwatch.ElapsedMilliseconds}");

                //            Console.WriteLine($"This is ThreadId={Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                //        });//异步等待--等待2s后启动新任务
                //}
            }
            #endregion



            //{
            //    //什么时候能用多线程？ 任务能并发的时候
            //    //多线程能干嘛？提升速度/优化用户体验
            //    Console.WriteLine("Eleven开启了一学期的课程");
            //    this.Teach("Lesson1");
            //    this.Teach("Lesson2");
            //    this.Teach("Lesson3");
            //    //不能并发，因为有严格顺序(只有Eleven讲课)
            //    Console.WriteLine("部署一下项目实战作业，需要多人合作完成");
            //    //开发可以多人合作---多线程--提升性能

            //    TaskFactory taskFactory = new TaskFactory();
            //    List<Task> taskList = new List<Task>();
            //    taskList.Add(taskFactory.StartNew(() => this.Coding("冰封的心", "Portal")));
            //    taskList.Add(taskFactory.StartNew(() => this.Coding("随心随缘", "  DBA ")));
            //    taskList.Add(taskFactory.StartNew(() => this.Coding("心如迷醉", "Client")));
            //    taskList.Add(taskFactory.StartNew(() => this.Coding(" 千年虫", "BackService")));
            //    taskList.Add(taskFactory.StartNew(() => this.Coding("简单生活", "Wechat")));

            //    //谁第一个完成，获取一个红包奖励
            //    taskFactory.ContinueWhenAny(taskList.ToArray(), t => Console.WriteLine($"XXX开发完成，获取个红包奖励{Thread.CurrentThread.ManagedThreadId.ToString("00")}"));
            //    //实战作业完成后，一起庆祝一下
            //    taskList.Add(taskFactory.ContinueWhenAll(taskList.ToArray(), rArray => Console.WriteLine($"开发都完成，一起庆祝一下{Thread.CurrentThread.ManagedThreadId.ToString("00")}")));
            //    //ContinueWhenAny  ContinueWhenAll 非阻塞式的回调；而且使用的线程可能是新线程，也可能是刚完成任务的线程，唯一不可能是主线程


            //    //阻塞当前线程，等着任意一个任务完成
            //    Task.WaitAny(taskList.ToArray());//也可以限时等待
            //    Console.WriteLine("Eleven准备环境开始部署");
            //    //需要能够等待全部线程完成任务再继续  阻塞当前线程，等着全部任务完成
            //    Task.WaitAll(taskList.ToArray());
            //    Console.WriteLine("5个模块全部完成后，Eleven集中点评");

            //    //Task.WaitAny  WaitAll都是阻塞当前线程，等任务完成后执行操作
            //    //阻塞卡界面，是为了并发以及顺序控制
            //    //网站首页：A数据库 B接口 C分布式服务 D搜索引擎，适合多线程并发，都完成后才能返回给用户，需要等待WaitAll
            //    //列表页：核心数据可能来自数据库/接口服务/分布式搜索引擎/缓存，多线程并发请求，哪个先完成就用哪个结果，其他的就不管了
            //}
            {
                //TaskFactory taskFactory = new TaskFactory();
                //List<Task> taskList = new List<Task>();
                //taskList.Add(taskFactory.StartNew(o => this.Coding("冰封的心", "Portal"), "冰封的心"));
                //taskList.Add(taskFactory.StartNew(o => this.Coding("随心随缘", "  DBA "), "随心随缘"));
                //taskList.Add(taskFactory.StartNew(o => this.Coding("心如迷醉", "Client"), "心如迷醉"));
                //taskList.Add(taskFactory.StartNew(o => this.Coding(" 千年虫", "BackService"), " 千年虫"));
                //taskList.Add(taskFactory.StartNew(o => this.Coding("简单生活", "Wechat"), "简单生活"));

                ////谁第一个完成，获取一个红包奖励
                //taskFactory.ContinueWhenAny(taskList.ToArray(), t => Console.WriteLine($"{t.AsyncState}开发完成，获取个红包奖励{Thread.CurrentThread.ManagedThreadId.ToString("00")}"));
            }
            {
                //Task.Run(() => this.DoSomethingLong("btnTask_Click")).ContinueWith(t => Console.WriteLine($"btnTask_Click已完成{Thread.CurrentThread.ManagedThreadId.ToString("00")}"));//回调
            }
            {
                //Task<int> result = Task.Run<int>(() =>
                // {
                //     Thread.Sleep(2000);
                //     return DateTime.Now.Year;
                // });
                ////int i = result.Result;//会阻塞
                //Task.Run(() =>
                //{
                //    int i = result.Result;//会阻塞
                //    Console.WriteLine($"i={i}");
                //});
            }
            {
                //Task.Run<int>(() =>
                //{
                //    Thread.Sleep(2000);
                //    return DateTime.Now.Year;
                //}).ContinueWith(tInt =>
                //{
                //    int i = tInt.Result;
                //    Console.WriteLine($"i={i}");
                //});

            }
            {
                //假如说我想控制下Task的并发数量，该怎么做？  20个
                List<Task> taskList = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    int k = i;
                    if (taskList.Count(t => t.Status != TaskStatus.RanToCompletion) >= 20)
                    {
                        Task.WaitAny(taskList.ToArray());
                        taskList = taskList.Where(t => t.Status != TaskStatus.RanToCompletion).ToList();
                    }
                    taskList.Add(Task.Run(() =>
                    {
                        Console.WriteLine($"This is {k} running ThreadId={Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                        Thread.Sleep(2000);
                    }));
                }
            }
            Console.WriteLine($"****************btnTask_Click End   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
        }


        /// <summary>
        /// Parallel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParallel_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"****************btnParallel_Click Start   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            {
                //Parallel并发执行多个Action 多线程的，
                //主线程会参与计算---阻塞界面
                //等于TaskWaitAll+主线程计算
                //Parallel.Invoke(() => this.DoSomethingLong("btnParallel_Click_1"),
                //    () => this.DoSomethingLong("btnParallel_Click_2"),
                //    () => this.DoSomethingLong("btnParallel_Click_3"),
                //    () => this.DoSomethingLong("btnParallel_Click_4"),
                //    () => this.DoSomethingLong("btnParallel_Click_5"));
            }
            {
                //Parallel.For(0, 5, i => this.DoSomethingLong($"btnParallel_Click_{i}"));
            }
            {
                //Parallel.ForEach(new int[] { 0, 1, 2, 3, 4 }, i => this.DoSomethingLong($"btnParallel_Click_{i}"));
            }
            {
                //ParallelOptions options = new ParallelOptions();
                //options.MaxDegreeOfParallelism = 3;
                //Parallel.For(0, 10, options, i => this.DoSomethingLong($"btnParallel_Click_{i}"));
            }
            {
                //有没有办法不阻塞？
                //Task.Run(() =>
                //{
                //    ParallelOptions options = new ParallelOptions();
                //    options.MaxDegreeOfParallelism = 3;
                //    Parallel.For(0, 10, options, i => this.DoSomethingLong($"btnParallel_Click_{i}"));
                //});
            }
            {
                //Parallel.For<int>(0, 5, () => { return DateTime.Now.Year; });
            }
            Console.WriteLine($"****************btnParallel_Click End   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"****************button2_Click Start   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            {
                ///在子线程内部，发生异常之后，异常找不到了，那么异常到那儿去了？必然是被吞掉了；
                ///1.如何把异常捕捉到呢？
                ///a.list 存入所有的Task
                ///b.Task.WaitAll()可以捕捉到AggregateException类型异常；
                ///c.可以多个Cath来捕捉异常，异常--先具体再全部
                ///d.可以通过aex.InnerExceptions获取到多线程中所有的异常
                ///2.发生异常是个很尴尬的事儿，如果多个线程同时执行业务，如果有一个线程异常了，其实对整个业务链来说是不完美的；需要一些应对策略，可能就需要让其他的线程停止下来，重新操作；
                #region 多线程异常处理
                {
                    ////Thread thread = null;
                    ////thread.Abort();//可以停止线程； 
                    ////你们在处理异常的时候、怎么玩？
                    //try
                    //{
                    //    List<Task> taskList = new List<Task>();
                    //    for (int i = 0; i < 50; i++)
                    //    {
                    //        string name = $"btnTaskAdvanced_Click_{i}";
                    //        taskList.Add(Task.Run(() =>
                    //        {
                    //            if (name.Equals("btnTaskAdvanced_Click_8"))
                    //            {
                    //                throw new Exception("btnTaskAdvanced_Click_8  异常了。。。");
                    //            }
                    //            else if (name.Equals("btnTaskAdvanced_Click_13"))
                    //            {
                    //                throw new Exception("btnTaskAdvanced_Click_13  异常了。。。");
                    //            }
                    //            else if (name.Equals("btnTaskAdvanced_Click_25"))
                    //            {
                    //                throw new Exception("btnTaskAdvanced_Click_25  异常了。。。");
                    //            }
                    //            Console.WriteLine($"this is {name} 成功， ThreadID={Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                    //        }));
                    //    }
                    //    Task.WaitAll(taskList.ToArray());
                    //}
                    //catch (AggregateException aex)
                    //{
                    //    foreach (var aexception in aex.InnerExceptions)
                    //    {
                    //        Console.WriteLine(aexception.Message);
                    //    }
                    //    Console.WriteLine(aex.Message);
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //}
                }
                #endregion

                //2.多线程中如果有一个线程执行失败，需要取消其他的线程；
                //3.50个线程：其中有一个线程启动了，其他别的线程还有启动；刚好就这个启动的线程就异常了；还没有启动的这49个线程，我还有必要让你启动吗？

                #region 线程取消 怎么做？
                {
                    //1.全局变量 
                    {
                        //bool isOk = true;
                        //for (int i = 0; i < 50; i++)
                        //{
                        //    string name = $"btnTaskAdvanced_Click_{i}";
                        //    Task.Run(() =>
                        //    {
                        //        if (isOk)
                        //        {
                        //            Console.WriteLine($"{name}  Start.....");
                        //        }
                        //        else
                        //        {
                        //            throw new AggregateException(); //停止了线程
                        //        }
                        //        if (name.Equals("btnTaskAdvanced_Click_8"))
                        //        {
                        //            isOk = false;
                        //            throw new Exception("btnTaskAdvanced_Click_8  异常了。。。");
                        //        }
                        //        else if (name.Equals("btnTaskAdvanced_Click_13"))
                        //        {
                        //            throw new Exception("btnTaskAdvanced_Click_13  异常了。。。");
                        //        }
                        //        else if (name.Equals("btnTaskAdvanced_Click_25"))
                        //        {
                        //            throw new Exception("btnTaskAdvanced_Click_25  异常了。。。");
                        //        }

                        //        if (isOk)
                        //        {
                        //            Console.WriteLine($"{name}  End.....");
                        //        }
                        //        else
                        //        {
                        //            //这里也可以回滚
                        //            throw new AggregateException(); //停止了线程
                        //        }

                        //        Console.WriteLine($"this is {name} 成功， ThreadID={Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                        //    });
                        //}
                    }
                    //2.CancellationTokenSource,
                    //有一个属性：IsCancellationRequested，默认值为false
                    //还有一个Cancel()方法，只要是Cancel()执行，就可以把IsCancellationRequested指定为true; 可以重复 调用Cancel()方法；
                    //跟全局变量有点像，CancellationTokenSource是线程安全的；Cancel() 调用以后IsCancellationRequested指定为true不能再重置回来的；
                    //以上+业务判断来实现线程取消；如果在Cancel 之前已经进入业务处理的线程是无法停止下来，所以在最后再判断一次，不让你正常结束；
                    {
                        //try
                        //{
                        //    CancellationTokenSource cts = new CancellationTokenSource();
                        //    List<Task> taskList = new List<Task>();
                        //    for (int i = 0; i < 100; i++)
                        //    {
                        //        Thread.Sleep(new Random().Next(100, 300));
                        //        string name = $"btnTaskAdvanced_Click_{i}";
                        //        taskList.Add(Task.Run(() =>
                        //       {
                        //           //只要是执行到这里了，就是一个单独的线程；这里就可以开启事务

                        //           if (!cts.IsCancellationRequested)
                        //           {
                        //               Console.WriteLine($"{name}  Start.....{Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                        //           }
                        //           else
                        //           {
                        //               Console.WriteLine($"{name} 失败了.....{Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                        //               //可以事务回滚
                        //               //throw new AggregateException(); //停止了线程
                        //           }
                        //           if (name.Equals("btnTaskAdvanced_Click_8"))
                        //           {
                        //               cts.Cancel(); //就可以把IsCancellationRequested指定为true; 
                        //               throw new Exception("btnTaskAdvanced_Click_8  异常了。。。");
                        //           }
                        //           else if (name.Equals("btnTaskAdvanced_Click_13"))
                        //           {
                        //               cts.Cancel(); //就可以把IsCancellationRequested指定为true; 
                        //               throw new Exception("btnTaskAdvanced_Click_13  异常了。。。");
                        //           }
                        //           else if (name.Equals("btnTaskAdvanced_Click_25"))
                        //           {
                        //               cts.Cancel(); //就可以把IsCancellationRequested指定为true; 
                        //               throw new Exception("btnTaskAdvanced_Click_25  异常了。。。");
                        //           }
                        //           //如果有业务需要，其实也可以直接Cancel()，其他的线程也都停止下来了；

                        //           if (!cts.IsCancellationRequested)
                        //           {
                        //               Console.WriteLine($"{name}  End.....{Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                        //           }
                        //           else
                        //           {
                        //               //这里也可以回滚
                        //               //throw new AggregateException(); //停止了线程
                        //               Console.WriteLine($"{name} 失败了.....{Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                        //           }
                        //           //Console.WriteLine($"this is {name} 成功， ThreadID={Thread.CurrentThread.ManagedThreadId.ToString("00")}");
                        //       }, cts.Token)); //只需要把cts.Token 给Task.run()

                        //    }

                        //    Task.WaitAll(taskList.ToArray());
                        //}
                        //catch (AggregateException aex)
                        //{
                        //    foreach (var exception in aex.InnerExceptions)
                        //    {
                        //        Console.WriteLine(exception.Message);
                        //    }
                        //}
                    }
                }
                #endregion

                #region 临时变量
                {
                    //1.线程的开启是非阻塞的，延迟启动；
                    //2.这里是循环5次，代码执行很快，
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    int k = i;
                    //    Task.Run(() =>
                    //    {
                    //        Console.WriteLine($"ThreadID={Thread.CurrentThread.ManagedThreadId.ToString("00")}_i={i}__ k={ k}");
                    //    });
                    //}
                }
                #endregion

                #region 线程安全&lock
                {

                    //线程安全 
                    //1.BlockingCollection<T> 为实现 IProducerConsumerCollection<T> 的线程安全集合提供阻塞和限制功能。
                    //2.ConcurrentBag<T>	表示对象的线程安全的无序集合。
                    //3.ConcurrentDictionary<TKey, TValue>	表示可由多个线程同时访问的键值对的线程安全集合。
                    //4.ConcurrentQueue<T>	表示线程安全的先进先出 (FIFO) 集合。
                    //5.ConcurrentStack<T>	表示线程安全的后进先出 (LIFO) 集合。
                    //6.OrderablePartitioner<TSource>	表示将一个可排序数据源拆分成多个分区的特定方式。
                    //7.Partitioner	提供针对数组、列表和可枚举项的常见分区策略。
                    //8.Partitioner<TSource>	表示将一个数据源拆分成多个分区的特定方式。 
                    {
                        //List<int> tasklist = new List<int>(); 
                        //for (int i = 0; i < 10000; i++)
                        //{
                        //    int k = i;
                        //    Task.Run(() =>
                        //    {
                        //        tasklist.Add(k); 
                        //    });
                        //}
                        //Console.WriteLine($"tasklist:{tasklist.Count()}");
                        //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&"); 
                    }
                    {
                        //List<int> tasklist = new List<int>();
                        //BlockingCollection<int> blockinglist = new BlockingCollection<int>();
                        //ConcurrentBag<int> conocurrentbag = new ConcurrentBag<int>();
                        //ConcurrentDictionary<string, int> concurrentDictionary = new ConcurrentDictionary<string, int>(); 
                        //ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();
                        //ConcurrentStack<int> concurrentStack = new ConcurrentStack<int>();
                        ////OrderablePartitioner<int> OrderablePartitioner = new OrderablePartitioner<int>();
                        ////Partitioner<int> partitionerArray=new 
                        ////Partitioner
                        //for (int i = 0; i < 10000; i++)
                        //{
                        //    int k = i;
                        //    Task.Run(() =>
                        //    {
                        //        tasklist.Add(k);
                        //        blockinglist.Add(k);
                        //        conocurrentbag.Add(k);
                        //        concurrentDictionary.TryAdd($"concurrentDictionary_{k}", k);
                        //        concurrentQueue.Enqueue(k);
                        //        concurrentStack.Push(k);
                        //    });
                        //} 
                        //Console.WriteLine($"tasklist:{tasklist.Count()}");
                        //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        //Console.WriteLine($"blockinglist{blockinglist.Count()}");
                        //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        //Console.WriteLine($"conocurrentbag{conocurrentbag.Count()}");
                        //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        //Console.WriteLine($"concurrentDictionary{concurrentDictionary.Count()}");
                        //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        //Console.WriteLine($"concurrentQueue{concurrentQueue.Count()}");
                        //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        //Console.WriteLine($"concurrentStack{concurrentStack.Count()}");
                        //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&"); 
                    }


                }
                #endregion 
            }
            {
                /////临时变量
                ////1.线程的开启是非阻塞的，延迟启动；
                ////2.这里是循环5次，代码执行很快，开启线程不阻塞
                ////3.k 是每一次循环都定义了一个k，就是在作用于以内定义的变量---闭包；只是对当前作用域内的代码生效；
                //for (int i = 0; i < 10000; i++)
                //{
                //    int k = i;
                //    Task.Run(() =>
                //    {
                //        Console.WriteLine($"ThreadID={Thread.CurrentThread.ManagedThreadId.ToString("00")}_i={i}__ k={ k}");
                //    });
                //}
            }

            ///线程安全
            {

                //this.SyncNo = 0;
                //this.ASyncNo = 0; 
                //for (int i = 0; i < 10000; i++)
                //{
                //    this.SyncNo++; 
                //}

                //List<Task> taskList = new List<Task>(); 
                //for (int i = 0; i < 10000; i++)
                //{
                //    taskList.Add(Task.Run(() =>
                //    {
                //        this.ASyncNo++;
                //    }));
                //} 
                //Task.WaitAll(taskList.ToArray()); 
                //Console.WriteLine($" this.SyncNo={ this.SyncNo} _____   this.ASyncNo={this.ASyncNo}");
                //1.this.SyncNo=?   10000
                //2.this.ASyncNo=？  9989
                //随着不同的执行，this.SyncNo不变的，都是10000，因为是单线程执行；ASyncNo每次得到的结果都不一样，小于10000；
                //为什么？  因为线程安全问题：
                //线程安全：如果你的代码在单线程情况下执行的结果和多线程执行的结果完全一致，那么这就是线程安全的；
                //线程安全问题一般发生在全局变量、共享变量、硬盘文件，只要是多线程都能访问和修改的公共数据；
                //因为是多线程操作，操作同时进行，可能会出现覆盖；
                //怎么解决？
                //1.lock  -----可以----Richard 极力不推荐，因为lock是反多线程；
                //2.wait? ------不行
                //3.线程休眠-----不靠谱
                //4.延迟----也不行；

                //线程安全 
                //1.BlockingCollection<T> 为实现 IProducerConsumerCollection<T> 的线程安全集合提供阻塞和限制功能。
                //2.ConcurrentBag<T>	表示对象的线程安全的无序集合。
                //3.ConcurrentDictionary<TKey, TValue>	表示可由多个线程同时访问的键值对的线程安全集合。
                //4.ConcurrentQueue<T>	表示线程安全的先进先出 (FIFO) 集合。
                //5.ConcurrentStack<T>	表示线程安全的后进先出 (LIFO) 集合。
                //6.OrderablePartitioner<TSource>	表示将一个可排序数据源拆分成多个分区的特定方式。
                //7.Partitioner	提供针对数组、列表和可枚举项的常见分区策略。
                //8.Partitioner<TSource>	表示将一个数据源拆分成多个分区的特定方式。 
                {
                    //List<int> tasklist = new List<int>();
                    //for (int i = 0; i < 10000; i++)
                    //{
                    //    int k = i;
                    //    Task.Run(() =>
                    //    {
                    //        tasklist.Add(k);
                    //    });
                    //}
                    //Console.WriteLine($"tasklist:{tasklist.Count()}");
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                }

                //A.Richard 老师推荐：框架提供的；可以放心使用；
                //1. 引入System.Collections.Concurrent命名---线程安全数据结构
                //2. 把之前的非线程安全的数据结构更换成以下数据结构即可；
                {
                    //List<int> tasklist = new List<int>();
                    //BlockingCollection<int> blockinglist = new BlockingCollection<int>();
                    //ConcurrentBag<int> conocurrentbag = new ConcurrentBag<int>();
                    //ConcurrentDictionary<string, int> concurrentDictionary = new ConcurrentDictionary<string, int>();
                    //ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();
                    //ConcurrentStack<int> concurrentStack = new ConcurrentStack<int>();
                    ////OrderablePartitioner<int> OrderablePartitioner = new OrderablePartitioner<int>();
                    ////Partitioner<int> partitionerArray=new 
                    ////Partitioner
                    //for (int i = 0; i < 10000; i++)
                    //{
                    //    int k = i;
                    //    Task.Run(() =>
                    //    {
                    //        tasklist.Add(k);
                    //        blockinglist.Add(k);
                    //        conocurrentbag.Add(k);
                    //        concurrentDictionary.TryAdd($"concurrentDictionary_{k}", k);
                    //        concurrentQueue.Enqueue(k);
                    //        concurrentStack.Push(k);
                    //    });
                    //}
                    //Console.WriteLine($"tasklist:{tasklist.Count()}");
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    //Console.WriteLine($"blockinglist{blockinglist.Count()}");
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    //Console.WriteLine($"conocurrentbag{conocurrentbag.Count()}");
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    //Console.WriteLine($"concurrentDictionary{concurrentDictionary.Count()}");
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    //Console.WriteLine($"concurrentQueue{concurrentQueue.Count()}");
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    //Console.WriteLine($"concurrentStack{concurrentStack.Count()}");
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                }
                //B：Lock，锁---排他性，独占； 
                //标准锁：锁对象，引用类型，不要锁string,可能会冲突；
                //强烈规定：以后大家使用lock，按照这种标准格式;private static readonly object obj_Forom = new object();
                //int，string,this  都不要；
                {
                    //List<int> tasklist = new List<int>();
                    //for (int i = 0; i < 10000; i++)
                    //{
                    //    int k = i;
                    //    Task.Run(() =>
                    //    {
                    //        lock (obj_Forom) //只允许一个线程从这里经过,这不就是单线程了吗？  反多线程；
                    //        {
                    //            tasklist.Add(k);
                    //        }
                    //    });
                    //}
                    //Console.WriteLine($"tasklist:{tasklist.Count()}");
                }

                string str01 = "Richard";
                string str02 = "Richard";

                //C：还有没有其他的方案
                //线程安全问题一般发生在全局变量、共享变量、硬盘文件，只要是多线程都能访问和修改的公共数据；
                //既然既然多线程去操作会有线程安全问题，那么就拆分数据源，然后每一个线程对标于单独的某一个数据块；
                //多线程操作完毕以后，再合并数据；在后续爬虫实战会有应用；
            }
            Console.WriteLine($"****************button2_Click End   {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
        }


        private static readonly object obj_Forom = new object();
        private int SyncNo = 0;
        private int ASyncNo = 0;
    }
}
