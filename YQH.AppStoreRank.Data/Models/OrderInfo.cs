using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.Data.Models
{
    public class OrderInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// 关联的任务id
        /// </summary>
        [ForeignKey("TaskInfo")]
        public System.Guid TaskInfoId { get; set; }


        /// <summary>
        /// 关联的工作室用户账号id
        /// </summary>
        [ForeignKey("Account")]
        public System.Guid UserId { get; set; }

        /// <summary>
        /// IDFA 用户唯一表示
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string IDFA { get; set; }

        /// <summary>
        /// 用户下单IP地址
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string IPAddress { get; set; }



        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal Money { get; set; }


        /// <summary>
        /// 订单完成状态
        /// </summary>
        public OrderStatus Status { get; set; }


        /// <summary>
        /// 任务开始时间
        /// </summary>
        public Nullable<System.DateTime> StartTime { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public Nullable<System.DateTime> EndTime { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 任务完成存储昵称或截图
        /// </summary>
        [MaxLength(200)]
        public string Evidence { get; set; }
        /// <summary>
        /// 任务信息
        /// </summary>
        public virtual TaskInfo TaskInfo { get; set; }
        public virtual Account Account { get; set; }
    }
}
