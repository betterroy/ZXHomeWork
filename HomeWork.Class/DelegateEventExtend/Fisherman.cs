using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class.DelegateEventExtend
{
    public class Fisherman
    {
        public int ID{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Leavel { get; set; }
        public void CatchFish()
        {
            Console.WriteLine("我是渔夫，我在钓鱼");
        }
    }
}
