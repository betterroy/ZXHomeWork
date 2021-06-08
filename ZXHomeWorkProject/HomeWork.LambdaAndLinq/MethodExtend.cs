using HomeWork.Class.DelegateEventExtend;
using System;
using System.Collections.Generic;

namespace HomeWork.LambdaAndLinq
{
    public static class MethodExtend
    {
        public static void EatFish(this Fisherman fisherman)
        {
            Console.WriteLine($"That is a Extension Method.I'm a fisherman,I'm eating fish right now.{fisherman.Name}");
        }
        public static List<Fisherman> FishermenWhere (this List<Fisherman> listFishermen)
        {
            List<Fisherman> lf = new List<Fisherman>();
            foreach (var item in listFishermen)
            {
                if (item.Leavel > 1)
                {
                    lf.Add(item);
                    Console.WriteLine($"My leavel is bigger than one>>>>>>>>{item.Name}");
                }
            }
            return lf;
        }
        public static List<Fisherman> FishermenWhere(this List<Fisherman> listFishermen,Func<Fisherman,bool> func)
        {
            List<Fisherman> lf = new List<Fisherman>();
            foreach (var item in listFishermen)
            {
                if (func.Invoke(item))
                {
                    lf.Add(item);
                    Console.WriteLine($"My leavel is bigger than one>>>>>>>>{item.Name}");
                }
            }
            return lf;
        }
        public static List<T> ListWhereByRoy<T>(this List<T> listFishermen, Func<T, bool> func)
        {
            List<T> lf = new List<T>();
            foreach (var item in listFishermen)
            {
                if (func.Invoke(item))
                {
                    lf.Add(item);
                }
            }
            return lf;
        }
    }
}
