using Course_Crawler.TestCrawler;
using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Course_Crawler.TestCrawler
{
    class Teacher
    {

        public string LoginSimulation1()
        {
            string a = "\"";
            string url = "http://10.64.0.248/data/ashx/system/LoginServer.ashx?submitData=OI0vd7cmTLbuahRssbOtsiHk5D//5NTFF5Rpot1DfGfBfAX6unJZkxRXlXNE%2B8aCCuKcWySdUf%2BU58GI70unfYh/oS8Y2VFmWfmgaag3VcNUJXjYHhi6fm%2BbE6lMkWrRHJV52Z/KQe1w1SrH5DQRoAJOZAoVmMnlit9dQ/1AYEg=";
            //string postData = "{\"input1\":\"MvxmwEWfUF26IvKNa1dUiZn1xmSBhNW0wJyoaUlDPXoh+Mb+z2eZK3r3c9Jd0aT0/Wzz3ht7LMeTllu8ISY9nfQIuKB0C19Y9/IfKYSktpZZOVaKx/XP3i/"+"mGxXC3K5m2la91ViRh3BO36xT4E98dbqVHPtynjuNafuVIBF5a2M=\",\"input2\":\"xxxx\":false}";
            string postData = "{\"submitData\": \"OI0vd7cmTLbuahRssbOtsiHk5D//5NTFF5Rpot1DfGfBfAX6unJZkxRXlXNE%2B8aCCuKcWySdUf%2BU58GI70unfYh/oS8Y2VFmWfmgaag3VcNUJXjYHhi6fm%2BbE6lMkWrRHJV52Z/KQe1w1SrH5DQRoAJOZAoVmMnlit9dQ/1AYEg=\"}";

            //1.获取登录Cookie
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";// POST OR GET， 如果是GET, 则没有第二步传参，直接第三步，获取服务端返回的数据
            req.AllowAutoRedirect = false;//服务端重定向。一般设置false
            req.ContentType = "application/x-www-form-urlencoded";//数据一般设置这个值，除非是文件上传

            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            req.ContentLength = postBytes.Length;
            Stream postDataStream = req.GetRequestStream();
            postDataStream.Write(postBytes, 0, postBytes.Length);
            postDataStream.Close();

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            string cookies = resp.Headers.Get("Set-Cookie");//获取登录后的cookie值。

            //2.登录想爬取页面的构造，主要多一个Cookie的构造
            string contentUrl = "http://10.64.0.248/buess/quality/Report/Frm_QMReport.aspx?pagecode=11283&sd=-31&sd1=0&yb=ccsl";
            HttpWebRequest reqContent = (HttpWebRequest)WebRequest.Create(contentUrl);
            reqContent.Method = "GET";
            reqContent.AllowAutoRedirect = true;//服务端重定向。一般设置false
            reqContent.ContentType = "application/x-www-form-urlencoded";//数据一般设置这个值，除非是文件上传

            reqContent.CookieContainer = new CookieContainer();
            reqContent.CookieContainer.SetCookies(reqContent.RequestUri, cookies);//将登录的cookie值赋予此次的请求。

            HttpWebResponse respContent = (HttpWebResponse)reqContent.GetResponse();
            string html = new StreamReader(respContent.GetResponseStream()).ReadToEnd();

            //3. 分析读取该页面的数据，可以使用HtmlAgilityPack第三方类，这里比较简单，自己写个获取方法就行
            //string age = GetVal(html, "<span title='入园时间：2010-6-28'>", "</span>");
            string age = "1";
            return age;
        }
    }
}
