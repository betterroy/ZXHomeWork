using Utility.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Collection.Data
{
    public partial class WriteLucene : Form
    {
        FormMethod formMethod = new FormMethod();
        public WriteLucene()
        {
            InitializeComponent();
            test();
        }

        private void test()
        {
            this.mtb_filepath.Text = @"C:\Users\Bette\Desktop\today\公司周模板\会议记要模板";
        }

        /// <summary>
        /// 读取全部数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (this.mtb_filepath.Text.IsNullOrWhiteSpace())
            {
                string savePath = formMethod.GetDialogPath();
                this.mtb_filepath.Text = savePath;
            }
            GetAllFiles(this.mtb_filepath.Text);
        }

        /// <summary>
        /// 获取指定根目录下的子目录及其文档
        /// </summary>
        /// <param name="rootPath">检索的文档根目录</param>
        private void GetAllFiles(string rootPath)
        {
            if (!System.IO.Directory.Exists(rootPath))
            {
                MessageBox.Show("指定的目录不存在");
                return;
            }

            List<FileInfo> files = new List<FileInfo>();
            TaskFactory taskFactory = new TaskFactory();
            List<Task> taskList = new List<Task>();

            //获取List列表
            toolStripLabel.Text = "遍历文件中...";
            btnRead.Enabled = false;
            btnRead1.Enabled = false;
            taskList.Add(
                taskFactory.StartNew(() =>
                {
                    files = ForFile(rootPath);
                })
            );
            taskFactory.ContinueWhenAll(taskList.ToArray(), t =>
            {
                taskList.Clear();
                settoolStripLabel($"文件遍历完成，共有{files.Count}个文件");

                settoolStripLabel($"开始建立索引...");
                //执行建立索引
                Task task = Task.Run(() =>
                {
                    CreateIndex(files);
                });
                Task.WaitAll(task);

                settoolStripLabel($"建立索引完成！");
                setButtonEnabled(btnRead, true);
                setButtonEnabled(btnRead1, true);
            });
        }

        /// <summary>
        /// 遍历获取到所有文件。
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        private List<FileInfo> ForFile(string rootPath)
        {
            List<FileInfo> files = new List<FileInfo>(); //声明一个files包，用来存储遍历出文档

            GetAllFiles(rootPath, files);

            return files;
        }

        /// <summary>
        /// 获取指定根目录下的子目录及其文档
        /// </summary>
        /// <param name="rootPath">根目录路径</param>
        /// <param name="files">word文档存储包</param>
        public void GetAllFiles(string rootPath, List<FileInfo> files)
        {
            DirectoryInfo dir = new DirectoryInfo(rootPath);
            string[] dirs = System.IO.Directory.GetDirectories(rootPath);//得到所有子目录
            foreach (string di in dirs)
            {
                GetAllFiles(di, files); //递归调用
            }
            FileInfo[] file = dir.GetFiles(); //查找word文件
            //遍历每个word文档
            foreach (FileInfo fi in file)
            {
                string filename = fi.Name;
                string filePath = fi.FullName;
                object filepath = filePath;
                setControlText($"读取到：{filePath}\r\n");

                files.Add(fi);
            }
        }


        /// <summary>
        /// 往lucene创建索引
        /// </summary>
        /// <param name="files">获得的文档包</param>
        private void CreateIndex(List<FileInfo> files)
        {
            int count = files.Count();
            for (int i = 0; i < count; i++)
            {
                FileInfo fileInfo = files[i];
                setControlText($"读取文件：{fileInfo.Name}中...\r\n");
                formMethod.WriteLucene(files[i]);
                setControlText($"读取文件：{fileInfo.Name}完成！\r\n");
            }
        }

        #region 设置文本与状态值

        /// <summary>
        /// 设置提示text框值
        /// </summary>
        /// <param name="val">追加值</param>
        /// <param name="isClear">是否先清空</param>
        public void setControlText(string val, bool isClear = false)
        {
            ExexAction(() =>
            {
                if (isClear)
                {
                    this.controlText.Clear();
                }
                this.controlText.AppendText(val);
            });
        }


        /// <summary>
        /// 设置提示toolStripLabel.Text值
        /// </summary>
        /// <param name="val">追加值</param>
        public void settoolStripLabel(string val)
        {
            ExexAction(() =>
            {
                this.toolStripLabel.Text=val;
            });
        }


        /// <summary>
        /// 设置提示Button Enabled值
        /// </summary>
        /// <param name="val">追加值</param>
        public void setButtonEnabled(Button button,bool val)
        {
            ExexAction(() =>
            {
                button.Enabled=val;
            });
        }

        /// <summary>
        /// 执行委托方法方法
        /// </summary>
        /// <param name="action"></param>
        public void ExexAction(Action action)
        {
            Action method = delegate
            {
                action.Invoke();
            };
            this.Invoke(method);
        }


        #endregion
    }
}
