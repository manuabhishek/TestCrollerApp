using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCrollerApp.Models
{
    public static class Constants
    {
        public static char[] splitterDelimeters = new char[] { '.', '?', '!', ' ', ';', ':', ',' };
        public static string wordRegex = @"(?<!\S)[a-z-]+(?=[,.!?:;]?(?!\S))";
        public static string imgRegex = "<img.+?src=[\"'](.+?)[\"'].+?>";
    }
}