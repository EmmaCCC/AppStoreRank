using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace YQH.AppStoreRank.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //string origins = ConfigurationManager.AppSettings["Origins"];
            ////跨域配置
            //config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute(origins, "*", "*")
            //{
            //    SupportsCredentials = true
            //});


            // Web API configuration and services
            var json = config.Formatters.JsonFormatter;
            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 干掉XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //序列化为小写开头的驼峰命名法
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'/'MM'/'dd' 'HH':'mm':'ss"
            });
            // Web API routes
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new WebApiExceptionFilterAttribute());
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(

                   name: "DefaultApi3",
                   routeTemplate: "{area1}/{area2}/{controller}/{action}/{id}",
                   defaults: new { id = RouteParameter.Optional },
                   constraints: new { action = @"\D+" }
              );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "{area}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { action = @"\D+" }

            );

            config.Routes.MapHttpRoute(
                 name: "DefaultApi1",
                 routeTemplate: "{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional },
                 constraints: new { action = @"\D+" }
             );
            //紧急打补丁
        }
    }
}
