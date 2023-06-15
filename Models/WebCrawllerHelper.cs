using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace TestCrollerApp.Models
{
    public class WebCrawllerHelper : IWebCrawllerHelper
    {
        public string GetHTMLAsString(string url)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] htmlData = wc.DownloadData(url);
            return System.Text.Encoding.UTF8.GetString(htmlData);
        }
        public IEnumerable<string> GetAllImages(string url, string htmlCode)
        {
            List<string> ImageList = new List<string>();
            Uri objUri = new Uri(url);
            foreach (Match m in Regex.Matches(htmlCode, Constants.imgRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                var value = m.Groups[1].Value;
                if (value.Contains("http"))
                {
                    ImageList.Add(value);
                }
                else
                {
                    ImageList.Add($"{objUri.Scheme}://{objUri.Authority}/{value}");
                }
                // add src to some array
            }
            return ImageList;
        }

        public string GetHTMLToText(string htmlCode)
        {
            // Remove new lines since they are not visible in HTML
            htmlCode = htmlCode.Replace("\n", " ");

            // Remove tab spaces
            htmlCode = htmlCode.Replace("\t", " ");

            // Remove multiple white spaces from HTML
            htmlCode = Regex.Replace(htmlCode, "\\s+", " ");

            // Remove HEAD tag
            htmlCode = Regex.Replace(htmlCode, "<head.*?</head>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove images tag
            htmlCode = Regex.Replace(htmlCode, "<img.+?src=[\"'](.+?)[\"'].+?>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove anchor tag
            htmlCode = Regex.Replace(htmlCode, @"<a [^>]*?>(?<text>.*?)</a>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove any JavaScript
            htmlCode = Regex.Replace(htmlCode, "<script.*?</script>", ""
              , RegexOptions.IgnoreCase | RegexOptions.Singleline);


            StringBuilder sbHTML = new StringBuilder(htmlCode);


            // Finally, remove all HTML tags and return plain text
            return System.Text.RegularExpressions.Regex.Replace(
              sbHTML.ToString(), "<[^>]*>", "");
        }
    }
}
