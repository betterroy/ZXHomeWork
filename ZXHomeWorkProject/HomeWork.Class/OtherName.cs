using HomeWork.Class.AttributeExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class
{
    public class OtherName : BaseModel
    {
        public DateTime Datetime { get; set; }
        [Remark("Names")]
        public string Name { get; set; }
    }
}
