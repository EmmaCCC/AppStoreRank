using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.BLL.RegisterLogin;
using YQH.AppStoreRank.BLL.RegisterLogin.Param;
using YQH.AppStoreRank.Data;

namespace YQH.AppStoreRank.Web.Controllers.Api.User
{
    public class UserController : ApiController
    {
        [HttpPost]
        public dynamic Login(LoginParam param)
        {
            try
            {
                var registerlogin = RegisterLoginFactory.GetRegisterLogin(RegisterLoginType.Phone);
                var user = registerlogin.Login(param);
                var token = UserAuth.GetToken(user);
                return new { status = 0, result = token };
            }
            catch (ErrorMsgException ex)
            {
                return new { status = ex.Status, message = ex.Message };
            }
            catch (Exception ex)
            {
                return new { status = 500, message = ex.Message };
            }
        }

        
        public dynamic Get(int pageIndex)
        {
            try
            {
                //var registerlogin = RegisterLoginFactory.GetRegisterLogin(RegisterLoginType.Phone);
                //var user = registerlogin.Login(param);
                //var token = UserAuth.GetToken(user);
                return new { status = 0};
            }
            catch (ErrorMsgException ex)
            {
                return new { status = ex.Status, message = ex.Message };
            }
            catch (Exception ex)
            {
                return new { status = 500, message = ex.Message };
            }
        }
    }
}
