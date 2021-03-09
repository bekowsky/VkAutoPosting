using AngleSharp;
using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikeBotService
{
  public  class VkSubscriberBot
    {
        IWebDriver driver;
        string login, password;
        public VkSubscriberBot(string _login, string _password)
        {

            login = _login;
            password = _password;
            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"C:\ChromeDriver\chromedriver\1.1.0_0.crx");
            driver = new ChromeDriver(@"C:\ChromeDriver\chromedriver", options);
        }

        public void Subscribing(string url)
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);
            try
            {
                driver.FindElement(By.CssSelector("#join_button")).Click();
            }
            catch
            {
                try
                {
                    driver.FindElement(By.CssSelector("#public_subscribe")).Click();
                } 
                catch
                {
                    try { driver.FindElement(By.CssSelector(".Profile__addFriendButton")).Click(); }
                    catch
                    {
                        if (driver.PageSource.Contains("g-recaptcha"))
                        {
                            var a = 1;
                        }
                    }
                }

            }
            Thread.Sleep(2000);
            if (driver.PageSource.Contains("g-recaptcha"))
            {
                var solver = new CaptchaSolver();
                var captcha = solver.SolveReCaptcha(StrToDoc(driver.PageSource), driver.Url);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("document.getElementById(\"g-recaptcha-response\").style.display = \"\";");                                                                                                 //нужно ввести решение капчи в специальное поле(оно есть у всех рекапч)
                driver.FindElement(By.CssSelector("textarea#g-recaptcha-response")).SendKeys(captcha); //ввод решения
                Thread.Sleep(500);

                js.ExecuteScript($"___grecaptcha_cfg.clients[0].Y.Y.callback({captcha});");
            }
        }

        private IDocument StrToDoc(string pageSource)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = context.OpenAsync(__resp => __resp.Header("Content-Type", "text/html; charset=utf-8").Content(pageSource));
            return document.Result;
        }

        public void LogIn()
        {
            driver.Navigate().GoToUrl("https://vk.com");
           
            driver.FindElement(By.CssSelector("#index_email")).SendKeys(login);
            driver.FindElement(By.CssSelector("#index_pass")).SendKeys(password);
            driver.FindElement(By.CssSelector("#index_login_button")).Click();
                                 
        }
    }
}
