using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HomeWork.Class.AttributeExtend
{
    public static class ValidateAttribute
    {
        public static bool Validate<T>(T t)
        {
            Type type = t.GetType();
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.IsDefined(typeof(AbstractValidateAttribute), true))
                {
                    object v = property.GetValue(t);
                    AbstractValidateAttribute customAttribute = property.GetCustomAttribute<AbstractValidateAttribute>();
                    if(!customAttribute.Validate(v))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static string GetRemark(this Enum @enum)
        {
            Type type = @enum.GetType();
            FieldInfo fileInfo = type.GetField(@enum.ToString());
            if (fileInfo != null)
            {
                if (fileInfo.IsDefined(typeof(RemarkAttribute), true))
                {
                    RemarkAttribute remarkAttribute = (RemarkAttribute)fileInfo.GetCustomAttribute(typeof(RemarkAttribute), true);
                    Console.WriteLine($"当前:{@enum} 的备注为：{remarkAttribute.remark}");
                    return remarkAttribute.remark;
                }
            }
            return @enum.ToString();
        }
        public static string GetRemark1(Enum @enum)
        {
            Type type = @enum.GetType();
            FieldInfo fileInfo = type.GetField(@enum.ToString());
            if (fileInfo != null)
            {
                if (fileInfo.IsDefined(typeof(RemarkAttribute), true))
                {
                    RemarkAttribute remarkAttribute = (RemarkAttribute)fileInfo.GetCustomAttribute(typeof(RemarkAttribute), true);
                    Console.WriteLine($"当前:{@enum} 的备注为：{remarkAttribute.remark}");
                    return remarkAttribute.remark;
                }
            }
            return @enum.ToString();
        }
    }
}
