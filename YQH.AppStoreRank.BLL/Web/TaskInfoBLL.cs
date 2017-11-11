using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YQH.AppStoreRank.BLL.Auth;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Enums;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Web
{
    public class TaskInfoBLL
    {
        protected IBaseRepository dataAccess = RepositoryFactory.GetDataAccess();

        /// <summary>
        /// 获取我的任务列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public dynamic GetMyList(int tasktype, string idfa, int pageindex, int pagesize)
        {
            try
            {
                Guid currentUserId = Guid.Parse(UserAuth.Current.Id);
                var query = dataAccess.LoadEntities<OrderInfo>(c => c.IDFA == idfa && c.UserId == currentUserId && c.Status != OrderStatus.未完成);


                if (tasktype != -1)
                    query = query.Where(c => c.Status == (OrderStatus)tasktype);

                int totalCount, pageCount;

                var list = dataAccess.LoadPageEntities<OrderInfo, DateTime>(query, pageindex, pagesize, out totalCount, out pageCount, false, u => u.CreateTime).ToList().Select(item => new
                {
                    id = item.Id,
                    appid = item.TaskInfo.AppId,
                    appname = item.TaskInfo.App.Name,
                    keywords = item.TaskInfo.KeyWords,
                    taskid = item.TaskInfo.Id,
                    bundleid = item.TaskInfo.App.Bundleid,
                    position = item.TaskInfo.Position,
                    logo = item.TaskInfo.App.Logo,
                    tasktype = item.TaskInfo.Type.ToString(),
                    idfa = item.IDFA,
                    ipaddress = item.IPAddress,
                    status = item.Status.ToString(),
                    starttime = (item.StartTime == null ? "" : item.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                    endtime = (item.EndTime == null ? "" : item.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                    createtime = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    money = item.Money
                });

                return new
                {
                    status = 0,
                    message = new
                    {
                        totalCount = totalCount,
                        pageCount = pageCount,
                        list = list
                    }
                };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                return new { status = 1, message = ex.Message };
            }
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public dynamic GetPageList(int tasktype, string idfa, int pageindex, int pagesize, int type)
        {
            try
            {
                //Guid currentUserId = Guid.Parse(UserAuth.Current.Id);
                //获取已做过的任务列表
                //var myList = dataAccess.LoadEntities<OrderInfo>(c => c.IDFA == idfa && c.Status != OrderStatus.未完成).Select(c => c.TaskInfoId).ToList();

                //var query = dataAccess.LoadEntities<TaskInfo>(c => c.IsDisabled == false && c.EndTime > DateTime.Now && c.Number > 0 && !myList.Contains(c.Id) && (c.Type == (TaskType)tasktype));

                var query = dataAccess.LoadEntities<TaskInfo>();
                //if (type == 0)//进行中的
                //{
                //    query = query.Where(c => c.StartTime <= DateTime.Now);
                //}
                //else if (type == 1)//未开始的
                //{
                //    query = query.Where(c => c.StartTime > DateTime.Now);
                //}

                int totalCount, pageCount;
                var list = dataAccess.LoadPageEntities<TaskInfo, DateTime>(query, pageindex, pagesize, out totalCount, out pageCount, false, u => u.CreateTime).ToList().Select(item => new
                {
                    id = item.Id,
                    appname = item.App.Name,
                    appid = item.AppId,
                    logo = item.App.Logo,
                    type = item.Type.ToString(),
                    money = item.Money,
                    bundleid = item.App.Bundleid,
                    keywords = item.KeyWords,
                    starttime = item.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    endtime = item.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    number = item.Number,
                    totalnumber = item.InitNumber,
                    position = item.Position,
                    createtime = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    status = item.StartTime <= DateTime.Now ? 0 : 1
                });

                return new
                {
                    mode="nousing bu tracking1",
                    status = 0,
                    message = new
                    {
                        totalCount = totalCount,
                        pageCount = pageCount,
                        list = list
                    }
                };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                return new { status = 1, message = ex.Message };
            }
        }


        public dynamic GetTaskInfo(Guid id)
        {
            try
            {
                var taskInfo = dataAccess.Find<TaskInfo>(id);
                return new
                {
                    taskInfo.App.Name,
                    taskInfo.KeyWords,
                    taskInfo.App.Logo,
                    taskInfo.Position,
                    taskInfo.Type
                };
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                return new { status = 1, message = ex.Message };
            }
        }

    }
}
