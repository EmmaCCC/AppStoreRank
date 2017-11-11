using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Enums;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Web
{
    public class IncomeBLL
    {
        protected IBaseRepository dataAccess = RepositoryFactory.GetDataAccess();
        /// <summary>
        /// 获取当前账户收益、余额等信息
        /// </summary>
        /// <returns></returns>
        public dynamic GetAccountInfo()
        {
            try
            {
                var query = dataAccess.LoadEntities<OrderInfo>();
                DateTime todayMin = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                Guid userId = Guid.Parse(UserAuth.Current.Id);
                return new
                {
                    status = 0,
                    message = new
                    {
                        todaymoney = dataAccess.LoadEntities<OrderInfo>(c => c.UserId == userId && c.EndTime >= todayMin && c.EndTime <= DateTime.Now && c.Status == OrderStatus.已完成).Sum(c => c.Money),
                        allmoney = dataAccess.LoadEntities<OrderInfo>(c => c.UserId == userId && c.Status == OrderStatus.已完成).Sum(c => c.Money),
                        accountmoney = dataAccess.LoadEntities<Account>(c => c.Id == userId).FirstOrDefault().Amount
                    }
                };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                return new { status = 1, message = ex.Message };
            }
        }

    }
}
