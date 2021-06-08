using System;
using System.Collections.Generic;
using System.Text;

namespace Course_6_HomeWork.ThreadforTask.HomeWork.Class
{
    class DramaScript
    {
        /// <summary>
        /// 剧本名称
        /// </summary>
        public string DramaName { get; set; }
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsStart { get; set; }
        /// <summary>
        /// 人物
        /// </summary>
        public List<Person> people { get; set; }
    }
}
