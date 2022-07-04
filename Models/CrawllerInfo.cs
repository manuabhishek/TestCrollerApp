using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCrollerApp.Models
{
    public class CrawllerInfo
    {
        public string URL { get; set; }
        public List<string> ImageList { get; set; }
        public int WordCount { get; set; }
        public Dictionary<string, int> TopTenWords { get; set; }
    }

}