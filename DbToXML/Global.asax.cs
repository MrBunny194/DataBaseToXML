using Data;
using DbToXML.Util;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DbToXML
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            Database.SetInitializer(new DbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            string cultureName = null;

            // Получаем куки из контекста, которые могут содержать установленную культуру
            var cultureCookie = HttpContext.Current.Request.Cookies["lang"];

            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureName = "ru";
            }

            // установка культуры
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}
