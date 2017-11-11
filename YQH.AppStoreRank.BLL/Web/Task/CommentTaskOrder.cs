using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YQH.AppStoreRank.Common;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Web.Task
{
    /// <summary>
    /// 评论任务
    /// </summary>
    class CommentTaskOrder : TaskInfoOrder
    {
        public CommentTaskOrder(TaskInfo taskInfo, ClientInfo info) : base(taskInfo, info)
        {
        }


        public CommentTaskOrder(OrderInfo orderInfo, ClientInfo info) : base(orderInfo, info)
        {

        }

        public override OrderInfo Get()
        {
            try
            {
                var orderInfo = base.Get();
                //设置失效时间
                Common.RedisHelper redis = new Common.RedisHelper();
                int failureTime = Convert.ToInt32(TimeConfig.commentFailureTime);
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
                int failureTime = Convert.ToInt32(TimeConfig.commentFailureTime);
                if ((now - startTime).TotalMinutes > failureTime)
                {
                    throw new ErrorMsgException("该任务超过" + failureTime + "分钟没完成，已经失效了");
                }
                int commentTime = Convert.ToInt32(TimeConfig.commentTime);
                if ((now - startTime).TotalMinutes >= commentTime)
                {
                    orderInfo.Status = Data.Enums.OrderStatus.已完成;
                    orderInfo.Evidence = JsonHelper.Serialize(data);
                    this.AfterSubmit();
                }
                else
                {
                    throw new ErrorMsgException("使用时间不到" + commentTime + "分钟哦");
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
