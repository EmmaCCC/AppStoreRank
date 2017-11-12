using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.Data.Models
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class Account
    {
        public Account()
        {
            this.OrderInfos = new HashSet<OrderInfo>();
            this.WithdrawRecords = new HashSet<WithdrawRecord>();
        }
        /// <summary>
        /// 主键id
        /// </summary>
        [Required]
        public System.Guid Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// 提现密码
        /// </summary>
        [MaxLength(50)]
        public string WithdrawPwd { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        [Required]
        public AccountType Type { get; set; }


        /// <summary>
        /// 账户金额
        /// </summary>
        [ConcurrencyCheck]
        public decimal Amount { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }



        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        public virtual ICollection<WithdrawRecord> WithdrawRecords { get; set; }
    }
}
