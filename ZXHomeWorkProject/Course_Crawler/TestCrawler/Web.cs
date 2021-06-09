using System;
//添加selenium的引用
//using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Course_Crawler.TestCrawler
{
    class Web
    {
        static ChromeDriver driver { get; set; }
        static ICookieJar cookie { get; set; }

        public void GetHtml()
        {
            var url = @"https://s8hwxkltn6.jiandaoyun.com/dash/5f48d400a25baa0006034c29";
            //GetHtml(url);
            iWebChrome();
        }
        public static void iWebChrome()
        {
            new ChromeDriver("/path/to/chromedriver");
            IWebDriver selenium = new ChromeDriver("D:\\Develop\\net\\allTest\\ZXHomeWorkProject\\ZXHomeWorkProject\\Course_Crawler\\bin\\Debug\\net5.0");

            selenium.Navigate().GoToUrl("http://www.baidu.com");
            selenium.Navigate().GoToUrl("http://www.hao123.com");
            selenium.Navigate().Back();
        }

        private static void GetHtml(string url)
        {
            //PhantomJSDriverService driverService = PhantomJSDriverService.CreateDefaultService();
            //driverService.IgnoreSslErrors = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--nogpu");
            List<String> tagNmaeList = new List<string>();
            //using (driver = new ChromeDriver(options))
            //{
            //    try
            //    {
            //        driver.Manage().Window.Maximize();
            //        driver.Navigate().GoToUrl(url);
            //        Thread.Sleep(5000);
            //        Console.WriteLine(driver.PageSource); //输出网页源码
            //    }
            //    catch (NoSuchElementException)
            //    {
            //        Console.WriteLine("找不到该元素"); ;
            //    }
            //}
            using (driver = new ChromeDriver(options))
            {
                try
                {
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(5000);
                    // 点击按钮
                    driver.ExecuteScript("return $('.count-sel').click()");
                    Thread.Sleep(500);
                    // 选择一百条每页
                    driver.ExecuteScript("return $(\".x-dropdown a[option='100']\").click()");
                    Thread.Sleep(1000);  // 给点时间加载网页
                                         //Console.WriteLine(driver.PageSource); //输出网页源码
                                         //GetCookie();
                    GetData(driver.PageSource);  // 调用解析数据方法，得到数据datatable 
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("找不到该元素"); ;
                }
            }

        }

        //分析HTML 数据
        private static void GetData(string ddd)
        {

            DataRow dr;
            DataTable dt = new DataTable();  //创建datatable,存储数据
            dt.Columns.Add(new System.Data.DataColumn("序号", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("要求到货时间", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("合同号", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("地址", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("货物名称", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("规格型号", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("公司型号", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("单位", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("数量", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("理论重量", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("金额", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("备注", typeof(System.String)));
            dt.Columns.Add(new System.Data.DataColumn("合同号2", typeof(System.String)));

            string oo = string.Empty;
            string kk = string.Empty;
            string ll = string.Empty;
            string hh = string.Empty;
            string fileConent = string.Empty;
            string tableContent = string.Empty;
            string rowContent = string.Empty;
            string columnConent = string.Empty;

            string rowPatterm = @"<tr[^>]*>[\s\S]*?<\/tr>";  // 正则取tr行
            string columnPattern = @"<td[^>]*>[\s\S]*?<\/td>"; // 正则取每行的单元格

            MatchCollection rowCollection = Regex.Matches(ddd, rowPatterm, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture); //对tr进行筛选
            for (int i = 1; i < rowCollection.Count; i++)
            {
                rowContent = rowCollection[i].Value;
                MatchCollection columnCollection = Regex.Matches(rowContent, columnPattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture); //对td进行筛选
                if (i == 1) continue; // 第一行表头来的，直接不要，你也可以用来生成datable的列

                #region 数据筛选
                dr = dt.NewRow(); // 创建新行
                for (int j = 0; j < columnCollection.Count - 1; j++)
                {
                    columnConent = columnCollection[j].Value;
                    int iBodyStart = columnConent.IndexOf(">", 0);
                    int iTableEnd = columnConent.IndexOf("</td>", iBodyStart);
                    string strWeb = (columnConent.Substring(iBodyStart + 1, iTableEnd - iBodyStart - 1)).Replace("<span>", "").Replace("</span>", ""); //获取最终数据

                    if (columnCollection.Count == 14)
                    {
                        if (j == 0) dr[0] = oo = strWeb;
                        if (j == 1) dr[1] = kk = strWeb;
                        if (j == 2) dr[2] = ll = strWeb;
                        if (j == 3) dr[3] = hh = strWeb;
                        if (j > 3) dr[j] = strWeb;
                    }
                    else
                    {
                        if (j == 0) dr[j] = oo;
                        if (j == 1) dr[j] = kk;
                        if (j == 2) dr[j] = ll;
                        if (j == 3) dr[j] = hh;
                        dr[j + 4] = strWeb;
                    }

                }
                dt.Rows.Add(dr);
                #endregion
            }
        }
        private static void Login(ChromeDriver driver)
        {
            // driver.FindElement(By.Id("btn_Login")).GetAttribute("value");
            //2.执行 js 获取 value 的值
            //driver.ExecuteScript("return document.getElementsById('txt_AccountId')[0].value;");
            driver.ExecuteScript("return $('#帐号输入框ID').val('账号')"); //账号密码
            driver.ExecuteScript("return $('#密码输入框ID').val('密码')");
            // 3.执行jQuery 获取 value 的值
            var account = driver.ExecuteScript("return $('#帐号输入框ID').val()");
            var pass = driver.ExecuteScript("return $('#密码输入框ID').val()");

            driver.FindElement(By.Id("登录按钮ID")).Click(); //点击登录
            Thread.Sleep(1000);  // 给点时间加载网页
        }
        private static void GetCookie()
        {
            cookie = driver.Manage().Cookies;  //主要方法

            //显示初始Cookie的内容
            Console.WriteLine("--------------------");
            Console.WriteLine($"当前Cookie集合的数量：\t{cookie.AllCookies.Count}");
            for (int i = 0; i < cookie.AllCookies.Count; i++)
            {
                Console.WriteLine($"Cookie的名称:{cookie.AllCookies[i].Name}");
                Console.WriteLine($"Cookie的值:{cookie.AllCookies[i].Value}");
                Console.WriteLine($"Cookie的所在域:{cookie.AllCookies[i].Domain}");
                Console.WriteLine($"Cookie的路径:{cookie.AllCookies[i].Path}");
                Console.WriteLine($"Cookie的过期时间:{cookie.AllCookies[i].Expiry}");
                Console.WriteLine("--------------------");
            }
        }
    }

}
