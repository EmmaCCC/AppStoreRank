using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YQH.AppStoreRank.Data;
using YQH.IOS;

namespace YQH.AppStoreRank.WebAdmin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            Common.LogHelper.WriteLog(this.GetType(), ex);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var dbContext = HttpContext.Current.Items["DbContext"] as AppStoreRankContext;
            if (dbContext != null)
            {
                dbContext.Dispose();
                HttpContext.Current.Items.Clear();

            }
        }
    }
}
