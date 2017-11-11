using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YQH.AppStoreRank.Data.Models
{
    public class OperationRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// 增加还是减少
        /// </summary>
        public bool IsAdd { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 修改前金额
        /// </summary>
        public decimal BeforeMoney { get; set; }
        /// <summary>
        /// 修改后金额
        /// </summary>
        public decimal AfterMoney { get; set; }

        public Guid UserId { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        public Guid OperUserId { get; set; }

        [MaxLength(50)]
        public string OperUserName { get; set; }


        /// <summary>
        /// 操作时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
    }
}
