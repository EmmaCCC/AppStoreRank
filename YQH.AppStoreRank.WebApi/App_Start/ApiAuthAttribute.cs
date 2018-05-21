using System;
using System.Web.Http;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.Common;


namespace YQH.AppStoreRank.WebApi
{
    public class ApiAuthAttribute : AuthorizeAttribute
    {
        public new string[] Roles { get; set; }

        public string Policy { get; set; }
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                var auth = UserAuth.Current;
                string policy = (string) auth.GetValue("Policy");
                if (string.IsNullOrEmpty(Policy))
                {
                    return true;
                }

                if (Policy.ToLower() != policy.ToLower())
                { 
                    LogHelper.WriteLog(this.GetType(), "policy wrong");
                    return false;
                }

                if (Roles == null)
                {
                    return true;
                }
               
                if (auth.IsRole(string.Join(",", Roles)))
                {
                    return true;
                }
                LogHelper.WriteLog(this.GetType(), "role wrong");
                return false;

            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(this.GetType(), ex);
                return false;
            }
        }


    }
}