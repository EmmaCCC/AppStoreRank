using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YQH.AppStoreRank.BLL.RegisterLogin
{
    public class RegisterLoginFactory
    {
        public static IRegisterLogin GetRegisterLogin(RegisterLoginType type)
        {
            switch (type)
            {
                case RegisterLoginType.Default:
                case RegisterLoginType.Web:
                case RegisterLoginType.WebAdmin:
                    {
                        return new WebRegisterLogin();
                    }
                case RegisterLoginType.Phone:
                    {
                        return new PhoneRegisterLogin();
                    }
                default:
                    {
                        return null;
                    }

            }
        }
    }
}
