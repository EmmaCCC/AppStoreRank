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
    public class WithdrawBLL
    {
        protected IBaseRepository dataAccess = RepositoryFactory.GetDataAccess();

        /// <summary>
        /// 获取提现申请记录
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public dynamic GetPageList(int pageindex, int pagesize)
        {
            try
            {
                Guid userId = Guid.Parse(UserAuth.Current.Id);
                var query = dataAccess.LoadEntities<WithdrawRecord>(c => c.UserId == userId);
                int totalCount, pageCount;
                var list = dataAccess.LoadPageEntities<WithdrawRecord, DateTime>(query, pageindex, pagesize, out totalCount, out pageCount, false, c => c.CreateTime).ToList().Select(item => new
                {
                    id = item.Id,
                    username = item.Account.UserName,
                    linkphone = item.Account.Phone,
                    money = item.Money,
                    status = item.Status.ToString(),
                    oper = (item.Operator == null ? "" : dataAccess.Find<Account>(item.Operator).UserName),
                    opertime = (item.OperateTime == null ? "" : item.OperateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                    remark = item.Remark,
                    createtime = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
                });

                return new
                {
                    status = 0,
                    message = new
                    {
                        totalCount = totalCount,
                        pageCount = pageCount,
                        list = list
                    }
                };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                return new { status = 1, message = ex.Message };
            }
        }

        /// <summary>
        /// 提现申请
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public dynamic AddInfo(dynamic info)
        {
            try
            {
                if (DateTime.Now.Day != 20)
                {
                    return new { status = 1, message = "只能在每月20号申请提现!" };
                }
                Guid currentUserId = Guid.Parse(UserAuth.Current.Id);
                Account accountItme = dataAccess.Find<Account>(currentUserId);
                if (accountItme.WithdrawPwd != (info.withdrawpwd.ToString()))
                {
                    return new { status = 1, message = "提现密码错误，请重试!" };
                }

                decimal withdrawMoney = decimal.Parse(info.money.ToString());
                if (accountItme.Amount < withdrawMoney)
                {
                    return new { status = 1, message = "申请提现金额超出个人账户余额!" };
                }

                WithdrawRecord item = new WithdrawRecord();
                item.Id = Guid.NewGuid();
                item.CreateTime = DateTime.Now;
                item.Status = WithdrawStatus.已申请;
                item.Money = withdrawMoney;
                item.UserId = currentUserId;

                accountItme.Amount -= withdrawMoney;
                dataAccess.Add<WithdrawRecord>(item);
                dataAccess.SaveChanges();
                return new { status = 0, message = "提现申请已提交!" };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex.Message);
                return new { status = 1, message = ex.Message };
            }
        }

    }
}
