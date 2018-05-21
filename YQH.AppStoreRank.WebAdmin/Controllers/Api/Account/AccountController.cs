using System;
using System.Linq;
using System.Web.Http;
using YQH.AppStoreRank.BLL.Admin;
using YQH.AppStoreRank.BLL.RegisterLogin;
using YQH.AppStoreRank.BLL.RegisterLogin.Param;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Enums;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.WebAdmin.Controllers.Api.Account
{

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {

        /// <summary>
        /// 分页获取用户的信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>

        [ApiAuth(Roles = new AccountType[] { AccountType.管理员 })]
        [WebApiExceptionFilter]
        public dynamic Get(int pageIndex, int pageSize, AccountType type, string name)
        {
            try
            {
                AccountBLL bll = new AccountBLL();
                var data = bll.GetPageList(pageIndex, pageSize, type, name);
                return new { status = 0, result = data };
            }

            catch (ErrorMsgException ex)
            {
                return new { status = ex.Status, message = ex.Message };
            }
            //catch (Exception ex)
            //{
            //    return new { status = 500, message = ex.Message };
            //}

        }

        /// <summary>
        /// 获取单个的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiAuth(Roles = new AccountType[] { AccountType.管理员 })]
        public dynamic Get(Guid id)
        {
            try
            {
                var s = this.ModelState;
                AccountBLL bll = new AccountBLL();
                var data = bll.GetAccountInfo(id);
                return new { status = 0, result = data };
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

        /// <summary>
        /// 添加或者修改一个用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ApiAuth(Roles = new AccountType[] { AccountType.管理员 })]
        public dynamic Post(Data.Models.Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string msg = ModelState.Values.First().Errors[0].ErrorMessage;
                    return new {status = 1, message = msg};
                }
                AccountBLL bll = new AccountBLL();
                bll.Save(account);
                return new { status = 0 };
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


        [Route("Login")]
        [HttpPost]
        public dynamic Login(LoginParam param)
        {
            try
            {
                var registerlogin = RegisterLoginFactory.GetRegisterLogin(RegisterLoginType.WebAdmin);
                registerlogin.Login(param);
                return new { status = 0 };
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

        [ApiAuth]
        [Route("ChangePassword")]
        [HttpPost]

        public dynamic ChangePassword(dynamic obj)
        {
            try
            {
                AccountBLL bll = new AccountBLL();
                bll.IsChange(obj);
                return new { status = 0 };
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



        [ApiAuth(Roles = new AccountType[] { AccountType.管理员 })]
        [HttpPost, Route("Disabled")]
        public dynamic SetDisabled(Data.Models.Account account)
        {
            try
            {

                AccountBLL bll = new AccountBLL();
                bll.SetDisabled(account.Id, account.IsDisabled);
                return new { status = 0 };
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

        [ApiAuth]
        [Route("EditMoney")]
        [HttpPost]
        public dynamic EditMoney(dynamic obj)
        {
            try
            {
                AccountBLL bll = new AccountBLL();
                return bll.EditMoney(obj);
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
