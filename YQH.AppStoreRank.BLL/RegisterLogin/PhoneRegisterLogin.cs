using System;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.BLL.RegisterLogin.Param;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.RegisterLogin
{
    public class PhoneRegisterLogin : BaseRegisterLogin
    {
        public override IIdentity Login(LoginParam param)
        {
            try
            {
                IIdentity user = null;

                if (!string.IsNullOrEmpty(param.RefreshToken))
                {
                    UserAuth.GetToken(param.RefreshToken, (userid) =>
                     {
                         user = new Identity(dataAccess.Find<Account>(Guid.Parse(userid)));
                         return user;
                     });
                    return user;
                }
                user = base.Login(param);
                return user;
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                throw ex;
            }
        }

        public override IIdentity Register(RegisterParam param)
        {

            throw new NotImplementedException();
        }
    }
}
