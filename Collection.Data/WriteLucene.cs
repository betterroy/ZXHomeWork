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
            taskFactory.ContinueWhenAll(taskList.ToArray(), t => { taskList.Clear(); Thread.Sleep(1000); });
            toolStripLabel.Text = "文件遍历完成";

            //建立索引
            taskList.Add(
                taskFactory.StartNew(() =>
                {
                    files = ForFile(rootPath);
                })
            );

            btnRead.Enabled = true;
            btnRead1.Enabled = true;
        }

        private List<FileInfo> ForFile(string rootPath)
        {
            List<FileInfo> files = new List<FileInfo>(); //声明一个files包，用来存储遍历出的word文档

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
            //FileInfo[] file = dir.GetFiles("*.doc"); //查找word文件
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
        /// 创建索引
        /// </summary>
        /// <param name="files">获得的文档包</param>
        //private void CreateIndex(List<FileInfo> files)
        //{
        //    bool isCreate = false;
        //    //判断是创建索引还是增量索引
        //    if (!System.IO.Directory.Exists(indexDirectory))
        //    {
        //        isCreate = true;
        //    }
        //    IndexWriter writer = new IndexWriter(FSDirectory.Open(indexDirectory), analyzer, isCreate, IndexWriter.MaxFieldLength.UNLIMITED);  //FSDirectory表示索引存放在硬盘上，RAMDirectory表示放在内存上
        //    for (int i = 0; i < files.Count(); i++)
        //    {
        //        //读取word文档内容
        //        Microsoft.Office.Interop.Word.ApplicationClass wordapp = new Microsoft.Office.Interop.Word.ApplicationClass();
        //        string filename = files[i].Name;
        //        object file = files[i].DirectoryName + "\\" + filename;
        //        object isreadonly = true;
        //        object nullobj = System.Reflection.Missing.Value;
        //        Microsoft.Office.Interop.Word._Document doct = wordapp.Documents.Open(ref file, ref nullobj, ref isreadonly, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
        //        //doct.ActiveWindow.Selection.WholeStory();
        //        //doct.ActiveWindow.Selection.Copy();
        //        //IDataObject data = Clipboard.GetDataObject();
        //        ////读出的内容赋给content变量
        //        //string content = data.GetData(DataFormats.Text).ToString();
        //        string content = doct.Content.Text;
        //        FileInfo fi = new FileInfo(file.ToString());
        //        string createTime = fi.CreationTime.ToString();
        //        string filemark = files[i].DirectoryName + createTime;
        //        //关闭word
        //        object missingValue = Type.Missing;
        //        object miss = System.Reflection.Missing.Value;
        //        object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
        //        doct.Close(ref saveChanges, ref missingValue, ref missingValue);
        //        wordapp.Quit(ref saveChanges, ref miss, ref miss);
        //        //  StreamReader reader = new StreamReader(fileInfo.FullName);读取txt文件的方法，如读word会出现乱码，不适用于word的读取
        //        Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();

        //        writer.DeleteDocuments(new Term("filemark", filemark)); //当索引文件中含有与filemark相等的field值时，会先删除再添加，以防出现重复
        //        doc.Add(new Lucene.Net.Documents.Field("filemark", filemark, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.NOT_ANALYZED));   //不分词建索引
        //        doc.Add(new Lucene.Net.Documents.Field("FileName", filename, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED)); //ANALYZED分词建索引
        //        doc.Add(new Lucene.Net.Documents.Field("Content", content, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.ANALYZED));
        //        doc.Add(new Lucene.Net.Documents.Field("Path", file.ToString(), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED));
        //        writer.AddDocument(doc);
        //        writer.Optimize();//优化索引
        //    }
        //    writer.Dispose();
        //}


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
        /// 执行方法
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
    }
}
