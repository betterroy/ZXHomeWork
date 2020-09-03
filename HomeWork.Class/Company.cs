using System;

namespace HomeWork.Class
{
    public class Company : BaseModel
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int LastModifierId { get; set; }
        public DateTime LastModifyTime { get; set; }

    }
}
