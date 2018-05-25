using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.BLL.RegisterLogin.Param;

namespace YQH.AppStoreRank.BLL.RegisterLogin
{
    public interface IRegisterLogin
    {
        IAuthIdentity Login(LoginParam param);

        IAuthIdentity Register(RegisterParam param);
    }
}
