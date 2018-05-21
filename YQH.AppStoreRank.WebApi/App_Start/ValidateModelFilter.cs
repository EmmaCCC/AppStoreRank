using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace YQH.AppStoreRank.WebApi
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string msg = string.Empty;
            if (actionContext.ModelState.IsValid == false)
            {
                foreach (var item in actionContext.ModelState.Values)
                {
                    msg = item.Errors[item.Errors.Count-1].ErrorMessage;

                }
                HttpResponseMessage rm = new HttpResponseMessage(HttpStatusCode.OK);
                rm.Content = new StringContent("{\"status\": 1, \"message\":\"" + msg + "\"}", Encoding.UTF8, "appliction/json");
                actionContext.Response = rm;
            }
        }
    }
}