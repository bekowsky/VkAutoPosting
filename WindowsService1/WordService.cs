using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WindowsService1
{
   public class WordService
    {
        Random random = new Random();
        WordsContext db = new WordsContext();
        ILogger logger;
        public WordService(ILogger _logger)
        {
            logger = _logger;
        }
        public void FillDB()
        {

            EventLog myLog = new EventLog();
            myLog.Source = "VkAutoPosting";
            HtmlDocument html = new HtmlDocument();
            html.Load(("C:\\words.txt"), UTF8Encoding.UTF8);

            HtmlNode Node = html.DocumentNode;
            var NodeList = Node.SelectSingleNode("/div[1]");
            foreach (HtmlNode node in NodeList.ChildNodes.Where(x => x.Name == "tr"))
            {
                Word word = new Word { en = node.ChildNodes[1].InnerText, ru = node.ChildNodes[2].InnerText };
                db.Words.Add(word);

            }
            db.SaveChanges();
            logger.LogSuccessMessage($"Успешно добавили слова в БД");

        }

        public string[] TakeWordTranslate()
        {
            EventLog myLog = new EventLog();
            myLog.Source = "VkAutoPosting";
            int index = random.Next(0, 4999);
        
            try
            {
               
                Word word1 = db.Words.First();
                Word word = db.Words.Single(x => x.id == index);               
                string[] arr = { word.ru, word.en };
                return arr;
            } catch(Exception ex)
            {
                logger.LogErrorMessage($"Ошибка WordService.TakeWordTranslate {ex.Message}");
            }

            return null;
           
           


        }
    }
}
