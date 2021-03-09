using AngleSharp.Dom;
using LikeBotService.AddFun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikeBotService
{
   public class CaptchaSolver
    {
       

        string myKey = "2c729ac959c750b55384a8f8483a5e11";
        public string SolveReCaptcha(IDocument document,string url)
        {
            
            var siteKey = document?.QuerySelector(".g-recaptcha")?.GetAttribute("data-sitekey") ?? "";
            if (siteKey == "")
                siteKey = document?.QuerySelector("iframe[src*=www.google.com/recaptcha/api2/anchor]")?.GetAttribute("src").findStringRegExGroup("&k=(?<i>.*?)&", "i");
            var getIdUrl = $"http://rucaptcha.com/in.php?key={myKey}&method=userrecaptcha&googlekey={siteKey}&pageurl={url}";

            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(getIdUrl).Result;
            var id = response.Split('|')[1];
            var captcha = "CAPCHA_NOT_READY";
            var getCaptchaUrl = $"http://rucaptcha.com/res.php?key={myKey}&action=get&id={id}";
            for (int i = 1; i < 5&&captcha=="CAPCHA_NOT_READY"; i++)
            {
                Thread.Sleep(20000);
               
                captcha = client.GetStringAsync(getCaptchaUrl).Result;

                
            }

            return captcha.Split('|')[1];
        }

    }
}
