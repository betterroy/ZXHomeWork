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
    }
}
