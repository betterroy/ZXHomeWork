using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.Data.Model
{
    public class FileContent
    {

        /// <summary>
        /// 文件的唯一ID
        /// </summary>
        public string fileGuid { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string filePath { get; set; }

        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string fileFullPath { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        public string fileText { get; set; }

        /// <summary>
        /// 文件上传时间
        /// </summary>
        public DateTime fileUploadTime { get; set; }


    }
}
