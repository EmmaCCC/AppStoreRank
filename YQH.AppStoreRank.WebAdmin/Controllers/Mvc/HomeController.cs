using System.Web.Mvc;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.WebAdmin.Controllers.Mvc
{
    public class HomeController : Controller
    {
        [MvcAuth(Roles = new AccountType[] { AccountType.管理员 })]
        public ActionResult Index()
        {
            return View();
        }
    }
}
