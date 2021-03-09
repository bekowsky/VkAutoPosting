using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsService1;
using LikeBotService;
using System.Net.Http;
using LikeBotService.AddFun;

namespace VkAutoPosting
{
    class Program
    {
        static void Main(string[] args)
        {


            var master = new MasterService();

            master.PostPicktureVk();
           // var chrome = new SubscriberChromeBot();
            



            //var vk = new BossLikeBot("+79992035141", "1999dbyrcIlove^^");
            //vk.Initialising();
            //vk.Subscrubing();

        }
    }
}
