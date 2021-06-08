using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPOCOModel.Model
{
    public class B_RowInfo
    {
        /// <summary>
        /// 列数据类型
        /// </summary>
        public string RowType { get; set; }
        /// <summary>
        /// 列数据类型长度
        /// </summary>
        public string RowByteNum { get; set; }
       /// <summary>
       /// 列名
       /// </summary>
        public string RowName { get; set; }
        /// <summary>
        /// 是否自增主键
        /// </summary>
        public int Identification { get; set; }
    }
}
