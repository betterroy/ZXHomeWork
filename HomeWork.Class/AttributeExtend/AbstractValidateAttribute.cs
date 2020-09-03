using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class.AttributeExtend
{
    public abstract class AbstractValidateAttribute : Attribute
    {
        public abstract bool Validate(object value);
    }
}
