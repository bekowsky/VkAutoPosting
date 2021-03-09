using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeBotService
{
    public partial class TaskListJsonModel
    {
        public long Status { get; set; }
        public bool Success { get; set; }
        public Data Data { get; set; }
    }

    public partial class Data
    {
        public List<Item> Items { get; set; }
        public long Limit { get; set; }
    }

    public partial class Item
    {
        public long Id { get; set; }
        public Name Name { get; set; }
        public Uri Image { get; set; }
        public long TaskType { get; set; }
        public long ServiceType { get; set; }
        public Price Price { get; set; }
    }

    public partial class Name
    {
        public string Object { get; set; }
        public string Action { get; set; }
        public string ActionAlt { get; set; }
        public string Full { get; set; }
        public string FullAlt { get; set; }
        public string ShortAction { get; set; }
        public string ShortActionAlt { get; set; }
    }

    public partial class Price
    {
        public long Value { get; set; }
        public string Text { get; set; }
    }
}
