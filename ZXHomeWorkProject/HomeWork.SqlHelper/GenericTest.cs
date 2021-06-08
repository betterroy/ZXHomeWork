using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.SqlHelper
{
    //泛型
    public class GenericClass<T, W, X>
    {
        public void Show(T t, W w, X x)
        {
            Console.WriteLine($"t.type={t.GetType().Name},w.type={w.GetType().Name},x.type={x.GetType().Name}");
        }
    }

    public class GenericMethod
    {
        public void Show<T, W, X>(T t, W w, X x)
        {
            Console.WriteLine($"t.type={t.GetType().Name},w.type={ w.GetType().Name},x.type={x.GetType().Name}");
        }
    }

    public class GenericDouble<T>
    {
        public void Show<W, X>(T t, W w, X x)
        {
            Console.WriteLine($"t.type={t.GetType().Name},w.type={w.GetType().Name},x.type={x.GetType().Name}");
        }
    }
}
