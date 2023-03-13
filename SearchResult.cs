using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class SearchResult {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ObjectType { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string[] Years { get; set; }
        public string OriginalTitle { get; set; }
        public bool Has51 { get; set; }
    }

}
