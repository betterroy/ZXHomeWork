using System;
using System.Collections.Generic;
using System.IO;

namespace Utility.Common
{
    public class FileControl
    {


        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="startupPath"></param>
        /// <param name="fileName"></param>
        /// <param name="addPath"></param>
        public static void MoveFile(string startupPath, string fileName, string addPath = "")
        {
            try
            {
                addPath = $"\\BackUp\\{addPath}\\";
                if (!Directory.Exists(startupPath + addPath))
                {
                    Directory.CreateDirectory(startupPath + addPath);
                }
                String fileNameOld = fileName;
                if (File.Exists(startupPath + addPath + fileName))
                {
                    fileName = fileName.Substring(0, fileName.IndexOf('.')) + DateTime.Now.Ticks.ToString() + fileName.Substring(fileName.IndexOf('.'), fileName.Length - fileName.IndexOf('.'));
                }
                File.Copy(startupPath + "\\" + fileNameOld, startupPath + addPath + fileName);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(ex.Message);
            }
        }




    }
}
