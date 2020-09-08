using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class.DelegateEventExtend
{
    public class HookFishStandard
    {
        public class HookFishEvent : EventArgs
        {
            public int beforeWeight { get; set; }
            public int afterWeight { get; set; }
        }
        /// <summary>
        /// 事件的发布者，发布是按并且在满足条件的情况下，触发事件
        /// </summary>
        public class PersonHooked
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int _fishWeight = 0;
            public int FishWeight
            {
                get { return _fishWeight; }
                set
                {
                    if (value > 0)
                    {
                        HookIncrease.Invoke(this, new HookFishEvent() { beforeWeight = _fishWeight, afterWeight = _fishWeight+value });
                    }
                    _fishWeight += value;
                }
            }
            public event EventHandler HookIncrease;
        }
        public static void Show()
        {
            PersonHooked personHooked = new PersonHooked()
            {
                ID = 1,
                Name = "roy"
            };
            personHooked.HookIncrease+= new man().sayWord;
            woman woman = new woman();
            personHooked.HookIncrease += woman.sayWord;
            personHooked.FishWeight = 10;
            personHooked.HookIncrease -= woman.sayWord;
            personHooked.FishWeight = 20;
            personHooked.FishWeight = 5;
        }
        public class man
        {
            public void sayWord(object sender, EventArgs e)
            {
                PersonHooked personHooked = (PersonHooked)sender;
                HookFishEvent hookFishEvent = (HookFishEvent)e;
                Console.WriteLine("我是男人");
                Console.WriteLine($"{personHooked.Name}之前的重量是{hookFishEvent.beforeWeight}");
                Console.WriteLine($"{personHooked.Name}现在的重量是{hookFishEvent.afterWeight}");
                Console.WriteLine("******************************haha。。******************************");
            }
        }
        public class woman
        {
            public void sayWord(object sender, EventArgs e)
            {
                PersonHooked personHooked = (PersonHooked)sender;
                HookFishEvent hookFishEvent = (HookFishEvent)e;
                Console.WriteLine("我是女人");
                Console.WriteLine($"{personHooked.Name}之前的重量是{hookFishEvent.beforeWeight}");
                Console.WriteLine($"{personHooked.Name}现在的重量是{hookFishEvent.afterWeight}");
                Console.WriteLine("******************************xixi。。******************************");
            }
        }
    }
}
