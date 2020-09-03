using HomeWork.Class.AttributeExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Class
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Account { get; set; }
        [Password(3)]
        public string Password { get; set; }
        [Password(4)]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int State { get; set; }
        public int UserType { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int LastModifierId { get; set; }
        public DateTime LastModifyTime { get; set; }

    }
}
