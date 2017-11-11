using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.Web
{
    public class ApiAuthAttribute : AuthorizeAttribute
    {
        public new AccountType[] Roles { get; set; }
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
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


    }
}