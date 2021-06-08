using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace CSharpPOCOModel.Common
{
    public class JsonHandle<T> where T : class
    {
        static string startupPath = Application.StartupPath;
        //static string fileJsonName = ConfigurationManager.AppSettings["FileJsonName"].ToString();
        static string fileJsonName = "";
        static string jsonPath = Path.Combine(startupPath, "Json");
        //得到文件
        static string filePath = "";
        public JsonHandle()
        {
            fileJsonName = typeof(T).Name + ".json";
            filePath = Path.Combine(jsonPath, fileJsonName);
        }
        /// <summary>
        /// 读json
        /// </summary>
        /// <returns></returns>
        public List<T> ReadListJson()
        {
            try
            {
                CreateFile();
                string sResultByte = File.ReadAllText(filePath);
                List<T> jsonSqls = JsonConvert.DeserializeObject<List<T>>(sResultByte);
                if (jsonSqls == null)
                    jsonSqls = new List<T>();
                return jsonSqls;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 写json
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int WriteJson(T tempT)
        {
            try
            {
                List<T> ts = ReadListJson();
                ts.Add(tempT);
                ComWriteJsonList(ts);
                return 1;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// 写json
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int WriteJsonUnAppend(T tempT)
        {
            try
            {
                List<T> ts = new List<T>();
                ts.Add(tempT);
                ComWriteJsonList(ts);
                return 1;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(ex.Message);
                return 0;
            }
        }
        private void ComWriteJsonList(List<T> ts)
        {
            string fileJsonNameByType = typeof(T).Name;
            string json = JsonConvert.SerializeObject(ts);
            List<T> jsonSqls = JsonConvert.DeserializeObject<List<T>>(json);
            FileControl.MoveFile(jsonPath, fileJsonNameByType + ".json", fileJsonNameByType);
            File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);//将内容写进jon文件中
        }
        private void CreateFile()
        {
            if (!Directory.Exists(jsonPath))
            {
                Directory.CreateDirectory(jsonPath);
            }
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }

    }
}
