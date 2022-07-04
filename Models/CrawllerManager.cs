using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TestCrollerApp.Models
{
    public class CrawllerManager
    {
        public CrawllerInfo GetInfo(string url)
        {
            CrawllerInfo objInfo = new CrawllerInfo();
            WebCrawllerHelper objCrawller = new WebCrawllerHelper();            
            string hmlCode = objCrawller.GetHTMLAsString(url);
            objInfo.ImageList = objCrawller.GetAllImages(url,hmlCode);
            string siteData = objCrawller.GetHTMLToText(hmlCode);
            var ary = siteData.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
            objInfo.WordCount = ary.Length;
            var reg = @"(?<!\S)[a-z-]+(?=[,.!?:;]?(?!\S))";

            var dict = new Dictionary<string, int>();

            foreach (var value in ary)
            {
                if (Regex.IsMatch(value, reg) && value.Length > 1)
                {
                    dict.TryGetValue(value, out int count);
                    dict[value] = count + 1;
                }
            }

            objInfo.TopTenWords = dict.OrderByDescending(x => x.Value).Take(10).ToDictionary(x => x.Key, x => x.Value);
            return objInfo;
        }
    }
}