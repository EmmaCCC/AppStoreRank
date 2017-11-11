using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.Common;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Enums;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Admin
{
    public class AccountBLL
    {
        protected IBaseRepository dataAccess = RepositoryFactory.GetDataAccess();

        public dynamic GetPageList(int pageIndex, int pageSize, AccountType type, string name)
        {
            try
            {
                int totalCount, pageCount;

                var query = dataAccess.LoadEntities<Account>(u => true);
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(u => (u.UserName != null && u.UserName.Contains(name)) || (u.Phone != null && u.Phone.Contains(name)));
                }
                if (type != AccountType.全部)
                {
                    query = query.Where(u => u.Type == type);
                }

                var list = dataAccess.LoadPageEntities(query, pageIndex, pageSize, out totalCount, out pageCount, false, u => u.Amount).Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Phone,
                    u.Type,
                    u.Amount,
                    u.IsDisabled
                });

                return new
                {
                    totalCount,
                    pageCount,
                    pageIndex,
                    pageSize,
                    list
                };

            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                throw;
            }
        }


        public void Save(Account account)
        {
            try
            {
                Account acc = new Account();

                if (account.Id == Guid.Empty)
                {
                    var isExistUserName = dataAccess.Exist<Account>(u => u.UserName == account.UserName);
                    if (isExistUserName)
                    {
                        throw new ErrorMsgException("用户名已经存在");
                    }
                    var isExistPhone = dataAccess.Exist<Account>(u => u.Phone == account.Phone);

                    if (isExistPhone)
                    {
                        throw new ErrorMsgException("手机号已经存在");
                    }
                    acc.Id = Guid.NewGuid();
                }
                else
                {
                    acc = dataAccess.Find<Account>(account.Id);
                }
                acc.UserName = account.UserName;
                if (!string.IsNullOrEmpty(account.Password))
                {
                    acc.Password = Common.DESEncrypt.MD5Encrypt(account.Password);
                }
                acc.Phone = account.Phone;
                acc.WithdrawPwd = account.WithdrawPwd;
                acc.Type = account.Type;

                if (account.Id == Guid.Empty)
                {
                    dataAccess.Add<Account>(acc);
                }
                else
                {
                    dataAccess.Update<Account>(acc);
                }

                dataAccess.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IsChange(dynamic obj)
        {
            try
            {
                Guid currentId = Guid.Parse(UserAuth.Current.Id);
                var user = dataAccess.Find<Account>(currentId);
                if (user.Password == DESEncrypt.MD5Encrypt(obj.old.ToString()).ToLower())
                {
                    user.Password = DESEncrypt.MD5Encrypt(obj.new1.ToString()).ToLower();
                }
                else
                {
                    throw new ErrorMsgException("原始密码错误");
                }
                dataAccess.SaveChanges();

            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(Account), ex.Message);
                throw;
            }
        }

        public dynamic GetAccountInfo(Guid id)
        {
            try
            {
                var account = dataAccess.Find<Account>(id);
                return new
                {
                    account.Id,
                    account.UserName,
                    account.Password,
                    account.Phone,
                    account.WithdrawPwd,
                    account.Type
                };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(Account), ex.Message);
                throw;
            }
        }

        public void SetDisabled(Guid id, bool isDisabled)
        {
            try
            {
                var account = dataAccess.Find<Account>(id);
                if (account != null)
                {
                    account.IsDisabled = isDisabled;
                }
                dataAccess.Update(account);
                dataAccess.SaveChanges();
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(Account), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 后台管理员修改账户金额
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public dynamic EditMoney(dynamic info)
        {
            try
            {
                Account item = dataAccess.Find<Account>(Guid.Parse(info.id.ToString()));
                decimal money = decimal.Parse(info.money.ToString());
                Boolean opertype = Boolean.Parse(info.type.ToString());
                decimal beforeMoney = item.Amount;
                if (opertype)
                {
                    item.Amount += money;
                }
                else
                {
                    if (item.Amount < money)
                    {
                        return new { status = 1, message = "输入金额小于当前账户余额，无法减去!" };
                    }
                    item.Amount -= money;
                }
                decimal afterMoney = item.Amount;

                OperationRecord recordItem = new OperationRecord();
                recordItem.Id = Guid.NewGuid();
                recordItem.IsAdd = opertype;
                recordItem.Money = money;
                recordItem.UserId = item.Id;
                recordItem.UserName = item.UserName;
                recordItem.OperUserId = Guid.Parse(UserAuth.Current.Id);
                recordItem.OperUserName = UserAuth.Current.Name;
                recordItem.CreateTime = DateTime.Now;
                recordItem.BeforeMoney = beforeMoney;
                recordItem.AfterMoney = afterMoney;

                dataAccess.Update<Account>(item);
                dataAccess.Add<OperationRecord>(recordItem);
                dataAccess.SaveChanges();
                return new { status = 0, message = "保存成功!" };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex.Message);
                return new { status = 1, message = ex.Message };
            }
        }

    }
}
