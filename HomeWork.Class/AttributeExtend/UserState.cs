using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class.AttributeExtend
{
    public enum UserState
    {
        [Remark("正常")]
        Normal = 0,
        [Remark("冻洁")]
        Frozen = 1,
        [Remark("已删除")]
        Deleted = 2,
    }
}
