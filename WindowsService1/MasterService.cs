using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService1
{
   public class MasterService
    {
       public void TestWord(string word)
        {
         

            ImageService imgClient = new ImageService();
            WordService wordClient = new WordService();
            VkService serviceVkClient = new VkService();
            PictureService servicePicture = new PictureService();

      

            string[] words = new string[2] {word,word };

            string url = imgClient.TakeLink(words[0], 1);
        

            string path0 = servicePicture.CombineTextPicture(url, words[0], words[1]);
         

            string path = @"C:\ImagesForVk\" + path0 + ".jpg";
            serviceVkClient.Posting("3c61a25deb2bad6b45ccb0cda522f5327003bd1e672775e349b96d45c6213c85db0d49dd23fd6eae9498c", path, "199275400");

        }
        public void PostPicktureVk()
        {
           
            if (!EventLog.SourceExists("VkAutoPosting"))
            {
                //An event log source should not be created and immediately used.
                //There is a latency time to enable the source, it should be created
                //prior to executing the application that uses the source.
                //Execute this sample a second time to use the new source.
                EventLog.CreateEventSource("VkAutoPosting", "MyNewLog");
            
                // The source is created.  Exit the application to allow it to be registered.
                return;
            }

            // Create an EventLog instance and assign its source.
            EventLog myLog = new EventLog();
            myLog.Source = "VkAutoPosting";


            // Write an informational entry to the event log.
            

            ImageService imgClient = new ImageService();
            WordService wordClient = new WordService();
            VkService serviceVkClient = new VkService();
            PictureService servicePicture = new PictureService();
           
            myLog.WriteEntry("Успех! Программа запустилась");

          //  wordClient.FillDB();
            string[] words = wordClient.TakeWordTranslate();

            myLog.WriteEntry($"Успех! Взяли слово с БД {words[0]} {words[1]}") ;
          

            string url = imgClient.TakeLink(words[0], 1);
            myLog.WriteEntry($"Успех! Получили ссылку {url}");

            string path0 = servicePicture.CombineTextPicture(url, words[0], words[1]);
            myLog.WriteEntry("Успех! Наложили текст на картинку");

            string path = @"C:\ImagesForVk\" + path0 + ".jpg";
            

            serviceVkClient.Posting("3c61a25deb2bad6b45ccb0cda522f5327003bd1e672775e349b96d45c6213c85db0d49dd23fd6eae9498c", path, "199275400");

            myLog.WriteEntry("Успех! Запостили картинку в вк");

        }

        public void Start()
        {
            while (true)
            {
                PostPicktureVk();
                Thread.Sleep(1800000);
            }
        }
    }
}
