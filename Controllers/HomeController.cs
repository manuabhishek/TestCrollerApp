using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using System.Xml;
using TestCrollerApp.Models;

namespace TestCrollerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICrawllerManager _crawllerManager;
        public HomeController(ICrawllerManager crawllerManager)
        {
            _crawllerManager = crawllerManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CrawllerInfo obj)
        {
            try
            {               
                return View(_crawllerManager.GetInfo(obj.URL));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}