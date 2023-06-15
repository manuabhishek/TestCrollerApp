using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TestCrollerApp.Models
{
    public class CrawllerManager : ICrawllerManager
    {
        private readonly IWebCrawllerHelper _webCrawllerHelper;
        public CrawllerManager(IWebCrawllerHelper webCrawllerHelper)
        {
            _webCrawllerHelper = webCrawllerHelper;
        }

        public CrawllerInfo GetInfo(string url)
        {
            var objInfo = new CrawllerInfo();                  
            var hmlCode = _webCrawllerHelper.GetHTMLAsString(url);
            objInfo.ImageList = _webCrawllerHelper.GetAllImages(url,hmlCode);
            var siteData = _webCrawllerHelper.GetHTMLToText(hmlCode);
            var ary = siteData.Split(Constants.splitterDelimeters, StringSplitOptions.RemoveEmptyEntries);
            objInfo.WordCount = ary.Length;
            var reg = Constants.wordRegex;

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