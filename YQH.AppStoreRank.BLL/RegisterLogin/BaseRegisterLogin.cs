using System;
using System.Web;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.BLL.RegisterLogin.Param;
using YQH.AppStoreRank.Common;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.RegisterLogin
{
    public class BaseRegisterLogin : IRegisterLogin
    {
        protected IBaseRepository dataAccess = RepositoryFactory.GetDataAccess();

        public virtual IAuthIdentity Login(LoginParam param)
        {
            string password = DESEncrypt.MD5Encrypt(param.Password).ToLower();
            var user = dataAccess.First<Account>(u => u.UserName == param.UserName && u.Password.ToLower() == password);
            if (user == null)
            {
                throw new ErrorMsgException("用户名或者密码错误");
            }
            if (user.IsDisabled)
            {
                throw new ErrorMsgException("用户已经被禁用，请联系管理员");
            }
            return new BLL.Auth.Identity(user);
        }

        public virtual IAuthIdentity Register(RegisterParam param)
        {

            return new Identity(new AppStoreRank.Data.Models.Account());
        }



        /// <summary>
        /// 保存cookie
        /// </summary>
        /// <param name="user"></param>
        protected void SaveCookie(IAuthIdentity user)
        {
            TokenModel token = UserAuth.GetToken(user);
            SaveCookie(user, token);

        }

        /// <summary>
        /// 保存cookie
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        protected void SaveCookie(IAuthIdentity user, TokenModel token)
        {
            Data.Models.Account account = user.GetUser() as Data.Models.Account;
            //添加token到
            HttpCookie cookie = new HttpCookie("token", token.Token);
            cookie.Expires = DateTime.Now.AddHours(2);
            HttpContext.Current.Response.Cookies.Add(cookie);

            //添加refreshToken和token的失效时间，失效时间为100分钟
            DateTime validDatetime = DateTime.UtcNow.AddMinutes(100);
            var refresh = new
            {

                refreshToken = token.RefreshToken,
                vaildTime = Common.Timestamp.GetTimestamp(validDatetime).ToString(),
                userId = account.Id,
                userType = account.Type

            };

            HttpCookie refreshcookie = new HttpCookie("refresh", JsonHelper.Serialize(refresh));
            refreshcookie.Expires = DateTime.Now.AddHours(2);
            HttpContext.Current.Response.Cookies.Add(refreshcookie);
        }

    }
}
