using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;

namespace TestCrollerApp.Models
{
    public interface IWebCrawllerHelper
    {
        string GetHTMLAsString(string url);

        IEnumerable<string> GetAllImages(string url, string htmlCode);

        string GetHTMLToText(string htmlCode);
       
    }
}