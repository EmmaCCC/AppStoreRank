using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Routing;

namespace YQH.AppStoreRank.Web.App_Start
{
    public class RefreshFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public RefreshFilter()
        {

        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            // if (string.IsNullOrEmpty(_key))
            int key = actionContext.Request.RequestUri.AbsoluteUri.GetHashCode();
            HttpResponseMessage rm = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            rm.Content = new StringContent("{\"status\": 1, \"message\":\"请求太频繁\"}", Encoding.UTF8, "appliction/json");
            
            Common.RedisHelper redis = new Common.RedisHelper(5);
            if (redis.IsKey(HttpContext.Current.Request.UserHostAddress + key))
                actionContext.Response = rm;
            else
                redis.SetString(HttpContext.Current.Request.UserHostAddress + key, "", new TimeSpan(0, 0, 2));
            
        }
    }
}