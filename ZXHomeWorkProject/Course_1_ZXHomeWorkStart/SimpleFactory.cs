using HomeWork.DB.InterFace;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ZXHomeWork
{
    class SimpleFactory
    {
        public static IDBHelper getClass()
        {
            string ReflictionConfig = CustomConfigManager.GetConfig("ReflictionConfig");
            var ReflictionConfigS = ReflictionConfig.Split(",");
            //获取数据清单metadata
            Assembly assembly3 = Assembly.LoadFrom($"{ReflictionConfigS[0]}"); //dll名称（需要后缀） 
            ///2.获取类型
            Type type = assembly3.GetType($"{ReflictionConfigS[1]}");
            //创建实例
            object obj = Activator.CreateInstance(type);
            //类型转换。
            return obj as IDBHelper;
        }
        public static class CustomConfigManager
        {
            public static string GetConfig(string key)
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");  //默认读取  当前运行目录
                IConfigurationRoot configuration = builder.Build();
                string configValue = configuration.GetSection(key).Value;
                return configValue;
            }
        }
    }
}
