using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    class VkService
    {
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public string GetUploadServer(string group, string token)
        {
            string getServerUrl = "https://api.vk.com/method/photos.getWallUploadServer?group_id=" + group + "" +
                "&access_token=" + token + "&v=5.70";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getServerUrl);
            string serverUrl;
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                serverUrl = reader.ReadToEnd();
            }
            dynamic UrlInfo = JsonConvert.DeserializeObject(serverUrl.Replace("upload_url", "UploadUrl"));
            serverUrl = UrlInfo.response.UploadUrl;
            return serverUrl;


        }

        public (string, string, string) UploadAttachments(string path, string serverUrl)
        {
            string data = "";
            using (var webClient = new WebClient())
            {

                var response = webClient.UploadFile(serverUrl, path);
                data = System.Text.Encoding.UTF8.GetString(response);
            }
            dynamic success = JsonConvert.DeserializeObject(data);
            string server = success.server;
            string photo = success.photo;
            string hash = success.hash;
            return (server, photo, hash);
        }

        public string SaveAttachments(string group, string server, string photo, string hash, string token)
        {
            string info;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vk.com/method/photos.saveWallPhoto" +
                "?group_id=" + group + "" +
                "&photo=" + photo + "" +
                "&server=" + server + "" +
                "&access_token=" + token + "" +
                "&hash=" + hash + "" +
                "&v=5.21"
                );

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                info = reader.ReadToEnd();
            }

            dynamic photoinfo = JsonConvert.DeserializeObject(info.Replace("owner_id", "Owner"));
            int count = photoinfo.response.Count;
            string attachment = "";
            for (int i = 0; i < count; i++)
            {
                attachment = "photo" + photoinfo.response[i].Owner + "_" + photoinfo.response[i].id;
                if (i != count - 1)
                    attachment += ",";
            }
            return attachment;
        }
        public void PostAttachments(string group, string attachment, string token)
        {
            string url = "https://api.vk.com/method/wall.post" +
                "?owner_id=-" + group +
                "&attachments=" + attachment +
                "&access_token=" + token + "" +
                "&v=5.70";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
        }



        public void Posting(string token, string path, string group)
        {
            // string token = "", path = @"C:\Users\Xiaomi\source\repos\AutoPosting\WordService\allwords_files\picturecreated.jpg", group = "199275400";

            string serverUrl = GetUploadServer(group, token);
            (string server, string photo, string hash) = UploadAttachments(path, serverUrl);
            string attachment = SaveAttachments(group, server, photo, hash, token);
            PostAttachments(group, attachment, token);
        }
    }
}
