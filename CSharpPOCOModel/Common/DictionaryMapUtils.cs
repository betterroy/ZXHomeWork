using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPOCOModel.Common
{
    /// <summary>
    /// 字典转换助手
    /// </summary>
    public class DictionaryMapUtils
    {
        /// <summary>
        /// 对象转换为字典
        /// </summary>
        /// <param name="obj">待转化的对象</param>
        /// <param name="isIgnoreNull">是否忽略NULL 这里我不需要转化NULL的值，正常使用可以不穿参数 默认全转换</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObjectToDictionary(object obj, bool isIgnoreNull = false)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            // 获取对象对应的类， 对应的类型
            Type t = obj.GetType();
            // 获取当前type公共属性
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in pi)
            {
                MethodInfo m = p.GetGetMethod();
                if (m != null && m.IsPublic)
                {
                    // 进行判NULL处理 
                    if (m.Invoke(obj, new object[] { }) != null || !isIgnoreNull)
                    {
                        map.Add(p.Name, m.Invoke(obj, new object[] { })); // 向字典添加元素
                    }
                }
            }
            return map;
        }
        /// <summary>
		/// 复制单个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <returns></returns>
		public static T Copy<T>(object model)
        {
            if (model == null)
            {
                return default(T);
            }
            T t = Activator.CreateInstance<T>();
            Type type = t.GetType();
            foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
            {
                PropertyInfo property = type.GetProperty(propertyInfo.Name);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(t, propertyInfo.GetValue(model, null), null);
                }
            }
            return t;
        }
        /// <summary>
		/// 两个实体之间相同属性的映射
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <returns></returns>
		public static TResult Mapping<TResult, T>(T model)
        {
            TResult tresult = Activator.CreateInstance<TResult>();
            foreach (PropertyInfo propertyInfo in typeof(TResult).GetProperties())
            {
                PropertyInfo property = typeof(T).GetProperty(propertyInfo.Name);
                if (property != null)
                {
                    propertyInfo.SetValue(tresult, property.GetValue(model));
                }
            }
            return tresult;
        }
    }
}
