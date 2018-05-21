using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using YQH.AppStoreRank.Common;

namespace YQH.AppStoreRank.WebApi
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            
            LogHelper.WriteLog(this.GetType(), actionExecutedContext.Exception);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonHelper.Serialize(new { status = 500, message = "服务器繁忙，请稍后再试" }));
            actionExecutedContext.Response = response;

            base.OnException(actionExecutedContext);
        }
    }
}