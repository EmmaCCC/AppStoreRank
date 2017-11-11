using System;
using System.Web;

namespace YQH.AppStoreRank.BLL.Auth
{
    public class CookieStore
    {
        public static void Clear()
        {
            ClearCookie("token");
            ClearCookie("refresh");
        }

        private static void ClearCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

    }
}