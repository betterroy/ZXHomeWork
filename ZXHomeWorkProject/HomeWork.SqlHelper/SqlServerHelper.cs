using HomeWork.Class;
using HomeWork.Class.AttributeExtend;
using HomeWork.DB.InterFace;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace HomeWork.SqlHelper
{
    public class SqlServerHelper : IDBHelper
    {
        public SqlServerHelper()
        {
            Console.WriteLine($"这里是{this.GetType()}无参数构造函数");
        }
        public SqlServerHelper(string param1)
        {
            Console.WriteLine($"这里是{this.GetType()}有参数构造函数");
        }
        public SqlServerHelper(int param1)
        {
            Console.WriteLine($"这里是{this.GetType()}有参数构造函数");
        }
        public SqlServerHelper(int param1, string param2)
        {
            Console.WriteLine($"这里是{this.GetType()}有参数构造函数");
        }
        public Company Find(int id)
        {
            Company company = new Company();
            return company;
        }
        public List<T> Find<T>() where T : BaseModel
        {
            List<T> oResultS = new List<T>();
            string constr = "server=.;database=CustomerDB;uid=sa;pwd=sasa";

            Type type = typeof(T);

            var propList = type.GetProperties().Select(p => $"[{p.Name}]");
            string props = string.Join(",", propList);

            string sql = $"select {props} from  [{type.Name}]";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = sql;
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object oResult = Activator.CreateInstance(type);
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(oResult, reader[prop.Name]);
                        }
                        oResultS.Add(oResult as T);
                    }
                }
            }
            return oResultS as List<T>;
        }
        public List<T> FindOtherName<T>() where T : BaseModel
        {
            List<T> oResultS = new List<T>();
            string constr = "server=.;database=CustomerDB;uid=sa;pwd=sasa";

            Type type = typeof(T);
            var props = "";
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.IsDefined(typeof(RemarkAttribute), true))
                {
                    RemarkAttribute remarkAttribute = (RemarkAttribute)property.GetCustomAttribute(typeof(RemarkAttribute), true);
                    props += $"{remarkAttribute.remark} as {property.Name},";
                }
                else
                {
                    props += $"{property.Name},";
                }
            }

            string sql = $"select {props.Substring(0,props.Length-1)} from  [{type.Name}]";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = sql;
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object oResult = Activator.CreateInstance(type);
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(oResult, reader[prop.Name]);
                        }
                        oResultS.Add(oResult as T);
                    }
                }
            }
            return oResultS as List<T>;
        }
        public T Find<T>(int id) where T : BaseModel
        {
            string constr = "server=.;database=CustomerDB;uid=sa;pwd=sasa";

            Type type = typeof(T);
            object oResult = Activator.CreateInstance(type);

            var propList = type.GetProperties().Select(p => $"[{p.Name}]");
            string props = string.Join(",", propList);

            string sql = $"select {props} from  [{type.Name}] where id={id}";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = sql;
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(oResult, reader[prop.Name]);
                        }
                    }
                }
            }
            return oResult as T;
        }
        /// <summary>
        /// 实体添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int InsertModel<T>(T Model) where T : BaseModel
        {
            string constr = "server=.;database=CustomerDB;uid=sa;pwd=sasa";

            Type type = typeof(T);
            //object oResult = Activator.CreateInstance(type);

            var propLists = type.GetProperties().Where(p => p.Name != "Id");
            var propList = propLists.Select(p => $"[{p.Name}]");
            string props = string.Join(",", propList);
            var propValuesList = propLists.Select(p => $"'{p.GetValue(Model)}'");
            string propsValue = string.Join(",", propValuesList);

            string sql = $@"insert into [{type.Name}] ({props}) 
                                values ({propsValue})  ";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = sql;
                    con.Open();
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
        }

        /// <summary>
        /// 实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int UpdateModel<T>(T Model) where T : BaseModel
        {
            string constr = "server=.;database=CustomerDB;uid=sa;pwd=sasa";

            Type type = typeof(T);

            var propLists = type.GetProperties().Where(p => p.Name != "Id");
            var propList = propLists.Select(p => $"[{p.Name}]='{p.GetValue(Model)}'");
            string props = string.Join(",", propList);
            //var propValuesList = propLists.Select(p => $"'{p.GetValue(Model)}'");
            //string propsValue = string.Join(",", propValuesList);

            string sql = $@"update [{type.Name}] set {props} where ID={Convert.ToInt32(Model.GetType().GetProperty("Id").GetValue(Model))}";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = sql;
                    con.Open();
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
        }
        /// <summary>
        /// 实体删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int DelModel<T>(T Model) where T : BaseModel
        {
            string constr = "server=.;database=CustomerDB;uid=sa;pwd=sasa";

            Type type = typeof(T);

            string sql = $@"delete from [{type.Name}] where ID={Convert.ToInt32(Model.GetType().GetProperty("Id").GetValue(Model))}";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = sql;
                    con.Open();
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
        }
        public void query()
        {
            throw new NotImplementedException();
        }
        #region 打印
        public void PrintT<T>(T t)
        {
            Type type = t.GetType();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"************************当前对象为{type.Name}************************");
            foreach (PropertyInfo prop in type.GetProperties())
            {
                Console.WriteLine($"{prop.Name}:{prop.GetValue(t)}》》》");
            }
            Console.WriteLine($"************************当前对象打印结束{type.Name}************************");
            Console.WriteLine();
            Console.WriteLine();
        }
        public void PrintT<T>(List<T> ts)
        {
            foreach (T t in ts)
            {
                Type type = t.GetType();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"************************当前对象为{type.Name}************************");
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    Console.WriteLine($"{prop.Name}:{prop.GetValue(t)}》》》");
                }
                Console.WriteLine($"************************当前对象打印结束{type.Name}************************");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        #endregion



        public T ConsoleReadLine<T>(T t) where T : class
        {
            Type type = t.GetType();
            object oPeople = Activator.CreateInstance(type);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"************************请根据提示输入内容；{type.Name}************************");
            foreach (PropertyInfo prop in type.GetProperties())
            {
                Console.Write($"{prop.Name}》》》");
                string read = Console.ReadLine();
                prop.SetValue(oPeople, Convert.ChangeType(read, prop.PropertyType));
            }
            Console.WriteLine($"************************请根据提示输入内容；{type.Name}************************");
            Console.WriteLine();
            Console.WriteLine();
            return oPeople as T;
        }
    }
}
