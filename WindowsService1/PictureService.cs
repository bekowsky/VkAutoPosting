using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WindowsService1
{
    class PictureService
    {
        public string CombineTextPicture(string url, string ru, string en)
        {
            System.Drawing.Text.PrivateFontCollection f = new System.Drawing.Text.PrivateFontCollection();
            string path = Directory.GetCurrentDirectory();
            EventLog myLog = new EventLog();
            myLog.Source = "VkAutoPosting";
            myLog.WriteEntry($"{path}");
            try
            {
                myLog.WriteEntry($"{Application.StartupPath}");
                f.AddFontFile(Path.Combine(Application.StartupPath, "Font","userfont2.ttf"));
               // f.AddFontFile("C:\\Font\\userfont.otf");
            } catch (Exception e)
            {
                myLog.WriteEntry($"ошибка со шрифтом {e.Message} {e.InnerException} {e.StackTrace}");
            }
            var request = WebRequest.Create(url);

            Image img;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                img = Bitmap.FromStream(stream);
            }

            Graphics g = Graphics.FromImage(img);
          

            int size = 150;
            SolidBrush Brush = new SolidBrush(Color.FromArgb(size, 0, 0, 0));
            g.FillRectangle(Brush, 0, 0, img.Width, img.Height);
            Font font = new Font(f.Families[0], size);

            Size len = TextRenderer.MeasureText(ru, font);
            
            while (len.Width > (img.Width - 200) || len.Height*6 > img.Height)
            {
                size -= 10;
                font = new Font(f.Families[0], size);
                len = TextRenderer.MeasureText(ru, font);
            }
            int maxHeight = len.Height;
            g.DrawString(ru, font, new SolidBrush(Color.White), (img.Width - len.Width)/2, img.Height / 2 + 25 );
            size = 150;
            font = new Font(f.Families[0], size);
            len = TextRenderer.MeasureText(en, font);
            while (len.Width > (img.Width - 200) || len.Height*6 >  img.Height)
            {
                size -= 10;
                font = new Font(f.Families[0], size);
                len = TextRenderer.MeasureText(en, font);
            }
           maxHeight = maxHeight > len.Height ? maxHeight : len.Height;
            g.DrawString(en, font, new SolidBrush(Color.White), (img.Width - len.Width) / 2, img.Height / 2 - 25 - maxHeight);


            string name = GetRandomString(5);
            try
            {
                img.Save(@"C:\ImagesForVk\" + name + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg); //путь и имя сохранения файла
            } catch (Exception e)
            {
                myLog.WriteEntry($"ошибка со сохранением {e.Message} {e.InnerException} {e.StackTrace}");
            }
            return name;
        }

        int GetMaxLenght(string a, string b)
        {
            return a.Length > b.Length ? a.Length : b.Length;
        }

        string GetRandomString(int Length)
        {
            string Alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;
            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }
            return sb.ToString();
        }
    }
}
