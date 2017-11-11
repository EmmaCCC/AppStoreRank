using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.WebAdmin
{
    public class MvcAuthAttribute : AuthorizeAttribute
    {
        public new AccountType[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                var auth = UserAuth.Current;

                if (Roles == null)
                {
                    return true;
                }
                List<string> roles = Roles.Select(r => ((int)r).ToString()).ToList();
                if (auth.IsRole(string.Join(",", roles)))
                {
                    return true;
                }
                return false;
              

            }
            catch
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url = "/Account/Login?returnUrl=" + filterContext.HttpContext.Request.Url;
            filterContext.Result = new RedirectResult(url);
        }


    }
}