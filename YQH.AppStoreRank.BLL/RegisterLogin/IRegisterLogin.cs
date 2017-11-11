using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.BLL.RegisterLogin.Param;

namespace YQH.AppStoreRank.BLL.RegisterLogin
{
    public interface IRegisterLogin
    {
        IIdentity Login(LoginParam param);

        IIdentity Register(RegisterParam param);
    }
}
