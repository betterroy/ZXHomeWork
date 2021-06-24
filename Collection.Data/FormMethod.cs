using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collection.Data
{
    public class FormMethod
    {
        public string GetDialogPath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = @"请选择文件路径"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = dialog.SelectedPath;
                return savePath;
            }
            return "";
        }
    }
}
