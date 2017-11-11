using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YQH.AppStoreRank.BLL.Admin;
using YQH.AppStoreRank.Data.Enums;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.WebAdmin.Controllers.Mvc
{
    [MvcAuth(Roles = new AccountType[] { AccountType.管理员 })]
    public class OrderInfoController : Controller
    {
        public ActionResult List()
        {
            return View();
        }

        public void ExportOrder(string starttime, string endtime, int status, string appname, string username)
        {
            OrderInfoBLL orderInfoBLL = new OrderInfoBLL();
            List<OrderInfo> exportList = orderInfoBLL.ExportList(starttime, endtime, status, appname, username);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("AppId\t" + "应用名称\t" + "任务类型\t" + "用户名\t" + "IDFA\t" + "IP地址\t" + "任务开始时间\t" + "任务完成时间\t" + "状态\t" + "订单创建时间\t");
            foreach (OrderInfo item in exportList)
            {
                sb.AppendLine(item.TaskInfo.AppId + "\t" + item.TaskInfo.App.Name + "\t" + item.TaskInfo.Type.ToString() + "\t" + item.Account.UserName + "\t" + item.IDFA + "\t" + item.IPAddress + "\t" + (item.StartTime == null ? "" : item.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss")) + "\t" + (item.EndTime == null ? "" : item.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss")) + "\t" + item.Status.ToString() + "\t" + item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") + "\t");
            }
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            System.Web.HttpContext.Current.Response.AddHeader
        ("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("订单记录明细" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"));
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
    }
}