using System;
using System.ComponentModel.DataAnnotations.Schema;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.Data.Models
{
    public class WithdrawRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// 关联的工作室用户账号id
        /// </summary>
        [ForeignKey("Account")]
        public System.Guid UserId { get; set; }

        /// <summary>
        /// 提现总金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 提现申请状态
        /// </summary>
        public WithdrawStatus Status { get; set; }


        /// <summary>
        /// 提现申请时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public System.Guid? Operator { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public System.DateTime? OperateTime { get; set; }
        public virtual Account Account { get; set; }
    }
}
