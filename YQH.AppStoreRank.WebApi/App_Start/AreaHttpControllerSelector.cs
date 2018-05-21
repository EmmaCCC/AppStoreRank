using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace YQH.AppStoreRank.WebApi
{
    public class AreaHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        /// <summary>  
        /// Lazy 当前程序集中包含的所有IHttpController反射集合，TKey为小写的Controller  
        /// </summary>  
        private readonly Lazy<ILookup<string, Type>> _apiControllerTypes;
        private ILookup<string, Type> ApiControllerTypes
        {
            get
            {
                return this._apiControllerTypes.Value;
            }
        }
        /// <summary>  
        /// Initializes a new instance of the AreaHttpControllerSelector class  
        /// </summary>  
        /// <param name="configuration"></param>  
        public AreaHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            this._configuration = configuration;
            this._apiControllerTypes = new Lazy<ILookup<string, Type>>(this.GetApiControllerTypes);
        }
        /// <summary>  
        /// 获取当前程序集中 IHttpController反射集合  
        /// </summary>  
        /// <returns></returns>  
        private ILookup<string, Type> GetApiControllerTypes()
        {
            IAssembliesResolver assembliesResolver = this._configuration.Services.GetAssembliesResolver();
            return this._configuration.Services.GetHttpControllerTypeResolver()
                .GetControllerTypes(assembliesResolver)
                .ToLookup(t => t.Name.ToLower().Substring(0, t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length), t => t);
        }
        /// <summary>  
        /// Selects a System.Web.Http.Controllers.HttpControllerDescriptor for the given System.Net.Http.HttpRequestMessage.  
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>  
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor des = null;
            string controllerName = this.GetControllerName(request);
            if (!string.IsNullOrWhiteSpace(controllerName))
            {
                var groups = this.ApiControllerTypes[controllerName.ToLower()];
                if (groups != null && groups.Any())
                {
                    string endString;
                    var routeDic = request.GetRouteData().Values;//存在controllerName的话必定能取到IHttpRouteData  
                    if (routeDic.Count > 1)
                    {
                        StringBuilder tmp = new StringBuilder();
                        foreach (var key in routeDic.Keys)
                        {
                            tmp.Append('.');
                            tmp.Append(routeDic[key]);
                            if (key.Equals(DefaultHttpControllerSelector.ControllerSuffix, StringComparison.CurrentCultureIgnoreCase))
                            {//如果是control，则代表命名空间结束  
                                break;
                            }
                        }
                        tmp.Append(DefaultHttpControllerSelector.ControllerSuffix);
                        endString = tmp.ToString();
                    }
                    else
                    {
                        endString = string.Format(".{0}{1}", controllerName, DefaultHttpControllerSelector.ControllerSuffix);
                    }
                    //取NameSpace节点数最少的Type  
                    var type = groups.Where(t => t.FullName.EndsWith(endString, StringComparison.CurrentCultureIgnoreCase))
                        .OrderBy(t => t.FullName.Count(s => s == '.')).FirstOrDefault();//默认返回命名空间节点数最少的第一项  
                    if (type != null)
                    {
                        des = new HttpControllerDescriptor(this._configuration, controllerName, type);
                    }
                }
            }
            if (des == null)
            {
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound,
                    string.Format("No route providing a controller name was found to match request URI '{0}'", request.RequestUri)));
            }
            return des;
        }
    }  
}