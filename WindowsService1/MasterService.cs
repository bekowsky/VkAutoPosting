
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
namespace WindowsService1
{
   public class MasterService
    {
        ILogger logger;
        public MasterService(ILogger _logger)
        {
            logger = _logger;
        }
       public void TestWord(string word)
        {
         

            ImageService imgClient = new ImageService(logger);
            WordService wordClient = new WordService(logger);
            VkService serviceVkClient = new VkService(logger);
            PictureService servicePicture = new PictureService(logger);

      

            string[] words = new string[2] {word,word };

            string url = imgClient.TakeLink(words[0], 1);
        

            string path0 = servicePicture.CombineTextPicture(url, words[0], words[1]);
         

            string path = @"C:\ImagesForVk\" + path0 + ".jpg";
            serviceVkClient.Posting("3c61a25deb2bad6b45ccb0cda522f5327003bd1e672775e349b96d45c6213c85db0d49dd23fd6eae9498c", path, "199275400");

        }
        public void PostPicktureVk()
        {
                       
            ImageService imgClient = new ImageService(logger);
            WordService wordClient = new WordService(logger);
            VkService serviceVkClient = new VkService(logger);
            PictureService servicePicture = new PictureService(logger);

           

            //  wordClient.FillDB();
            string[] words = wordClient.TakeWordTranslate();

            logger.LogInfoMessage($"Взяли слово с БД {words[0]} {words[1]}") ;
          

            string url = imgClient.TakeLink(words[0], 1);
            logger.LogInfoMessage($"Получили ссылку {url}");

            string path0 = servicePicture.CombineTextPicture(url, words[0], words[1]);
            logger.LogInfoMessage("Наложили текст на картинку");

            string path = @"C:\ImagesForVk\" + path0 + ".jpg";
            

            serviceVkClient.Posting("3c61a25deb2bad6b45ccb0cda522f5327003bd1e672775e349b96d45c6213c85db0d49dd23fd6eae9498c", path, "199275400");

            logger.LogSuccessMessage("Успех! Запостили картинку в вк");

        }

        public void Start()
        {
            logger.LogInfoMessage("Запустили автопостинг");
            while (true)
            {
                try
                {
                    PostPicktureVk();
                    Thread.Sleep(1800000);
                } catch
                {
                    logger.LogErrorMessage("Ошибка! Не удалось запостить картинку, повторная попытка через 5 минут");
                    Thread.Sleep(300000);
                }
            }
        }

        public void Cleaning()
        {
            logger.LogInfoMessage("Запустили очистку директории");
            while (true)
            {
                
                if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles("C:\\ImagesForVk"))
                        {
                            File.Delete(file);
                        }
                      
                        logger.LogSuccessMessage("Успех! Директория очищена");
                    } catch (Exception ex)
                    {
                        logger.LogErrorMessage($"Ошибка! Ошибка очистки директории Информация:{ex.Message}");
                    }
                    Thread.Sleep(86000000);
                } else
                {
                    Thread.Sleep(10000);
                }
               

            }
        }
    }
}
