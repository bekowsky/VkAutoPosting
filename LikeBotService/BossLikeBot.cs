using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikeBotService
{
  
  public  class BossLikeBot
    {
        
        HttpClient client;
        string apiKey;
        string login;
        string password;
        VkSubscriberBot subscriberBot;
        
        public BossLikeBot(string _login, string _password)
        {           
            login = _login;
            password = _password;
        }

        
        public void Initialising()
        {
            subscriberBot = new VkSubscriberBot(login,password);
            subscriberBot.LogIn();
            client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-Api-Key", "f940506514a1dcce2f36430db641f64187573c462ad3c8ad");
            var response = client.GetStringAsync("https://api-public.bosslike.ru/v1/bots/users/me/").Result;
        }

        public void Subscrubing()
        {
          
            bool flag = true;
            while (flag)
            {    
                var jsonList = client.GetStringAsync("https://api-public.bosslike.ru/v1/bots/tasks/?service_type=1&task_type=3").Result;
                var TaskList = JsonConvert.DeserializeObject<TaskListJsonModel>(jsonList);
                flag = TaskList.Data.Items.Count == 20 ? true : false;
                foreach (var item in TaskList.Data.Items)
                {
                    try
                    {
                        var jsonLink = client.GetStringAsync($"https://api-public.bosslike.ru/v1/bots/tasks/{item.Id}/do/").Result;
                    
                        var url = JsonConvert.DeserializeObject<TaskJsonModel>(jsonLink).Data.Url;
                       
                        subscriberBot.Subscribing(url);

                        var check = true;
                        for (int i = 1; i < 6&&check; i++)
                        {
                            try
                            {
                                var checkResponse = client.GetStringAsync($"https://api-public.bosslike.ru/v1/bots/tasks/{item.Id}/check/").Result;
                               
                                Console.WriteLine("Успешно выполнено задание");
                                break;

                            } catch (Exception ex)
                            {
                                Console.WriteLine($"Не удалось получить награду {i} попытка") ;
                                Thread.Sleep(1000);
                            }
                        }
                        
                        
                    }
                    catch (Exception ex)
                    {

                    }
                }


            }

        }




    }
}
