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
            ILogger logger;
            if (Environment.UserInteractive)
                logger = new ConsoleLogger();
            else logger = new WindowsLogger("");


            var master = new MasterService(logger);

            master.Cleaning();

            // var chrome = new SubscriberChromeBot();



            //var vk = new BossLikeBot("+79992035141", "1999dbyrcIlove21");
            //vk.Initialising();
            //vk.Subscrubing();

        }
    }
}
