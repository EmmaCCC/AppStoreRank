using System.Web.Mvc;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.WebAdmin.Controllers.Mvc
{

    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Logout()
        {
            CookieStore.Clear();
            return Redirect("/Account/Login");
        }

        [MvcAuth]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [MvcAuth(Roles = new AccountType[] { AccountType.管理员 })]
        public ActionResult List()
        {
            return View();
        }

        [MvcAuth(Roles = new AccountType[] { AccountType.管理员 })]
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Operation()
        {
            return View();
        }
    }
}