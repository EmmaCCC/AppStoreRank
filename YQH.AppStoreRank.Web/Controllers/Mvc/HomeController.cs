using System.IO;
using System.Web.Mvc;

namespace YQH.AppStoreRank.Web.Controllers.Mvc
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult UploadFile()
        {
            if (Request.Files.Count > 0)
            {
                Request.Files[0].SaveAs(Server.MapPath("~/App_Data/") + Path.GetFileName(Request.Files[0].FileName));
              
            }
            return Json(new { status = 1, message = "success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error(int id)
        {
            if (id == 404)
            {
                return View("Error404");

            }
            if (id == 500)
            {
                return View("Error500");
            }

            return View();
        }
    }
}