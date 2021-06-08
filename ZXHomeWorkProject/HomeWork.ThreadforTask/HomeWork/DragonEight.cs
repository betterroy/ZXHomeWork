using Course_6_HomeWork.ThreadforTask.HomeWork.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course_6_HomeWork.ThreadforTask.HomeWork.Common;
using System.Linq;
using System.Diagnostics;

namespace Course_6_HomeWork.ThreadforTask.HomeWork
{
    public partial class DragonEight : Form
    {
        public DragonEight()
        {
            InitializeComponent();
        }

        DramaScript DragonEightScript = new DramaScript();

        CancellationTokenSource cts = new CancellationTokenSource();

        private void DragonEight_Load(object sender, EventArgs e)
        {
            DragonEightScript.DramaName = "天龙八部";
            DragonEightScript.IsStart = true;
            DragonEightScript.people = new List<Person>();
            DragonEightScript.people.Add(new Person() { Name = "虚竹", Call = "小和尚,逍遥掌门,灵鹫宫宫主,西夏驸马" });
            DragonEightScript.people.Add(new Person() { Name = "乔峰", Call = "丐帮帮主,契丹人,南院大王,挂印离开" });
            DragonEightScript.people.Add(new Person() { Name = "段誉", Call = "钟灵儿,木婉清,王语嫣,大理国王" });

        }
        List<Task> taskListCall = new List<Task>();
        TaskFactory taskFactory = new TaskFactory();
        //锁
        private static readonly object obj_Lock = new object();


        ////假如说我想控制下Task的并发数量，该怎么做？  20个
        List<Task> taskList = new List<Task>();
        

        private void button1_Click(object sender, EventArgs e)
        {
            //taskFactory.StartNew(() =>
            //{
            //    for (int i = 0; i < 30; i++)
            //    {
            //        int k = i;
            //        if (taskList.Count(t => t.Status != TaskStatus.RanToCompletion) >= 20)
            //        {
            //            Task.WaitAny(taskList.ToArray());
            //            taskList = taskList.Where(t => t.Status != TaskStatus.RanToCompletion).ToList();
            //        }
            //        taskList.Add(Task.Run(() =>
            //        {
            //            cwStr($"This is {k} running ThreadId={Thread.CurrentThread.ManagedThreadId.ToString("00")}\r\n");
            //            Thread.Sleep(2000);
            //        }));
            //    }
            //});

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task.Run(() =>
            {
                while (true)
                {
                    var random = new Random();
                    int num = random.Next(0, 10000);
                    if (num == DateTime.Now.Year)
                    {
                        Thread.Sleep(random.Next(100, 200));
                        cts.Cancel();
                        break;
                    }
                }
            });
            foreach (var item in DragonEightScript.people)
            {
                taskListCall.Add(taskFactory.StartNew(o =>
                {
                    string[] strs = item.Call.Split(",");
                    for (int i = 0; i < strs.Length; i++)
                    {
                        //if (!cts.IsCancellationRequested)
                        //{
                            int k = i;
                            if (k == 0 && DragonEightScript.IsStart)
                            {
                                lock (obj_Lock)
                                {
                                    PrintWithColorByStep($"{item.Name}成就为：{strs[k]}\r\n");
                                    if (k == 0 && DragonEightScript.IsStart)
                                    {
                                        PrintWithColorByStep($"{item.Name}》》》》》》》》》》》》》天龙八部就此拉开序幕。。。。\r\n");
                                        DragonEightScript.IsStart = false;
                                    }
                                }
                            }
                            else
                            {
                                PrintWithColorByStep($"{item.Name}成就为：{strs[k]}\r\n");
                            }
                        //}
                        //else
                        //{
                        //    break;
                        //}
                    }
                }, item.Name,cts.Token));
            }
            taskFactory.ContinueWhenAny(taskListCall.ToArray(), p =>
            {
                PrintWithColorByStep($"{p.AsyncState}：“已经准备好了。。。”\r\n");
            });
            taskFactory.ContinueWhenAll(taskListCall.ToArray(), p =>
            {
                if (cts.IsCancellationRequested == false)
                {

                    Task t = p.First();
                    PrintWithColorByStep($"“中原群雄大战辽兵，忠义两难一死谢天”\r\n");
                    stopwatch.Stop();
                }
                else
                {
                    PrintWithColorByStep($"天降雷霆灭世，天龙八部的故事就此结束...");
                }
                PrintWithColorByStep($"整个天龙八部的故事花了：{stopwatch.ElapsedMilliseconds} ms");
            });
        }

        private static readonly object objVoice_Print = new object();
        public void PrintWithColorByStep(string content)
        {
            lock (objVoice_Print)
            {
                foreach (var s in content)
                {
                    cwStr(s.ToString());
                    Thread.Sleep(new Random().Next(50, 100));
                }
            }
        }
        public void cwStr(string str)
        {
            this.Invoke(new Action(() =>
            {
                richTextBox1.Text += str;
            }));
        }

    }
}
