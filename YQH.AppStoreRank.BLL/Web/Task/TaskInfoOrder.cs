using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YQH.AppStoreRank.Common;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Web.Task
{
    public abstract class TaskInfoOrder
    {
        protected IBaseRepository dataAccess = RepositoryFactory.GetDataAccess();
        // protected static object obj = new object();

        public static TaskInfoOrder Create(Guid taskId, ClientInfo clientInfo)
        {
            try
            {

                var taskInfo = RepositoryFactory.GetDataAccess().Find<TaskInfo>(taskId);

                switch (taskInfo.Type)
                {
                    case Data.Enums.TaskType.下载激活:
                    case Data.Enums.TaskType.注册:
                        {
                            return new DownloadActiveTaskOrder(taskInfo, clientInfo);
                        }
                    case Data.Enums.TaskType.留存:
                        {
                            return new RetainTaskOrder(taskInfo, clientInfo);
                        }
                    case Data.Enums.TaskType.评论:
                        {
                            return new CommentTaskOrder(taskInfo, clientInfo);
                        }
                    default: return null;
                }

            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(TaskInfoOrder), ex);
                throw;
            }

        }


        public static TaskInfoOrder Get(Guid orderId, ClientInfo clientInfo)
        {
            try
            {

                var orderInfo = RepositoryFactory.GetDataAccess().Find<OrderInfo>(orderId);

                switch (orderInfo.TaskInfo.Type)
                {
                    case Data.Enums.TaskType.下载激活:
                    case Data.Enums.TaskType.注册:
                        {
                            return new DownloadActiveTaskOrder(orderInfo, clientInfo);
                        }
                    case Data.Enums.TaskType.留存:
                        {
                            return new RetainTaskOrder(orderInfo, clientInfo);
                        }
                    case Data.Enums.TaskType.评论:
                        {
                            return new CommentTaskOrder(orderInfo, clientInfo);
                        }
                    default: return null;
                }

            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(typeof(TaskInfoOrder), ex);
                throw;
            }

        }



        public TaskInfo TaskInfo { get; set; }

        public ClientInfo ClientInfo { get; set; }

        public OrderInfo OrderInfo { get; set; }

        public dynamic TimeConfig { get; set; }

        protected TaskInfoOrder(TaskInfo taskInfo, ClientInfo clientInfo)
        {
            this.TaskInfo = taskInfo;
            this.ClientInfo = clientInfo;
            var configPath = HttpContext.Current.Server.MapPath("/lib/config.json");
            TimeConfig = JsonHelper.Deserialize<dynamic>(System.IO.File.ReadAllText(configPath));
        }


        protected TaskInfoOrder(OrderInfo orderInfo, ClientInfo clientInfo)
        {

            this.OrderInfo = orderInfo;
            this.ClientInfo = clientInfo;
            var configPath = HttpContext.Current.Server.MapPath("/lib/config.json");
            TimeConfig = JsonHelper.Deserialize<dynamic>(System.IO.File.ReadAllText(configPath));
        }


        /// <summary>
        /// 获取一个任务
        /// </summary>
        public virtual OrderInfo Get()
        {

            if (this.TaskInfo.IsDisabled)
            {
                throw new ErrorMsgException("该任务已经被删除");
            }
            var now = DateTime.Now;
            if (now < this.TaskInfo.StartTime)
            {
                throw new ErrorMsgException("该任务还没有开始");
            }
            if (now > this.TaskInfo.EndTime)
            {
                throw new ErrorMsgException("该任务已经过期");
            }

            var getTask = dataAccess.First<OrderInfo>(a => (a.IPAddress == this.ClientInfo.IpAddress || a.IDFA == this.ClientInfo.IDFA) && a.TaskInfoId == this.TaskInfo.Id && a.Status != Data.Enums.OrderStatus.未完成);
            if (getTask != null)
            {
                if (getTask.IPAddress == this.ClientInfo.IpAddress)
                {
                    throw new ErrorMsgException("您当前IP已经领取过该任务了");
                }
                if (getTask.IDFA == this.ClientInfo.IDFA)
                {
                    throw new ErrorMsgException("您当前手机已经领取过该任务了");
                }

            }

            var doingTask = dataAccess.First<OrderInfo>(a => (a.IPAddress == this.ClientInfo.IpAddress || a.IDFA == this.ClientInfo.IDFA) && a.Status != Data.Enums.OrderStatus.已完成 && a.Status != Data.Enums.OrderStatus.未完成);
            if (doingTask != null)
            {
                if (doingTask.IPAddress == this.ClientInfo.IpAddress)
                {
                    throw new ErrorMsgException("您当前IP同时只能进行一个任务");
                }
                if (doingTask.IDFA == this.ClientInfo.IDFA)
                {
                    throw new ErrorMsgException("您当前手机同时只能进行一个任务");
                }
            }
            //领取
            //lock (obj)
            //{

            //if (dataAccess.DbContext.Database.ExecuteSqlCommand("update TaskInfoes set Number = Number - 1 where Id = @p0 and Number > 0", this.TaskInfo.Id) != 1)
            //    throw new ErrorMsgException("任务已经被领取完了");

            //this.TaskInfo.Number = this.TaskInfo.Number - 1;
            //if (this.TaskInfo.Number < 0)
            //{
            //    throw new ErrorMsgException("任务已经被领取完了");
            //}

            var orderInfo = new OrderInfo
            {
                Id = Guid.NewGuid(),
                TaskInfoId = this.TaskInfo.Id,
                IDFA = this.ClientInfo.IDFA,
                IPAddress = this.ClientInfo.IpAddress,
                Status = Data.Enums.OrderStatus.已领取,
                CreateTime = DateTime.Now,
                UserId = this.ClientInfo.UserId,
                Money = this.TaskInfo.Money
            };
            //dataAccess.Update(this.TaskInfo);
            dataAccess.Add(orderInfo);
            dataAccess.SaveChanges();

            return orderInfo;
            //}



        }

        public virtual void Begin()
        {
            try
            {
                var orderInfo = this.OrderInfo;

                if (orderInfo == null)
                {
                    throw new ErrorMsgException("没有找到您领取的任务，无法开始");
                }
                if (orderInfo.Status == Data.Enums.OrderStatus.未完成)
                {
                    throw new ErrorMsgException("该任务已经失效，无法开始");
                }
                if (orderInfo.Status != Data.Enums.OrderStatus.已领取)
                {
                    throw new ErrorMsgException("任务状态不正确，无法开始");
                }
                orderInfo.Status = Data.Enums.OrderStatus.进行中;
                orderInfo.StartTime = DateTime.Now;
                dataAccess.Update(orderInfo);
                dataAccess.SaveChanges();
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                throw;
            }

        }

        public virtual OrderInfo Submit(dynamic data)
        {
            var orderInfo = this.OrderInfo;
            if (orderInfo == null)
            {
                throw new ErrorMsgException("没有找到您领取的任务，无法提交");
            }
            if (orderInfo.Status == Data.Enums.OrderStatus.未完成)
            {
                throw new ErrorMsgException("该任务已经失效，请重新领取");
            }

            if (orderInfo.Status != Data.Enums.OrderStatus.进行中)
            {
                throw new ErrorMsgException("任务状态不正确，无法提交");
            }
            if (orderInfo.StartTime == null)
            {
                throw new ErrorMsgException("您还未开始该任务");
            }

            return orderInfo;
        }


        protected virtual void AfterSubmit()
        {

            var orderInfo = this.OrderInfo;
            orderInfo.EndTime = DateTime.Now;
            var user = dataAccess.Find<Account>(orderInfo.UserId);
            user.Amount += orderInfo.Money;
            dataAccess.Update(orderInfo);
            dataAccess.Update(user);
            dataAccess.SaveChanges();
            Common.RedisHelper redis = new Common.RedisHelper();
            redis.Remove(orderInfo.Id.ToString());


        }

        public virtual void GiveUp()
        {
            try
            {
                var orderInfo = this.OrderInfo;
                if (orderInfo.IDFA != this.ClientInfo.IDFA)
                {
                    throw new ErrorMsgException("当前订单不是该手机的");
                }
                if (orderInfo != null)
                {
                    orderInfo.Status = Data.Enums.OrderStatus.未完成;
                }
                var taskInfo = dataAccess.Find<TaskInfo>(orderInfo.TaskInfoId);
                taskInfo.Number++;
                dataAccess.Update(orderInfo);
                dataAccess.Update(taskInfo);
                dataAccess.SaveChanges();
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteLog(this.GetType(), ex);
                throw;
            }
        }
    }
}
