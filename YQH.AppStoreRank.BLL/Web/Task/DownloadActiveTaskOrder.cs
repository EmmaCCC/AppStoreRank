using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Web.Task
{
    /// <summary>
    /// 激活下载任务
    /// </summary>
    class DownloadActiveTaskOrder : TaskInfoOrder
    {
        public DownloadActiveTaskOrder(TaskInfo taskInfo, ClientInfo clientInfo) : base(taskInfo, clientInfo)
        {
        }

        public DownloadActiveTaskOrder(OrderInfo orderInfo, ClientInfo clientInfo) : base(orderInfo, clientInfo)
        {
        }

        public override OrderInfo Get()
        {
            try
            {
                var orderInfo = base.Get();
                //设置失效时间
                Common.RedisHelper redis = new Common.RedisHelper();
                int failureTime = Convert.ToInt32(TimeConfig.activateFailureTime);
                redis.SetString(orderInfo.Id.ToString(), "1", new TimeSpan(0, failureTime, 0));
                return orderInfo;
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                throw;
            }
        }

        public override OrderInfo Submit(dynamic data)
        {
            try
            {
                var orderInfo = base.Submit((ExpandoObject)data);
                var now = DateTime.Now;
                if (orderInfo.StartTime == null)
                {
                    throw new ErrorMsgException("该任务尚未开始");
                }
                var startTime = orderInfo.StartTime.Value;
                int failureTime = Convert.ToInt32(TimeConfig.activateFailureTime);
                if ((now - startTime).TotalMinutes > failureTime)
                {
                    throw new ErrorMsgException("该任务超过" + failureTime + "分钟没完成，已经失效了");
                }
                int activeTime = Convert.ToInt32(TimeConfig.activateTime);
                if ((now - startTime).TotalMinutes >= activeTime)
                {
                    orderInfo.Status = Data.Enums.OrderStatus.已完成;
                    this.AfterSubmit();
                }
                else
                {
                    throw new ErrorMsgException("使用时间不到" + activeTime + "分钟哦");
                }

                return orderInfo;
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                throw;
            }
        }
    }
}
