using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HomeWork.Class.DelegateExtend
{
    public class CustomExtend
    {
        public void Show()
        {
            InvokeDelegate invokeDelegate = new InvokeDelegate();
            Type type = typeof(InvokeDelegate);
            Type type1 = invokeDelegate.GetType();
            MethodInfo method = type.GetMethod("Do");
            CustomAction customAction = new CustomAction(() =>
            {
                method.Invoke(invokeDelegate, null);
            });
            if (method.IsDefined(typeof(AbstractAttribute), true))
            {
                IEnumerable<AbstractAttribute> items = method.GetCustomAttributes<AbstractAttribute>();
                foreach (var item in items.ToArray().Reverse())
                {
                    customAction = item.Do(customAction);
                }
            }
            customAction.Invoke();
        }
    }
    public class InvokeDelegate
    {
        [BeforeDelegate]
        [Before1Delegate]
        public void Do()
        {
            Console.WriteLine("这是核心业务逻辑。");
        }
    }
    public delegate void CustomAction();

    public abstract class AbstractAttribute : Attribute
    {
        public abstract CustomAction Do(CustomAction customAction);
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeDelegateAttribute : AbstractAttribute
    {
        public override CustomAction Do(CustomAction customAction)
        {
            CustomAction customAction1 = new CustomAction(() =>
            {
                Console.WriteLine("执行前的方法");
                customAction.Invoke();
            });
            return customAction1;
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class Before1DelegateAttribute : AbstractAttribute
    {
        public override CustomAction Do(CustomAction customAction)
        {
            CustomAction customAction1 = new CustomAction(() =>
            {
                Console.WriteLine("另一个执行前的方法。");
                customAction.Invoke();
            });
            return customAction1;
        }
    }
}
