using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCrollerApp.Models
{
    public interface ICrawllerManager
    {
        CrawllerInfo GetInfo(string url);
    }
}