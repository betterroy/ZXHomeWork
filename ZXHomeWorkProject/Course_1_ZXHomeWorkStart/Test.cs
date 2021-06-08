using HomeWork.Class;
using HomeWork.DB.InterFace;
using HomeWork.SqlHelper;
using System;
using System.Reflection;

namespace ZXHomeWork
{
    class Test
    {
        public void Main()
        {
            Console.WriteLine("获取反射类开始!");
            IDBHelper iDBHelper = SimpleFactory.getClass();
            //IDBHelper iDBHelper1 = SimpleFactory.getClass();
            //5.调用方法
            Company company = iDBHelper.Find(1);



            #region  创建带构造函数对象
            {
                //Console.WriteLine("*********************Reflection创建带构造函数参数的对象*************************");
                //Assembly assembly3 = Assembly.LoadFrom("HomeWork.SqlHelper.dll"); //dll名称（需要后缀）  
                //Type type = assembly3.GetType("HomeWork.SqlHelper.SqlServerHelper");
                //object obj = Activator.CreateInstance(type);
                //object obj1 = Activator.CreateInstance(type, new object[] { "你好" });
                //object obj2 = Activator.CreateInstance(type, new object[] { 123 });
                //object obj3 = Activator.CreateInstance(type, new object[] { 123, "你好" });
                //Type type1 = typeof(SqlServerHelper);
            }
            #endregion


            #region  反射调用方法
            {
                //Type type1 = typeof(ReflectionTest);
                ////普通调用方式
                //ReflectionTest reflectionTest = new ReflectionTest();
                //reflectionTest.Show1();
                //reflectionTest.Show2(123);
                //reflectionTest.Show3(123);
                ////reflectionTest.Show4
                //ReflectionTest.Show5("Richard");

                ////Console.WriteLine("*********************Reflection调用普通方法*************************");
                //Assembly assembly3 = Assembly.LoadFrom("HomeWork.SqlHelper.dll"); //dll名称（需要后缀）  
                //Type type = assembly3.GetType("HomeWork.SqlHelper.ReflectionTest");
                //object objTet = Activator.CreateInstance(type);
                ////objTet.Show();
                //MethodInfo Show1 = type.GetMethod("Show1");
                //object oResutl1 = Show1.Invoke(objTet, new object[] { });
                //object oResutl = Show1.Invoke(objTet, new object[0]);

                //MethodInfo Show2 = type.GetMethod("Show2");
                //object oResutl2 = Show2.Invoke(objTet, new object[] { 123 });

                ////Console.WriteLine("*********************Reflection调用普重载方法*************************");
                //MethodInfo Show33 = type.GetMethod("Show3",new Type[] { typeof(DateTime)});
                //object oResutl33 = Show33.Invoke(objTet, new object[] { DateTime.Now });
                //Console.WriteLine($"{ typeof(DateTime) }>>>>>oResutl33执行完的参数值{oResutl33}");


                //MethodInfo Show3 = type.GetMethod("Show3", new Type[] { typeof(int), typeof(string) });
                //object oResutl3 = Show3.Invoke(objTet, new object[] { 123, "阳光下的微笑" });

                //MethodInfo Show3_1 = type.GetMethod("Show3", new Type[] { typeof(string), typeof(int) });
                //object oResutl3_1 = Show3_1.Invoke(objTet, new object[] { "明日梦", 234 });

                //MethodInfo Show3_2 = type.GetMethod("Show3", new Type[] { typeof(int) });
                //object oResutl3_2 = Show3_2.Invoke(objTet, new object[] { 345 });

                //MethodInfo Show3_3 = type.GetMethod("Show3", new Type[] { typeof(string) });
                //object oResutl3_3 = Show3_3.Invoke(objTet, new object[] { "赤" });

                //MethodInfo Show3_4 = type.GetMethod("Show3", new Type[0]);
                //object oResutl3_4 = Show3_4.Invoke(objTet, new object[] { });

                //Console.WriteLine("*********************Reflection调用私有方法*************************");
                //MethodInfo Show4 = type.GetMethod("Show4", BindingFlags.NonPublic | BindingFlags.Instance);
                //object oResutl4 = Show4.Invoke(objTet, new object[] { "伟文" });

                //MethodInfo Show5 = type.GetMethod("Show5", BindingFlags.Static | BindingFlags.Public);
                //object oResutl5 = Show5.Invoke(objTet, new object[] { "追逐梦想的人。。" });
                //object oResutl5_1 = Show5.Invoke(null, new object[] { "you。。" });

            }
            #endregion

            #region 泛型类与方法


            {
                Console.WriteLine("*********************Reflections实例化泛型类+调用泛型方法*************************");
                {
                    //GenericMethod genericMethod = new GenericMethod();
                    //genericMethod.Show<int, string, DateTime>(123, "黄大仙", DateTime.Now);
                    //Assembly assembly3 = Assembly.LoadFrom("HomeWork.SqlHelper.dll"); //dll名称（需要后缀）  
                    //Type type = assembly3.GetType("HomeWork.SqlHelper.GenericMethod");
                    //object genericTest = Activator.CreateInstance(type);
                    //MethodInfo show = type.GetMethod("Show");
                    ////注意：需要指定泛型方法的泛型类型
                    //MethodInfo show1 = show.MakeGenericMethod(new Type[] { typeof(int), typeof(string), typeof(DateTime) });
                    //show1.Invoke(genericTest, new object[] { 123, "黄大仙", DateTime.Now });//如果是泛型方法，需要先确定类型，再执行方法，注意：指定的类型和传入的参数类型必须匹配
                }
                {
                    //Assembly assembly3 = Assembly.LoadFrom("HomeWork.SqlHelper.dll"); //dll名称（需要后缀）  
                    //Console.WriteLine(typeof(GenericClass<,,>));
                    ////这个能获取到的刷个1 否则刷个2
                    ////Type type = assembly3.GetType("HomeWork.SqlHelper.GenericClass`3");                    
                    ////MethodInfo show = type.GetMethod("Show");
                    ////MethodInfo show2 = show.MakeGenericMethod(new Type[] { typeof(DateTime), typeof(int), typeof(string) });

                    //Type type = assembly3.GetType("HomeWork.SqlHelper.GenericClass`3");
                    //Type type1 = type.MakeGenericType(new Type[] { typeof(int), typeof(string), typeof(DateTime) });
                    //object genericObj = Activator.CreateInstance(type1);
                    //MethodInfo show1 = type1.GetMethod("Show");
                    //show1.Invoke(genericObj, new object[] { 234, "工程师 冯兆", DateTime.Now });

                    ////GenericClass genericClass = new DB.SqlServer.GenericClass();
                    //GenericClass<int, string, DateTime> genericClass = new GenericClass<int, string, DateTime>();
                    //genericClass.Show(234, "工程师 冯兆", DateTime.Now);
                }
                {
                    //Assembly assembly = Assembly.LoadFrom("HomeWork.SqlHelper.dll"); //dll名称（需要后缀）  
                    //Type type = assembly.GetType("HomeWork.SqlHelper.GenericDouble`1");
                    //Type type1 = type.MakeGenericType(new Type[] { typeof(int) });
                    //var obj = Activator.CreateInstance(type1);
                    //MethodInfo show = type1.GetMethod("Show");
                    //MethodInfo show1 = show.MakeGenericMethod(new Type[] { typeof(string), typeof(DateTime) });
                    ////show1.Invoke(obj,new object[] { "cxx",DateTime.Now});
                    //show1.Invoke(obj, new object[] { 456, "cxx", DateTime.Now });
                }
                {
                    //反射：IOC
                    //反射应用于哪些框架；
                    //IOC框架；反射+配置文件+工厂==IOC框架中应用；

                    //反射--MVC
                    //dll： HomeWork.SqlHelper.dll
                    //type: HomeWork.SqlHelper.GenericDouble
                    //就可以创建对象
                    //type可以获取到Method===Method名称--字符串
                    //  dll名称+类名称+方法名称===可以调用这个方法 
                    // localhost://Home/Index/123== 可以调用到MVC项目中的某一个Action，你们觉得这是用的什么技术？
                    //MVC中调用方法就是反射的真实写照。。

                    //反射在ORM中的应用： 
                    //ORM---对象关系映射，就是通过对类的达成对数据库的操作；
                    //方法、属性、字段

                    //People people = new People();
                    //people.Id = 123;
                    //people.Name = "赤";
                    //people.Description = "高级班VIP学员";
                    ////people.Age = 34;
                    //Console.WriteLine($"people.Id={people.Id}");
                    //Console.WriteLine($"people.Name={people.Name}");
                    //Console.WriteLine($"people.Description={people.Description}");
                    //如果说People加了一个字段或者或者加了几个属性，普通方式=必须要修改代码--导致代码不稳定；
                    //反射的时候，在获取值的时候可以不用修改代码。。 

                    //那反射怎么做呢？
                    //Type type = typeof(User);
                    //object oPeople = Activator.CreateInstance(type);
                    ////oPeople.Id=
                    //foreach (PropertyInfo prop in type.GetProperties())
                    //{
                    //    if (prop.Name.Equals("Id"))
                    //    {
                    //        prop.SetValue(oPeople, 123);
                    //    }
                    //    else if (prop.Name.Equals("Name"))
                    //    {
                    //        prop.SetValue(oPeople, "赤");
                    //    }
                    //    //Console.WriteLine(prop.Name);
                    //}
                    //foreach (FieldInfo field in type.GetFields()) //获取所有的字段
                    //{
                    //    if (field.Name.Equals("Description"))
                    //    {
                    //        field.SetValue(oPeople, "高级班VIP学员");
                    //    }
                    //    //Console.WriteLine(field.Name);
                    //}
                    //foreach (PropertyInfo prop in type.GetProperties())
                    //{
                    //    Console.WriteLine(prop.GetValue(oPeople));
                    //}
                    //foreach (FieldInfo field in type.GetFields()) //获取所有的字段
                    //{
                    //    Console.WriteLine(field.GetValue(oPeople));
                    //}
                    ////准备给大家来一个手写ORM；ORM--就是通过类的使用达成对数据库的使用；无非就是增删改查；

                    {
                        //SqlServerHelper sqlServerHelper = new SqlServerHelper();
                        ////Company company = sqlServerHelper.QueryCompany(1); 
                        //Company company = sqlServerHelper.Find<Company>(1);
                        //User user = sqlServerHelper.Find<User>(1);
                        //Console.WriteLine(company);
                        //Console.WriteLine(user);
                    }
                    //升级一下。。
                    //如果我想要来查询Usernew？

                    {

                        //Type type = typeof(SqlServerHelper);
                        //type.ge
                    }
                }

            }

            #endregion
            Console.ReadLine();
            Console.WriteLine("获取反射类完成!");
        }
    }
}
