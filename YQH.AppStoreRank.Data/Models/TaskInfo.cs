using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YQH.AppStoreRank.Data.Enums;

namespace YQH.AppStoreRank.Data.Models
{
    /// <summary>
    /// 发布的任务信息
    /// </summary>
    public class TaskInfo
    {
        public TaskInfo()
        {
            this.OrderInfos = new HashSet<OrderInfo>();
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// 关联的应用id
        /// </summary>
        [ForeignKey("App")]
        public string AppId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public TaskType Type { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string KeyWords { get; set; }

        /// <summary>
        /// 剩余数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int InitNumber { get; set; }

        /// <summary>
        /// 投放开始（展示）时间
        /// </summary>
        public System.DateTime StartTime { get; set; }

        /// <summary>
        /// 投放结束时间
        /// </summary>
        public System.DateTime EndTime { get; set; }

        /// <summary>
        /// 任务发布时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// appstore搜索显示的位置
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Position { get; set; }


        public bool IsDisabled { get; set; }

        public virtual ApplicationInfo App { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }

    }
}
