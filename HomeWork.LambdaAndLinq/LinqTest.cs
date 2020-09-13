using HomeWork.Class.DelegateEventExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.LambdaAndLinq
{
    public class LinqTest
    {
        public static void show()
        {
            List<Fisherman> listFisherman = new List<Fisherman>();
            Fisherman fisherman1 = new Fisherman() { ID = 1, Name = "roy", Description = "我是一个初级入门钓者", Leavel = 1 };
            listFisherman.Add(fisherman1);
            Fisherman fisherman2 = new Fisherman() { ID = 1, Name = "better roy", Description = "我是一个中级入门钓者", Leavel = 2 };
            listFisherman.Add(fisherman2);
            fisherman1.EatFish();


            Console.WriteLine("************************normal 扩展方法************************");
            List<Fisherman> fishermen1 = listFisherman.FishermenWhere();
            Console.WriteLine("************************normal 扩展方法************************");
            Console.WriteLine();

            Console.WriteLine("************************Func 扩展方法************************");
            List<Fisherman> fishermen2 = listFisherman.FishermenWhere(item => item.Leavel > 1);
            Console.WriteLine("************************Func 扩展方法************************");
            Console.WriteLine();

            Console.WriteLine("************************Linq 扩展方法************************");
            List<Fisherman> fishermen3 = listFisherman.ListWhereByRoy<Fisherman>(item => item.Leavel > 1);
            Console.WriteLine("************************Linq 扩展方法************************");
            Console.WriteLine();

        }
    }
}
