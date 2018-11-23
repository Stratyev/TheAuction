using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TheAuction.Infrastructure.Quartz;
using TheAuction.Infrastructure;
using TheAuction.Models;
using CodeFirst;

namespace TheAuction
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //new TimerInitializer();
            new Initializer();

            //new JustTimer(10000);
            //new JustThreadingTimer(10000);

            //CheckCreatorScheduler CCS = new CheckCreatorScheduler();
            //CCS.Start();
        }
    }
}
