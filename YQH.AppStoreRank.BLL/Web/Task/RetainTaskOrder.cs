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
    /// 留存任务
    /// </summary>
    class RetainTaskOrder : TaskInfoOrder
    {
        public RetainTaskOrder(TaskInfo taskInfo, ClientInfo clientInfo) : base(taskInfo, clientInfo)
        {
        }

        public RetainTaskOrder(OrderInfo orderInfo, ClientInfo clientInfo) : base(orderInfo, clientInfo)
        {
        }

        public override OrderInfo Get()
        {
            var orderInfo = base.Get();
            try
            {
                //设置失效时间
                Common.RedisHelper redis = new Common.RedisHelper();
                string failureTime = Convert.ToString(TimeConfig.retainedFailureTime);
                var now = DateTime.Now;
                var failureDateTime = Convert.ToDateTime(now.AddDays(1).ToString("yyyy-MM-dd " + failureTime));
                redis.SetString(orderInfo.Id.ToString(), "1", failureDateTime - now);
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
                string failureTime = Convert.ToString(TimeConfig.retainedFailureTime);
                var now = DateTime.Now;
              
                var nextDay = Convert.ToDateTime(orderInfo.StartTime.Value.ToString("yyyy-MM-dd")).AddDays(1);

                var failureDateTime = Convert.ToDateTime(nextDay.ToString("yyyy-MM-dd " + failureTime));

                if (now > failureDateTime)
                {
                    throw new ErrorMsgException("该任务太长时间没做，已经失效了");
                }
                //次日0点就算完成
                if (now > nextDay)
                {
                    orderInfo.Status = Data.Enums.OrderStatus.已完成;
                    this.AfterSubmit();
                }
                else
                {
                    throw new ErrorMsgException("需要次日才可提交完成");
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
