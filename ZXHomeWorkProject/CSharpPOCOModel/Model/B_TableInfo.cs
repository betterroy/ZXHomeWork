using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPOCOModel.Model
{
    public class B_TableInfo
    {
        public string TableName { get; set; }
        public List<B_RowInfo> Rows { get; set; }
    }
}
