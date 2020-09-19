using HomeWork.Class;
using HomeWork.Class.AttributeExtend;
using HomeWork.Class.DelegateEventExtend;
using HomeWork.Class.DelegateExtend;
using HomeWork.DB.InterFace;
using HomeWork.LambdaAndLinq;
using HomeWork.SqlHelper;
using HomeWork.ThreadS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace ZXHomeWork
{
    class Program
    {
        static void Main(string[] args)
        {

            //Dictionary<string, string> myDic = new Dictionary<string, string>();
            //myDic.Add("param1", "1");//公司
            //myDic.Add("param2", "2");//订单号
            //myDic.Add("param3", "3");//物料
            //Console.WriteLine(myDic.ToString()) ;
            ////IDBHelper iDBHelper = SimpleFactory.getClass();
            //////5.调用方法
            ////Company company = iDBHelper.Find(1);
            //Console.WriteLine("************根据ID获取开始************");
            //SqlServerHelper sqlServerHelper = new SqlServerHelper();
            //Company company = sqlServerHelper.Find<Company>(1);
            //sqlServerHelper.PrintT<Company>(company);
            //User user = sqlServerHelper.Find<User>(1);
            //sqlServerHelper.PrintT<User>(user);
            //Console.WriteLine("************根据ID获取结束************");

            //printNullLine();

            //Console.WriteLine("************************获取全部开始************************");
            //List<Company> companyS = sqlServerHelper.Find<Company>();
            //sqlServerHelper.PrintT<Company>(companyS);
            //List<User> userS = sqlServerHelper.Find<User>();
            //sqlServerHelper.PrintT<User>(userS);
            //Console.WriteLine("************************根据ID获取结束************************");

            //printNullLine();
            //Console.WriteLine("************************泛型实体数据库插入************************");
            //Company company1 = new Company();
            //company1 = sqlServerHelper.ConsoleReadLine<Company>(company1);
            //int result = sqlServerHelper.InsertModel<Company>(company1);
            //Console.WriteLine($"执行成功，影响的行数为:{result}行。");
            //Console.WriteLine("************************泛型实体数据库插入************************");

            //printNullLine();
            //Console.WriteLine("************************泛型实体数据库更新************************");
            //Company company1 = new Company();
            //company1.Name = "小王";
            //company1.CreateTime = DateTime.Now;
            //company1.LastModifyTime = DateTime.Now;
            //company1.Id = Convert.ToInt32(Console.ReadLine());
            //int result = sqlServerHelper.UpdateModel<Company>(company1);
            //Console.WriteLine($"执行成功，影响的行数为:{result}行。");
            //Console.WriteLine("************************泛型实体数据库更新************************");
            //printNullLine();
            //Console.WriteLine("************************泛型实体数据库删除************************");
            //Company company1 = new Company();
            //company1.Id = Convert.ToInt32(Console.ReadLine());
            //int result = sqlServerHelper.DelModel<Company>(company1);
            //Console.WriteLine($"执行成功，影响的行数为:{result}行。");
            //Console.WriteLine("************************泛型实体数据库删除************************");
            //printNullLine();
            //Console.WriteLine("************************实体验证************************");
            //User user = new User();
            //Console.Write("密码3位以上：");
            //user.Password = "3333";
            //Console.Write("邮箱4位以上：");
            //user.Email = "44444";
            //bool result = ValidateAttribute.Validate<User>(user);
            //Console.WriteLine($"验证是否合法:{result}。");
            //Console.WriteLine("************************实体验证************************");
            //printNullLine();
            //Console.WriteLine("************************枚举返回特性值************************");
            //ValidateAttribute.GetRemark(UserState.Normal);
            //ValidateAttribute.GetRemark(UserState.Frozen);
            //UserState.Deleted.GetRemark();
            //ValidateAttribute.GetRemark1(UserState.Deleted);
            //Console.WriteLine("************************枚举返回特性值************************");

            //printNullLine();
            //Console.WriteLine("************************获取全部开始************************");
            //List<OtherName> otherName = sqlServerHelper.FindOtherName<OtherName>();
            //sqlServerHelper.PrintT<OtherName>(otherName);
            //Console.WriteLine("************************根据ID获取结束************************");

            //printNullLine();
            //Console.WriteLine("************************委托初尝试************************");
            //NormalExtend normalExtend = new NormalExtend();
            //normalExtend.Do();
            //Console.WriteLine("************************委托初尝试************************");

            //printNullLine();
            //Console.WriteLine("************************委托套娃************************");
            //CustomExtend customExtend = new CustomExtend();
            //customExtend.Show();
            //Console.WriteLine("************************委托套娃************************");


            //Console.WriteLine("************************发布订阅模式************************");
            //HookFishStandard.Show();
            //Console.WriteLine("************************发布订阅模式************************");

            //printNullLine();
            //Console.WriteLine("************************Test Func and Action************************");
            //Func<string, string> func = new Func<string, string>(NormalExtend.FuncDo);
            //func += NormalExtend.FuncDo2;
            //string name = "roy";
            //name+=func.Invoke(name);
            //Console.WriteLine("************************Test Func and Action************************");

            //Console.WriteLine("************************Lambda And Linq************************");
            //LinqTest.show();
            //Console.WriteLine("************************Lambda And Linq************************");
            //printNullLine();


            Console.ReadLine();
        }
        static void printNullLine()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
