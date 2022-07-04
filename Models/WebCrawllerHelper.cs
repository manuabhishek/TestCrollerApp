using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace TestCrollerApp.Models
{
    public class WebCrawllerHelper
    {
        public string GetHTMLAsString(string url)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] htmlData = wc.DownloadData(url);
            return System.Text.Encoding.UTF8.GetString(htmlData);
        }
        public List<string> GetAllImages(string url, string HTMLCode)
        {
            List<string> ImageList = new List<string>();
            Uri objUri = new Uri(url);
            foreach (Match m in Regex.Matches(HTMLCode, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                var value = m.Groups[1].Value;
                if (value.Contains("http"))
                {
                    ImageList.Add(value);
                }
                else
                {
                    ImageList.Add(String.Concat(objUri.Scheme,"://",objUri.Authority,"/", value));
                }
                // add src to some array
            }
            return ImageList;
        }

        public string GetHTMLToText(string HTMLCode)
        {
            // Remove new lines since they are not visible in HTML
            HTMLCode = HTMLCode.Replace("\n", " ");

            // Remove tab spaces
            HTMLCode = HTMLCode.Replace("\t", " ");

            // Remove multiple white spaces from HTML
            HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");

            // Remove HEAD tag
            HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove images tag
            HTMLCode = Regex.Replace(HTMLCode, "<img.+?src=[\"'](.+?)[\"'].+?>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove anchor tag
            HTMLCode = Regex.Replace(HTMLCode, @"<a [^>]*?>(?<text>.*?)</a>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove any JavaScript
            HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
              , RegexOptions.IgnoreCase | RegexOptions.Singleline);

          
            StringBuilder sbHTML = new StringBuilder(HTMLCode);
           

            // Finally, remove all HTML tags and return plain text
            return System.Text.RegularExpressions.Regex.Replace(
              sbHTML.ToString(), "<[^>]*>", "");
        }
    }
}
