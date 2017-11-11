using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YQH.AppStoreRank.Data.Models
{
    /// <summary>
    /// 应用信息
    /// </summary>
    public class ApplicationInfo
    {
        public ApplicationInfo()
        {
            this.TaskInfos = new HashSet<TaskInfo>();
        }

        /// <summary>
        /// appid
        /// </summary>
        [Key]
        [Required]
        [MaxLength(100)]
        public string AppId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Logo { get; set; }

        /// <summary>
        /// 开发商
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Developer { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        /// <summary>
        /// 大小
        /// </summary>  
        public double Size { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Bundleid { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary> 
        [Required]
        public DateTime CreateTime { get; set; }

        public bool IsDisabled { get; set; }

        public virtual ICollection<TaskInfo> TaskInfos { get; set; }
    }
}
