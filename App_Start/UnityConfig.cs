using System.Web.Mvc;
using TestCrollerApp.Models;
using Unity;
using Unity.Mvc5;

namespace TestCrollerApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IWebCrawllerHelper, WebCrawllerHelper>();
            container.RegisterType<ICrawllerManager, CrawllerManager>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}