using System.Web.Http;
using YQH.AppStoreRank.BLL.Admin;
using YQH.AppStoreRank.Data;

namespace YQH.AppStoreRank.WebAdmin.Controllers.Api
{
    //[ApiAuth(Roles = new AccountType[] { AccountType.管理员 })]
    public class OrderInfoController : ApiController
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="status"></param>
        /// <param name="appname"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public dynamic GetList(int pageindex, int pagesize, string starttime, string endtime, int status, string appname, string username)
        {
            try
            {
                OrderInfoBLL orderInfoBLL = new OrderInfoBLL();
                //throw new Exception("错误");
                return orderInfoBLL.GetPageList(pageindex, pagesize, starttime, endtime, status, appname, username);
            }
            catch (ErrorMsgException ex)
            {
                return new { status = 1, message = ex.Message };
            }
        }
    }
}