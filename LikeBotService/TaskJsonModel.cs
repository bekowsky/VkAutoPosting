using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeBotService
{
   
        public partial class TaskJsonModel
        {
            public string Status { get; set; }
            public string Success { get; set; }
            public Data Data { get; set; }
        }

        public partial class Data
        {
            public string Url { get; set; }
            public string TaskType { get; set; }
            public string ServiceType { get; set; }
            public string Seconds { get; set; }
            public string Action { get; set; }
            public string UserPrice { get; set; }
            public string Comment { get; set; }
            public Answer Answer { get; set; }
           
        }

        public partial class Answer
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }

       
    }

