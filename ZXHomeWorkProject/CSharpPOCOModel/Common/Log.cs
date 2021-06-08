using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharpPOCOModel.Common
{
    public class Log
    {
        #region 日志
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="strMsg"></param>
        public static void WriteToLog(string strMsg)
        {
            try
            {
                string startupPath = Application.StartupPath;
                if (!Directory.Exists(startupPath + "\\LogFiles"))
                {
                    Directory.CreateDirectory(startupPath + "\\LogFiles");
                }
                string fileName = startupPath + "\\LogFiles\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                FileInfo fileInfo = new FileInfo(fileName);
                StreamWriter streamWriter = fileInfo.AppendText();
                streamWriter.WriteLine(string.Concat(new object[]
                {
                    DateTime.Now,
                    ":",
                    strMsg,
                    "\r\n"
                }));
                streamWriter.Close();
            }
            catch
            {
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion
    }
}
