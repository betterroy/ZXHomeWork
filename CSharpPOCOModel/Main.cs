using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CSharpPOCOModel.Model;
using CSharpPOCOModel.Common;

namespace CSharpPOCOModel
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            int screenWidth = System.Windows.Forms.SystemInformation.WorkingArea.Width;
            int screenHeight = System.Windows.Forms.SystemInformation.WorkingArea.Height;

            int mywidth =Convert.ToInt32(screenWidth * 0.6); //屏幕宽度 
            int myheight = Convert.ToInt32(screenHeight * 0.8); //屏幕高度
            this.ClientSize = new System.Drawing.Size(mywidth, myheight);

            int x = (screenWidth - this.Size.Width) / 2;
            int y = (screenHeight - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (Point)new Size(x, y);         //窗体的起始位置为(x,y)

        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        private string connString = String.Empty;

        private List<string> listDescription = new List<string>();

        /// <summary>
        /// 连接到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 连接到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login(this);
            login.ShowDialog();
        }

        /// <summary>
        /// 系统初始化
        /// </summary>
        public void Init(string conn)
        {
            this.connString = conn;

            this.mlab_staus_tips.Text = @"连接成功...";

            LoadDatabase();

            CRichTextBoxMenu cRichTextBoxMenu = new CRichTextBoxMenu(this.mrtb_content);
            cRichTextBoxMenu.Init();
        }

        /// <summary>
        /// 加载数据库
        /// </summary>
        private void LoadDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connString))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    string cmdText = "use master;select * from sysdatabases ORDER BY name";
                    SqlCommand comm = new SqlCommand(cmdText, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);

                    //填充到DataSet
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "database_table");

                    this.mcb_database.DataSource = ds.Tables["database_table"];
                    this.mcb_database.DisplayMember = "name";
                    this.mcb_database.ValueMember = "name";
                }

                this.connString = Regex.Replace(this.connString, "Catalog=[0-9a-zA-Z_]+;", "Catalog=" + this.mcb_database.Text + ";");
                LoadTables(this.mcb_database.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"数据库加载错误！错误信息：" + ex.Message);
            }
        }

        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="database"></param>
        private void LoadTables(string database, string where = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connString))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string likeName = "";
                    if (where != "")
                    {
                        likeName = $" and name like '%{where}%'";
                    }
                    StringBuilder builder = new StringBuilder().AppendFormat($"use {database};select * from sysobjects where xtype='U' {likeName} ORDER BY NAME");
                    SqlCommand comm = new SqlCommand(builder.ToString(), conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);

                    //填充到DataSet
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "datatable_table");

                    this.mlist_datatable.DataSource = ds.Tables["datatable_table"];
                    this.mlist_datatable.DisplayMember = "name";
                    this.mlist_datatable.ValueMember = "name";
                    //清理
                    this.mlist_datatable.SelectedItems.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="tableName"></param>
        private void GenerateEntity(string tableName)
        {
            try
            {
                this.mlab_staus_tips.Text = string.Format("正在操作表[{0}]", tableName);

                string cmdText = $@"SELECT syscolumns.name as ColName,systypes.name as TypeName,sys.extended_properties.value as Description,sysobjects.name as TableName 
                                    , identification =case when COLUMNPROPERTY(syscolumns.id, syscolumns.name,'IsIdentity')= 1 then 1 else 0 end,
	                                isPrimary =case when exists(SELECT 1 FROM sysobjects where xtype = 'PK' and name in (
                                           SELECT name FROM sysindexes WHERE indid in(
                                           SELECT indid FROM sysindexkeys WHERE id = syscolumns.id AND colid = syscolumns.colid
	                                ))) then 1 else 0 END
                                    --,fieldLength = COLUMNPROPERTY(syscolumns.id, syscolumns.name, 'PRECISION')
	                                ,byteNum = syscolumns.length
                                FROM syscolumns
                                INNER join sysobjects on syscolumns.id = sysobjects.id inner join systypes on syscolumns.xtype = systypes.xtype
                                LEFT join sys.extended_properties on sys.extended_properties.major_id = syscolumns.id and sys.extended_properties.minor_id = syscolumns.colorder  
                                where sysobjects.name='{tableName}' and systypes.name<>'sysname' 
                                order by identification,ColName,sys.extended_properties.minor_id ASC";

                using (SqlConnection conn = new SqlConnection(this.connString))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    SqlCommand comm = new SqlCommand(cmdText.ToString(), conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);

                    //填充到DataSet
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "entity_table");
                    //实体
                    AddModel(ds);
                    //添加修改删除
                    AddInsertUpdateDelete(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"类生成失败！错误信息：" + ex.Message);
            }
        }
        #region 添加实体、增加、修改、删除、等语句
        /// <summary>
        /// 添加语句
        /// </summary>
        /// <param name="ds"></param>
        public void AddModel(DataSet ds)
        {
            int num = 0;
            StringBuilder stringBuilder = new StringBuilder();

            if (ds.Tables["entity_table"].Rows.Count == 0)
            {
                this.mrtb_content.Text = @"没有查询结果...";
            }
            else
            {
                stringBuilder.Append("using System;\r\n\r\n");

                if (!string.IsNullOrEmpty(this.mtb_namespace.Text.Trim()))
                {
                    stringBuilder.AppendLine("namespace " + this.mtb_namespace.Text + "\r\n{");
                    num += 4;
                }

                stringBuilder.Append("    /// <summary>\r\n");
                stringBuilder.Append($"    /// 创建人：{Environment.UserName}\r\n");
                stringBuilder.Append($"    /// 日  期：{DateTime.Now:yyyy.MM.dd HH:mm}\r\n");
                stringBuilder.Append($"    /// 描  述：{ds.Tables["entity_table"].Rows[0][3]}实体\r\n");
                stringBuilder.Append("    /// </summary>\r\n");

                stringBuilder.Append(new string(' ', num));
                stringBuilder.AppendLine("public class " + ds.Tables["entity_table"].Rows[0][3].ToString() + "Entity");
                stringBuilder.Append(new string(' ', num));
                stringBuilder.AppendLine("{");
                for (int i = 0; i < ds.Tables["entity_table"].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ds.Tables["entity_table"].Rows[i][2].ToString()))
                    {
                        stringBuilder.Append(new string(' ', num + 4));
                        stringBuilder.AppendLine("/// <summary>");
                        stringBuilder.Append(new string(' ', num + 4));
                        stringBuilder.AppendLine("/// " + ds.Tables["entity_table"].Rows[i][2]);

                        this.listDescription.Add(ds.Tables["entity_table"].Rows[i][2].ToString());

                        stringBuilder.Append(new string(' ', num + 4));
                        stringBuilder.AppendLine("/// </summary>");
                    }
                    stringBuilder.Append(new string(' ', num + 4));
                    stringBuilder.AppendLine(string.Concat(new string[]
                    {
                            "public ",
                            ds.Tables["entity_table"].Rows[i][1].ToString(),
                            " ",
                            ds.Tables["entity_table"].Rows[i][0].ToString(),
                            " { get; set; }"
                    }));
                }
                stringBuilder.Append(new string(' ', num));
                stringBuilder.AppendLine("}");
                if (!string.IsNullOrEmpty(this.mtb_namespace.Text.Trim()))
                {
                    stringBuilder.AppendLine("}");
                }
                string text = ChangeWords(stringBuilder.ToString());
                this.mrtb_content.Text = text;
                this.ChangeColor();
            }
        }
        /// <summary>
        /// 增删改语句
        /// </summary>
        /// <param name="ds"></param>
        public void AddInsertUpdateDelete(DataSet ds)
        {
            B_TableInfo b_TableInfo = new B_TableInfo();
            if (ds.Tables["entity_table"].Rows.Count == 0)
            {
                this.mrtb_content.Text = @"没有查询结果...";
            }
            else
            {
                b_TableInfo.TableName = ds.Tables["entity_table"].Rows[0][3].ToString();
                b_TableInfo.Rows = new List<B_RowInfo>();
                for (int i = 0; i < ds.Tables["entity_table"].Rows.Count; i++)
                {
                    B_RowInfo b_RowInfo = new B_RowInfo();
                    b_RowInfo.RowType = ds.Tables["entity_table"].Rows[i]["TypeName"].ToString();
                    b_RowInfo.RowName = ds.Tables["entity_table"].Rows[i]["ColName"].ToString();
                    b_RowInfo.RowByteNum = ds.Tables["entity_table"].Rows[i]["byteNum"].ToString();
                    b_RowInfo.Identification = Convert.ToInt32(ds.Tables["entity_table"].Rows[i]["identification"]);
                    b_TableInfo.Rows.Add(b_RowInfo);
                }
                //添加
                AddData(b_TableInfo);
                //修改
                UpdateDate(b_TableInfo);
                //删除
                DeleteDate(b_TableInfo);
            }
        }
        /// <summary>
        /// 添加语句
        /// </summary>
        /// <param name="ds"></param>
        public void AddData(B_TableInfo b_TableInfo)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("\r\n\r\n");
            stringBuilder.Append("\t//------------------------------------------添加------------------------------------------\r\n");
            stringBuilder.Append("\t/// <summary>\r\n");
            stringBuilder.Append($"\t/// 创建人：{Environment.UserName}\r\n");
            stringBuilder.Append($"\t/// 日  期：{DateTime.Now:yyyy.MM.dd HH:mm}\r\n");
            stringBuilder.Append($"\t/// 描  述：添加：add{b_TableInfo.TableName}\r\n");
            stringBuilder.Append("\t/// </summary>\r\n");
            stringBuilder.AppendLine($"\tpublic int add{b_TableInfo.TableName}()");
            stringBuilder.AppendLine("\t{");

            var proplist = b_TableInfo.Rows.Select(s => $"[{s.RowName}]");
            string props = string.Join(",", proplist); //以逗号分隔的字符串 
            string propsAnd = string.Join(",", proplist.Select(s => $"N'{s}'")); //以逗号分隔的字符串 

            stringBuilder.AppendLine($"\t\tstring sql = @\"INSERT INTO {b_TableInfo.TableName} ({props})");
            stringBuilder.AppendLine($"\t\tVALUES ({propsAnd})\";\r\n");

            stringBuilder.AppendLine("\t\tSqlParameter[] parameters = new SqlParameter[]{");

            StringBuilder stringBuilderRow = new StringBuilder();
            for (int i = 0; i < b_TableInfo.Rows.Count(); i++)
            {
                B_RowInfo row = b_TableInfo.Rows[i];
                if (i < b_TableInfo.Rows.Count() - 1)
                {
                    stringBuilder.AppendLine($"\t\t\tnew SqlParameter(\"@{row.RowName}\",SqlDbType.{row.RowType.FirstCharToUpper()},{row.RowByteNum}),");
                }
                else
                {
                    stringBuilder.AppendLine($"\t\t\tnew SqlParameter(\"@{row.RowName}\",SqlDbType.{row.RowType.FirstCharToUpper()},{row.RowByteNum})");
                }

                stringBuilderRow.AppendLine($"\t\tparameters[{i}].Value = {b_TableInfo.TableName}.{row.RowName};");
            }
            stringBuilder.AppendLine("\t\t};");
            stringBuilder.AppendLine(stringBuilderRow.ToString());

            stringBuilder.AppendLine("\t\ttry");
            stringBuilder.AppendLine("\t\t{");
            stringBuilder.AppendLine("\t\t\tDbHelperSQL.ExecuteSql(sql, parameters);");
            stringBuilder.AppendLine("\t\t}");
            stringBuilder.AppendLine("\t\tcatch (Exception ex)");
            stringBuilder.AppendLine("\t\t{");
            stringBuilder.AppendLine("\t\t\tthrow ex;");
            stringBuilder.AppendLine("\t\t}");
            
            if (!string.IsNullOrEmpty(this.mtb_namespace.Text.Trim()))
            {
                stringBuilder.AppendLine("\t}");
            }
            string text = stringBuilder.ToString();
            this.mrtb_content.Text += text;
            this.ChangeColor();
        }
        /// <summary>
        /// 修改语句
        /// </summary>
        /// <param name="ds"></param>
        public void UpdateDate(B_TableInfo b_TableInfo)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("\r\n\r\n");
            stringBuilder.Append("\t//------------------------------------------修改------------------------------------------\r\n");
            stringBuilder.Append("\t/// <summary>\r\n");
            stringBuilder.Append($"\t/// 创建人：{Environment.UserName}\r\n");
            stringBuilder.Append($"\t/// 日  期：{DateTime.Now:yyyy.MM.dd HH:mm}\r\n");
            stringBuilder.Append($"\t/// 描  述：修改：update{b_TableInfo.TableName}\r\n");
            stringBuilder.Append("\t/// </summary>\r\n");
            stringBuilder.AppendLine($"\tpublic int update{b_TableInfo.TableName}()");
            stringBuilder.AppendLine("\t{");

            var proplist = b_TableInfo.Rows.Select(s => $"[{s.RowName}]");
            string propsAnd = string.Join(",", proplist.Select(s => $"{s} = N'{s}'")); //以逗号分隔的字符串 

            stringBuilder.AppendLine($"\t\tstring sql = @\"UPDATE {b_TableInfo.TableName} SET {propsAnd} WHERE ID=@ID\r\n");

            stringBuilder.AppendLine("\t\tSqlParameter[] parameters = new SqlParameter[]{");

            StringBuilder stringBuilderRow = new StringBuilder();
            for (int i = 0; i < b_TableInfo.Rows.Count(); i++)
            {
                B_RowInfo row = b_TableInfo.Rows[i];
                if (i < b_TableInfo.Rows.Count() - 1)
                {
                    stringBuilder.AppendLine($"\t\t\tnew SqlParameter(\"@{row.RowName}\",SqlDbType.{row.RowType.FirstCharToUpper()},{row.RowByteNum}),");
                }
                else
                {
                    stringBuilder.AppendLine($"\t\t\tnew SqlParameter(\"@{row.RowName}\",SqlDbType.{row.RowType.FirstCharToUpper()},{row.RowByteNum})");
                }

                stringBuilderRow.AppendLine($"\t\tparameters[{i}].Value = {b_TableInfo.TableName}.{row.RowName};");
            }
            stringBuilder.AppendLine("\t\t};");
            stringBuilder.AppendLine(stringBuilderRow.ToString());

            stringBuilder.AppendLine("\t\ttry");
            stringBuilder.AppendLine("\t\t{");
            stringBuilder.AppendLine("\t\t\tDbHelperSQL.ExecuteSql(sql, parameters);");
            stringBuilder.AppendLine("\t\t}");
            stringBuilder.AppendLine("\t\tcatch (Exception ex)");
            stringBuilder.AppendLine("\t\t{");
            stringBuilder.AppendLine("\t\t\tthrow ex;");
            stringBuilder.AppendLine("\t\t}");

            if (!string.IsNullOrEmpty(this.mtb_namespace.Text.Trim()))
            {
                stringBuilder.AppendLine("\t}");
            }
            string text = stringBuilder.ToString();
            this.mrtb_content.Text += text;
            this.ChangeColor();
        }
        
        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="ds"></param>
        public void DeleteDate(B_TableInfo b_TableInfo)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("\r\n\r\n");
            stringBuilder.Append("\t//------------------------------------------删除------------------------------------------\r\n");
            stringBuilder.Append("\t/// <summary>\r\n");
            stringBuilder.Append($"\t/// 创建人：{Environment.UserName}\r\n");
            stringBuilder.Append($"\t/// 日  期：{DateTime.Now:yyyy.MM.dd HH:mm}\r\n");
            stringBuilder.Append($"\t/// 描  述：删除：del{b_TableInfo.TableName}\r\n");
            stringBuilder.Append("\t/// </summary>\r\n");
            stringBuilder.AppendLine($"\tpublic int del{b_TableInfo.TableName}()");
            stringBuilder.AppendLine("\t{");

            List<B_RowInfo> identifyRow = b_TableInfo.Rows.Where(w => w.Identification == 1 ? true : false).ToList();
            var proplist = identifyRow.Select(s => $"[{s.RowName}]");
            string propsAnd = string.Join(",", proplist.Select(s => $"{s} = N'{s}'")); //以逗号分隔的字符串 

            stringBuilder.AppendLine($"\t\tstring sql = @\"DELETE FROM {b_TableInfo.TableName} WHERE {propsAnd}\r\n");

            stringBuilder.AppendLine("\t\tSqlParameter[] parameters = new SqlParameter[]{");

            StringBuilder stringBuilderRow = new StringBuilder();
            for (int i = 0; i < identifyRow.Count(); i++)
            {
                B_RowInfo row = identifyRow[i];
                if (i < b_TableInfo.Rows.Count() - 1)
                {
                    stringBuilder.AppendLine($"\t\t\tnew SqlParameter(\"@{row.RowName}\",SqlDbType.{row.RowType.FirstCharToUpper()},{row.RowByteNum}),");
                }
                else
                {
                    stringBuilder.AppendLine($"\t\t\tnew SqlParameter(\"@{row.RowName}\",SqlDbType.{row.RowType.FirstCharToUpper()},{row.RowByteNum})");
                }

                stringBuilderRow.AppendLine($"\t\tparameters[{i}].Value ={b_TableInfo.TableName}.{row.RowName};");
            }
            stringBuilder.AppendLine("\t\t};");
            stringBuilder.AppendLine(stringBuilderRow.ToString());

            stringBuilder.AppendLine("\t\ttry");
            stringBuilder.AppendLine("\t\t{");
            stringBuilder.AppendLine("\t\t\tDbHelperSQL.ExecuteSql(sql, parameters);");
            stringBuilder.AppendLine("\t\t}");
            stringBuilder.AppendLine("\t\tcatch (Exception ex)");
            stringBuilder.AppendLine("\t\t{");
            stringBuilder.AppendLine("\t\t\tthrow ex;");
            stringBuilder.AppendLine("\t\t}");

            if (!string.IsNullOrEmpty(this.mtb_namespace.Text.Trim()))
            {
                stringBuilder.AppendLine("\t}");
            }
            string text = stringBuilder.ToString();
            this.mrtb_content.Text += text;
            this.ChangeColor();
        }


        #endregion
        /// <summary>
        /// 修正数据类型
        /// </summary>
        /// <param name="content">数据库数据类型</param>
        /// <returns></returns>
        private string ChangeWords(string content)
        {
            string input = Regex.Replace(content, "nvarchar", "string");
            input = Regex.Replace(input, "bit", "bool");
            input = Regex.Replace(input, "varchar", "string");
            input = Regex.Replace(input, "text", "string");
            input = Regex.Replace(input, "ntext", "string");
            input = Regex.Replace(input, "nchar", "string");
            input = Regex.Replace(input, "char", "string");
            input = Regex.Replace(input, "tinyint", "byte");
            input = Regex.Replace(input, "smallint", "short");
            input = Regex.Replace(input, "bigint", "long");
            input = Regex.Replace(input, "numeric", "decimal");
            input = Regex.Replace(input, "money", "decimal");
            input = Regex.Replace(input, "float", "double");
            input = Regex.Replace(input, "real", "float");
            input = Regex.Replace(input, "uniqueidentifier", "Guid");
            input = Regex.Replace(input, "datetime", "DateTime");
            input = Regex.Replace(input, "image", "byte[]");
            input = Regex.Replace(input, "binary", "byte[]");

            return input;
        }

        /// <summary>
        /// 着色
        /// </summary>
        private void ChangeColor()
        {
            this.mrtb_content.SelectionStart = 0;
            this.mrtb_content.SelectionLength = this.mrtb_content.Text.Length;
            this.mrtb_content.SelectionColor = Color.Black;
            if (this.listDescription.Count > 0)
            {
                this.ChangeKeyColor(this.listDescription, Color.Green);
            }
            this.ChangeKeyColor("using", Color.Blue);
            this.ChangeKeyColor("namespace", Color.Blue);
            this.ChangeKeyColor("public", Color.Blue);
            this.ChangeKeyColor("class", Color.Blue);
            this.ChangeKeyColor("/// <summary>", Color.Gray);
            this.ChangeKeyColor("///", Color.Gray);
            this.ChangeKeyColor("/// </summary>", Color.Gray);
            this.ChangeKeyColor("int", Color.Blue);
            this.ChangeKeyColor("long", Color.Blue);
            this.ChangeKeyColor("double", Color.Blue);
            this.ChangeKeyColor("float", Color.Blue);
            this.ChangeKeyColor("char", Color.Blue);
            this.ChangeKeyColor("string", Color.Blue);
            this.ChangeKeyColor("bool", Color.Blue);
            this.ChangeKeyColor("decimal", Color.Blue);
            this.ChangeKeyColor("enum", Color.Blue);
            this.ChangeKeyColor("const", Color.Blue);
            this.ChangeKeyColor("struct", Color.Blue);
            this.ChangeKeyColor("DateTime", Color.CadetBlue);
            this.ChangeKeyColor("get", Color.Blue);
            this.ChangeKeyColor("set", Color.Blue);
            //this.ChangeKeyColor("insert", Color.Blue);
            //this.ChangeKeyColor("update", Color.Blue);
            //this.ChangeKeyColor("delete", Color.Blue);
            //this.ChangeKeyColor("values", Color.Blue);
            this.ChangeKeyColor("Int", Color.Blue);
            this.ChangeKeyColor("Decimal", Color.Blue);
            this.ChangeKeyColor("Bigint", Color.Blue);
            this.ChangeKeyColor("Varchar", Color.Blue);
            this.ChangeKeyColor("Nvarchar", Color.Blue);
            this.ChangeKeyColor("SqlDbType", Color.Blue);
            this.ChangeKeyColor("try", Color.Blue);
            this.ChangeKeyColor("catch", Color.Blue);
            this.ChangeKeyColor("throw", Color.Blue);
            this.ChangeKeyColor("DbHelperSQL", Color.Blue);
            this.ChangeKeyColor("new", Color.Blue);
            this.ChangeKeyColor("SqlParameter", Color.Green);
            this.ChangeKeyColor("Exception", Color.Green);
        }

        /// <summary>
        /// 着色
        /// </summary>
        /// <param name="key"></param>
        /// <param name="color"></param>
        private void ChangeKeyColor(string key, Color color)
        {
            Regex regex = new Regex(key);
            MatchCollection matchCollection = regex.Matches(this.mrtb_content.Text);
            foreach (Match match in matchCollection)
            {
                this.mrtb_content.SelectionStart = match.Index;
                this.mrtb_content.SelectionLength = key.Length;
                this.mrtb_content.SelectionColor = color;
            }
        }

        /// <summary>
        /// 描述信息着色
        /// </summary>
        /// <param name="list"></param>
        /// <param name="color"></param>
        private void ChangeKeyColor(List<string> list, Color color)
        {
            foreach (string current in list)
            {
                this.ChangeKeyColor(current, color);
            }
        }

        /// <summary>
        /// 选择数据库，加载数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mcb_database_DropDownClosed(object sender, EventArgs e)
        {
            string text = this.mcb_database.Text;
            this.connString = Regex.Replace(this.connString, "Catalog=[0-9a-zA-Z_]+;", "Catalog=" + text + ";");
            this.LoadTables(text);
        }

        /// <summary>
        /// 选择数据表，生成实体代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mlist_datatable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateEntity(this.mlist_datatable.Text);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选择文件保存地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbtn_selectpath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = @"请选择文件路径"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = dialog.SelectedPath;
                this.mtb_filepath.Text = savePath;
            }
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbtn_tofile_Click(object sender, EventArgs e)
        {
            string fileName = this.mlist_datatable.Text;
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(@"请选择数据表");
            }
            else
            {
                string path = this.mtb_filepath.Text + "\\File\\";
                if (string.IsNullOrEmpty(path)) path = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = path + "\\" + fileName + ".cs";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.Default);
                streamWriter.Write(this.mrtb_content.Text);

                streamWriter.Flush();
                streamWriter.Close();
                streamWriter.Dispose();

                MessageBox.Show(@"文件生成成功！");
            }
        }

        #region 其他

        //SQLServer类型    C#类型
        //    bit           bool
        //    tinyint       byte
        //    smallint      short
        //    int           int
        //    bigint        long
        //    real          float
        //    float         double
        //    money         decimal
        //    datetime      DateTime
        //    char          string
        //    varchar       string
        //    nchar         string
        //    nvarchar      string
        //    text          string
        //    ntext         string
        //    image         byte[]
        //    binary        byte[]
        //    uniqueidentifier    Guid

        #endregion

        private void mbtn_reset_Click(object sender, EventArgs e)
        {
            GenerateEntity(this.mlist_datatable.Text);
        }

        private void TableText_TextChanged(object sender, EventArgs e)
        {
            LoadTables(this.mcb_database.Text, TableText.Text);
        }
    }
}
