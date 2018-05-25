using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using YQH.AppStoreRank.Data;

namespace YQH.AppStoreRank.BLL.Auth
{
    public class UserAuth
    {
        const string SECRETKEY = "nTDW9RLu&xR&dY89QI75JWm*FFw$kr!i";
        const int EXPIRES_IN = 7200;
        const int REFRESH_EXPIRES_IN = 5184000;
        private readonly string _token;
        private IDictionary<string, object> _tokendic;
        

        private UserAuth(HttpContext context)
        {
            _token = GetHttpToken(context);
            Init();
        }

        public UserAuth(string token)
        {
            _token = token;
            Init();

        }

        public string Token
        {
            get { return _token; }
        }


        public object GetValue(string key)
        {
            if (_tokendic.ContainsKey(key))
                return _tokendic[key];
            else
                return null;
        }

   

        public string Id
        {
            get
            {
                return _tokendic["auth_id"].ToString();
            }
        }

        public string Name
        {
            get
            {
                return _tokendic["auth_name"].ToString();
            }
        }

        public bool IsRole(string role)
        {
            ArrayList roles = (ArrayList)_tokendic["auth_roles"];
            //因传入角色可能是多个角色，临时进行调整
            foreach (var r in roles)
            {
                if (role.Contains(r.ToString()))
                    return true;
            }
            return false;
        }


        public static TokenModel GetToken(IAuthIdentity user)
        {
            return CreateToken(user);
        }

        /// <summary>
        /// 根据刷新Token得到token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static TokenModel GetToken(string refreshToken, Func<string, IAuthIdentity> fun)
        {
            var dic = JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(refreshToken, SECRETKEY);
            if (dic["auth_flag"].ToString() != "refresh")
                throw new ErrorMsgException("无效的Token");
            if (Common.Timestamp.GetUTCTimestamp() - Convert.ToInt64(dic["expires_in"]) > REFRESH_EXPIRES_IN)
                throw new ErrorMsgException("Token已过期");
            return CreateToken(fun(dic["auth_id"].ToString()));
        }

        public static UserAuth Current
        {
            get
            {
                return new UserAuth(HttpContext.Current);
            }
        }

        public static string GetTempId()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get("TempId");
            if (cookie != null)
                return cookie.Value;
            return null;
           
        }

        private static TokenModel CreateToken(IAuthIdentity user)
        {
            var dic = user.GetTokenData();
            dic.Add("auth_id", user.GetId());
            dic.Add("auth_name", user.GetName());
            dic.Add("auth_roles", user.GetRoles());
            dic.Add("auth_flag", "token");
            dic.Add("expires_in", Common.Timestamp.GetUTCTimestamp());
            string token = JWT.JsonWebToken.Encode(dic, SECRETKEY, JWT.JwtHashAlgorithm.HS256);

            Dictionary<string, object> refreshdic = new Dictionary<string, object>();
            refreshdic.Add("auth_id", user.GetId());
            refreshdic.Add("auth_flag", "refresh");
            refreshdic.Add("expires_in", Common.Timestamp.GetUTCTimestamp());

            TokenModel tm = new TokenModel();
            tm.Token = JWT.JsonWebToken.Encode(dic, SECRETKEY, JWT.JwtHashAlgorithm.HS256);
            tm.RefreshToken = JWT.JsonWebToken.Encode(refreshdic, SECRETKEY, JWT.JwtHashAlgorithm.HS256);
            tm.Expires = EXPIRES_IN;
            tm.Id = user.GetId();
            return tm;
        }

        public static bool IsLogin()
        {
            try
            {
                var user = UserAuth.Current;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Init()
        {
            if (_token == null)
                throw new ErrorMsgException("无效的Token");

            _tokendic = JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(_token, SECRETKEY);
            if (_tokendic["auth_flag"].ToString() != "token")
                throw new ErrorMsgException("无效的Token");
            if (Common.Timestamp.GetUTCTimestamp() - Convert.ToInt64(_tokendic["expires_in"]) > EXPIRES_IN)
                throw new ErrorMsgException("Token已过期");
        }

        private string GetHttpToken(HttpContext context)
        {
            if (context.Request.QueryString["token"] != null)
                return context.Request.QueryString["token"];
            else if (context.Request.Headers.Get("Authorization") != null)
                return context.Request.Headers.Get("Authorization");
            else if (context.Request.Cookies["token"] != null)
                return context.Request.Cookies["token"].Value;
            else
                return null;
        }
    }
}
