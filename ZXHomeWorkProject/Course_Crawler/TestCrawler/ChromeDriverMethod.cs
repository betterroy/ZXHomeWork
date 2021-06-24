using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Crawler.TestCrawler
{
    public class ChromeDriverMethod
    {
        public void Show()
        {
            string baitai_id = "006";
            string word = "python";
            string savepath = "666.html";

            WebBrowser browser = new WebBrowser();
            browser.Navigate("https://wenku.baidu.com/view/06d87e755e0e7cd184254b35eefdc8d377ee147a.html?from=search");
            var htmldocument = browser.Document.DomDocument;



            var getsource = SearchYahoo(savepath, word, baitai_id);
        }
        public static string SearchYahoo(string SavePath, string word, string baitai_id)
        {
            ChromeOptions options = new ChromeOptions();
            //Set the http proxy value, host and port.
            //Set the proxy to the Chrome options
            var proxy = new Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.IsAutoDetect = false;
            proxy.HttpProxy = "localhost:8080";
            proxy.SslProxy = "localhost:8080";
            options.Proxy = proxy;

            options.AddArgument("ignore-certificate-errors");
            //options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            string ua = null;
            string url = @"https://search.yahoo.co.jp/";
            var spua = "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A403 Safari/8536.25";
            var pcua = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";


            switch (baitai_id)
            {
                case "002":
                    ua = pcua;
                    url = @"https://www.yahoo.co.jp/";
                    break;
                case "004":
                    ua = spua;
                    break;
                case "006":
                    ua = spua;
                    break;
                default:
                    break;
            }

            options.AddArgument(string.Format("--user-agent={0}", ua));
            List<String> tagNmaeList = new List<string>(); ;
            var driver = new ChromeDriver(options);
            string Source = null;
            try
            {


                //002的换用另外程序
                driver.Navigate().GoToUrl(url);
                var inputNode = driver.FindElementByXPath(@"//input[@name='p']");
                Thread.Sleep(3000);
                inputNode.SendKeys(word);
                var search = driver.FindElementByXPath(@"//input[@type='submit']");
                Thread.Sleep(2000);
                search.Click();
                Thread.Sleep(5000);
                Source = driver.PageSource;


            }
            catch (Exception ex)
            {


            }
            finally
            {
                // driver.Close();

                driver.Quit();


            }
            System.IO.File.WriteAllText(SavePath, Source, System.Text.Encoding.UTF8);
            return Source;


        }
    }
}
