using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public class WordsContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
    }
}
