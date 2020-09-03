using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : AbstractValidateAttribute
    {
        public int length { get; private set; }

        public PasswordAttribute(int len)
        {
            length = len;
        }

        public override bool Validate(object value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return false;
            return !string.IsNullOrEmpty(value.ToString()) && value.ToString().Length > length;
        }
    }
}
