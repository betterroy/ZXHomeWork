using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class.DelegateExtend
{
    public class NormalExtend
    {
        public delegate void First();                   //空方法委托
        public delegate void Second(string sayString);  //带参数委托
        public delegate int Third();                    //带返回值委托
        public delegate int Fourth(out string outstr, ref string refstr);//带参数带返回值委托
        public delegate void Fifth(string sayHi);       //委托

        public void Do()
        {
            First first = new First(FirstMethod);
            first.Invoke();
            Second second = new Second(SecondMethod);
            second.Invoke("你好");
            Third third = new Third(ThirdMethod);
            int tResult = third.Invoke();
            Fourth fourth = new Fourth(FourthMethod);
            string outstr = "";
            string refstr = "";
            fourth.Invoke(out outstr, ref refstr);
            //把委托当做参数传递
            Fifth fifth = new Fifth(FifthMethod);
            SayHi("小安", fifth);
        }
        public void FirstMethod()
        {
            Console.WriteLine("That is first delegate");
        }
        public void SecondMethod(string sayString)
        {
            Console.WriteLine($"That is second delegate:{sayString}");
        }
        public int ThirdMethod()
        {
            Console.WriteLine("That is third delegate");
            return default(int);
        }
        public int FourthMethod(out string outstr, ref string refstr)
        {
            outstr = "1";
            outstr += "outstr：增加后";
            refstr += "refstr：增加后";
            Console.WriteLine("That is fouth delegate");
            return default(int);
        }
        public void FifthMethod(string name)
        {
            Console.WriteLine($"That is fifth delegate:{name}");
        }
        public void SayHi(string name, Fifth fifth)
        {
            fifth.Invoke(name);
        }
        public void SayHiTaowa(string name, Fifth fifth)
        {
            fifth.Invoke(name);
        }
        public static string FuncDo(string a)
        {
            Console.WriteLine("输出：" + a);
            return a;
        }
        public static string FuncDo2(string a)
        {
            Console.WriteLine("输出：" + a);
            return a;
        }
    }
}
