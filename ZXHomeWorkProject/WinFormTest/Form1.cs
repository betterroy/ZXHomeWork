using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Parse("2021-01-01");
            int weeknow = Convert.ToInt32(now.DayOfWeek);
            now = DateTime.Parse("2021-01-02");
            weeknow = Convert.ToInt32(now.DayOfWeek);
            now = DateTime.Parse("2020-12-31");
            weeknow = Convert.ToInt32(now.DayOfWeek);
            now = DateTime.Parse("2021-01-11");
            weeknow = Convert.ToInt32(now.DayOfWeek);
            now = DateTime.Parse("2021-01-12");
            weeknow = Convert.ToInt32(now.DayOfWeek);
        }
        public delegate void delegateTest();
        public delegate void delegateTestS(int i);
        private void button2_Click(object sender, EventArgs e)
        {
            delegateTest _delegateTest = new delegateTest(Fist);
            _delegateTest.Invoke();
            delegateTestS _delegateTestS = new delegateTestS(Sencond);
            _delegateTestS.Invoke(1);

            delegateTest _delegateTest1 = new delegateTest(()=> {
                addStr("张峣sbbbbbbbb\r\n");
            });
            _delegateTest1.Invoke();


            Action action = new Action(Fist);
            action.Invoke();
            Action<int> action1 = new Action<int>(Sencond);
            action1.Invoke(1);
        }
        public void Fist()
        {
            addStr("张峣sb");
        }
        public void Sencond( int i)
        {
            addStr($"张峣sb{i}");
        }
        public void addStr(string str)
        {
            textBox1.Text += $"{str}\r\n";
        }
    }
}
