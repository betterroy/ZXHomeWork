using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class RemarkAttribute : Attribute
    {
        public string remark { get; private set; }
        public RemarkAttribute(string remarkValue)
        {
            remark = remarkValue;
        }
    }
}
