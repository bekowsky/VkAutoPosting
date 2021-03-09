using AngleSharp;
using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikeBotService
{
    
    public class SubscriberChromeBot

    {
        
        IWebDriver driver;
        public SubscriberChromeBot()
        {

  
            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"C:\ChromeDriver\chromedriver\1.1.0_0.crx");
          
             driver = new ChromeDriver(@"C:\ChromeDriver\chromedriver", options);
        }
        public IDocument StrToDoc (string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = context.OpenAsync(__resp => __resp.Header("Content-Type", "text/html; charset=utf-8").Content(html));
            return document.Result;
        }

        public IDocument GetPage(string url)
        {

            driver.Navigate().GoToUrl(url);
            var context = BrowsingContext.New(Configuration.Default);
            var document = context.OpenAsync(__resp => __resp.Header("Content-Type", "text/html; charset=utf-8").Content(driver.PageSource));
            return document.Result;

        }

        public void Test (string url)
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(5000);
            if (driver.PageSource.Contains("g-recaptcha"))
            {
                var solver = new CaptchaSolver();
                var captcha = solver.SolveReCaptcha(StrToDoc(driver.PageSource), driver.Url);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("document.getElementById(\"g-recaptcha-response\").style.display = \"\";");                                                                                                 //нужно ввести решение капчи в специальное поле(оно есть у всех рекапч)
                driver.FindElement(By.CssSelector("textarea#g-recaptcha-response")).SendKeys(captcha); //ввод решения
                Thread.Sleep(500);

                js.ExecuteScript($"setCookie();");
            }
        }


        public void LogIn()
        {
            driver.Navigate().GoToUrl("https://olike.ru");
            driver.FindElement(By.CssSelector("#intro #soc-login-table td img")).Click();
            Thread.Sleep(5000);

            String first_handle = driver.CurrentWindowHandle;
            
            //JavascriptExecutor js = (JavascriptExecutor)driver;
            //js.executeScript("arguments[0].click();", tableRow);
            //new WebDriverWait(driver, 5).until(ExpectedConditions.numberOfWindowsToBe(2));
            var allHandles = driver.WindowHandles;
            foreach (var item in allHandles)
            {
                if (item!=first_handle)
                {                    
                    driver.SwitchTo().Window(item);
                }
            }
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys("+79992035141");
            driver.FindElement(By.CssSelector("input[name=pass]")).SendKeys("1999dbyrcIlove^^");
            driver.FindElement(By.CssSelector("#install_allow")).Click();
            Thread.Sleep(1000);
            driver.SwitchTo().Window(first_handle);


            while (true)
            {
                try
                {
                    var vkwindow = "";
                    driver.FindElement(By.CssSelector(".universalButton_dash")).Click();
                    Thread.Sleep(1000);
                    allHandles = driver.WindowHandles;
                    foreach (var item in allHandles)
                    {
                        if (item != first_handle)
                        {
                            driver.SwitchTo().Window(item);
                            vkwindow = item;
                        }
                    }
                    try
                    {
                        driver.FindElement(By.CssSelector("#join_button")).Click();
                    }
                    catch
                    {
                        driver.FindElement(By.CssSelector("#public_subscribe")).Click();

                    }
                    Thread.Sleep(2000);

                    //if (driver.PageSource.Contains("g-recaptcha"))
                    //{
                    //    var solver = new CaptchaSolver();
                    //    var captcha = solver.SolveReCaptcha(StrToDoc(driver.PageSource),driver.Url);
                    //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    //   js.ExecuteScript("document.getElementById(\"g-recaptcha-response\").style.display = \"\";");                                                                                                 //нужно ввести решение капчи в специальное поле(оно есть у всех рекапч)
                    //    driver.FindElement(By.CssSelector("textarea#g-recaptcha-response")).SendKeys(captcha); //ввод решения
                    //    Thread.Sleep(500);

                    //    js.ExecuteScript($"setCookie();");
                    //}

                    driver.SwitchTo().Window(first_handle);

                    driver.FindElement(By.CssSelector("#doneTaskButton")).Click();

                    

                    Thread.Sleep(1000);
                } catch (Exception ex)
                {
                    driver.Close();
                    driver.SwitchTo().Window(first_handle);
                }
            }
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
