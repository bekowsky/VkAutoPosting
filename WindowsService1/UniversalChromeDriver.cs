using AngleSharp;
using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
   public class UniversalChromeDriver
    {
        IWebDriver driver;
        public UniversalChromeDriver()
        {


            driver = new ChromeDriver(@"C:\ChromeDriver\chromedriver_win32");
        }

        public IDocument GetPage(string url)
        {

            driver.Navigate().GoToUrl(url);
            var context = BrowsingContext.New(Configuration.Default);
            var document = context.OpenAsync(__resp => __resp.Header("Content-Type", "text/html; charset=utf-8").Content(driver.PageSource));
            return document.Result;

        }

        public string GetPageStr(string url)
        {

            driver.Navigate().GoToUrl(url);
            return driver.PageSource ;
            

        }
        public void EndSession()
        {
            driver.Quit();
        }

    }
}
