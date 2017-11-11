using System;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.BLL.RegisterLogin.Param;
using YQH.AppStoreRank.Data;

namespace YQH.AppStoreRank.BLL.RegisterLogin
{
    public class WebRegisterLogin : BaseRegisterLogin
    {
        public override IIdentity Login(LoginParam param)
        {
            try
            {
                var user = base.Login(param);
                SaveCookie(user);
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
