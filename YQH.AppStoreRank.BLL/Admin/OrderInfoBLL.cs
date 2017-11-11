using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Enums;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Admin
{
    public class OrderInfoBLL
    {
        protected IBaseRepository dataAccess  = new BaseRepository();

        //protected AppStoreRankContext dataAccess = DbContextFactory.GetCurrentDbContext() as AppStoreRankContext;
        /// <summary>
        /// 获取订单列表信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="status"></param>
        /// <param name="appname"></param>
        /// <param name="username"></param>

        /// <returns></returns>
        public dynamic GetPageList(int pageindex, int pagesize, string starttime, string endtime, int status, string appname, string username)
        {
            try
            {



                //using (var db = new AppStoreRankContext())
                //{
                //    //var query = db.OrderInfos.AsQueryable();
                //    var query = dataAccess.LoadEntities<OrderInfo>();

                //    var url = ConfigurationManager.AppSettings["WebDoamin"];
                //    int totalCount = query.Count();
                //    int pageCount = (int)(Math.Ceiling((double)totalCount / pagesize));

                //    Stopwatch sw = new Stopwatch();

                //    sw.Start();

                //    var list = query.OrderByDescending(c => c.CreateTime).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList().Select(item => new
                //    {
                //        id = item.Id,
                //        appid = item.TaskInfo.AppId,
                //        appname = item.TaskInfo.App.Name,
                //        tasktype = item.TaskInfo.Type.ToString(),
                //        username = item.Account.UserName,
                //        idfa = item.IDFA,
                //        ipaddress = item.IPAddress,
                //        status = item.Status.ToString(),
                //        starttime = (item.StartTime == null ? "" : item.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                //        endtime = (item.EndTime == null ? "" : item.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                //        createtime = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                //        evidence = String.IsNullOrEmpty(item.Evidence) ? JsonConvert.DeserializeObject("{\"pic\":\"\",\"nickname\":\"\"}") : JsonConvert.DeserializeObject(item.Evidence.Replace("/Upload/Images/", ConfigurationManager.AppSettings["WebDoamin"] + "/Upload/Images/")),
                //        money = item.Money
                //    }).ToList();
                //    sw.Stop();
                //    return new
                //    {
                //        mode = "no using dbcontextfactory",
                //        status = 0,
                //        time = sw.Elapsed.Milliseconds,
                //        totalCount = totalCount,
                //        pageCount = pageCount,
                //        message = list
                //    };
                //}




                var query = dataAccess.LoadEntities<OrderInfo>();

                var url = ConfigurationManager.AppSettings["WebDoamin"];
                int totalCount, pageCount;


                if (!String.IsNullOrEmpty(starttime) && !String.IsNullOrEmpty(endtime))
                {
                    DateTime start = DateTime.Parse(starttime), end = DateTime.Parse(endtime);
                    query = query.Where(c => c.StartTime >= start && c.EndTime <= end);
                }

                if (status != -1)
                {
                    query = query.Where(c => c.Status == (OrderStatus)status);
                }

                if (!string.IsNullOrEmpty(appname))
                {
                    var taskidList = dataAccess.LoadEntities<TaskInfo>(c => c.AppId.Contains(appname)).Select(c => c.Id).ToList();
                    query = query.Where(c => taskidList.Contains(c.TaskInfoId));
                }

                if (!string.IsNullOrEmpty(username))
                {
                    var useridList = dataAccess.LoadEntities<Account>(c => c.UserName.Contains(username)).Select(c => c.Id).ToList();
                    query = query.Where(c => useridList.Contains(c.UserId));
                }

                var pageQuery = dataAccess.LoadPageEntities(query,pageindex, pagesize, out totalCount,
                    out pageCount, false, u => u.CreateTime);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                var list1 = pageQuery.ToList();
              

                var list = list1.Select(item => new
                {
                    id = item.Id,
                    appid = item.TaskInfo.AppId,
                    appname = item.TaskInfo.App.Name,
                    tasktype = item.TaskInfo.Type.ToString(),
                    username = item.Account.UserName,
                    idfa = item.IDFA,
                    ipaddress = item.IPAddress,
                    status = item.Status.ToString(),
                    starttime = (item.StartTime == null ? "" : item.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                    endtime = (item.EndTime == null ? "" : item.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                    createtime = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    evidence = String.IsNullOrEmpty(item.Evidence) ? JsonConvert.DeserializeObject("{\"pic\":\"\",\"nickname\":\"\"}") : JsonConvert.DeserializeObject(item.Evidence.Replace("/Upload/Images/", url + "/Upload/Images/")),
                    money = item.Money
                }).ToList();

                sw.Stop();
                return new
                {
                    mode = "using orderinforepositio2",
                    status = 0,
                    time = sw.Elapsed.Milliseconds,
                    totalCount = totalCount,
                    pageCount = pageCount,
                    message = list
                };


            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                return new { status = 1, message = ex.Message };
            }
        }

        /// <summary>
        /// 导出数据列表
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="status"></param>
        /// <param name="appname"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<OrderInfo> ExportList(string starttime, string endtime, int status, string appname, string username)
        {
            try
            {
                //var query = dataAccess.LoadEntities<OrderInfo>(c => true);
                //if (!String.IsNullOrEmpty(starttime) && !String.IsNullOrEmpty(endtime))
                //{
                //    DateTime start = DateTime.Parse(starttime), end = DateTime.Parse(endtime);
                //    query = query.Where(c => c.StartTime >= start && c.EndTime <= end);
                //}

                //if (status != -1)
                //{
                //    query = query.Where(c => c.Status == (OrderStatus)status);
                //}

                //if (!string.IsNullOrEmpty(appname))
                //{
                //    var taskidList = dataAccess.LoadEntities<TaskInfo>(c => c.AppId.Contains(appname)).Select(c => c.Id).ToList();
                //    query = query.Where(c => taskidList.Contains(c.TaskInfoId));
                //}

                //if (!string.IsNullOrEmpty(username))
                //{
                //    var useridList = dataAccess.LoadEntities<Account>(c => c.UserName.Contains(username)).Select(c => c.Id).ToList();
                //    query = query.Where(c => useridList.Contains(c.UserId));
                //}

                //return query.OrderByDescending(c => c.CreateTime).ToList();
                return new List<OrderInfo>();
            }
            catch (Exception ex)
            {
                return new List<OrderInfo>();
            }
        }
    }
}
