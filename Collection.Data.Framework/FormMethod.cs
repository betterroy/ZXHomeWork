using Collection.Data.Lucene.Handle;
using Collection.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collection.Data
{
    public class FormMethod
    {
        LuceneIndexer luceneIndexer = new LuceneIndexer();
        /// <summary>
        /// 显示弹出框，选择路径
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 写入文件。
        /// </summary>
        /// <param name="fileInfo"></param>
        public void WriteLucene(FileInfo fileInfo)
        {
            string filePath = fileInfo.FullName;

            FileContent fileContent = new FileContent();
            fileContent.filename = fileInfo.Name;
            fileContent.fileFullPath = filePath;
            fileContent.fileGuid = "";
            fileContent.filePath = "";
            fileContent.fileUploadTime = DateTime.Now;


            if (filePath.LastIndexOf('.') > 0)
            {
                string fileType = filePath.ToString().Substring(filePath.LastIndexOf('.') + 1);
                if (fileType == "doc" || fileType == "docx") { }
                    //fileContent.fileText = getWordContent(fileInfo);
                else if (fileType == "pdf")
                    fileContent.fileText = getPdfContent(filePath);
                else if (fileType == "xls" || fileType == "xlsx") { }
                    //fileContent.fileText = getXlsContent(filePath);
            }
            //luceneIndexer.Add(fileContent);
        }


        /// <summary>
        /// 获取PDF中的内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string getPdfContent(string path)
        {
            try
            {
                string text = string.Empty;

                //string pdffilename = path;
                //PdfReader pdfReader = new PdfReader(pdffilename);
                //int numberOfPages = pdfReader.NumberOfPages;

                //for (int i = 1; i <= numberOfPages; ++i)
                //{
                //    iTextSharp.text.pdf.parser.ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                //    text += iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(pdfReader, i, strategy).Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""); ;
                //}
                //pdfReader.Close();

                return text;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// 获取word中的内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadFile(string sourcePath, string targetPath)
        {
            //WPS.ApplicationClass app = new WPS.ApplicationClass();
            //WPS.Document doc = null;
            //try
            //{
            //    doc = app.Documents.Open(sourcePath, true, true, false, null, null, false, "", null, 100, 0, true, true, 0, true);
            //    doc.ExportPdf(targetPath, "", "");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return false;
            //}
            //finally
            //{
            //    doc.Close();
            //}
            //return true;
            return default(string);
        }


    }
}
