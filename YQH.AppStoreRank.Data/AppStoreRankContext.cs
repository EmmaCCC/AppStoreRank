using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.Data
{
    public class AppStoreRankContext : DbContext
    {
        public AppStoreRankContext()
            : base("name=AppStoreRankContext")
        {

        }
        static AppStoreRankContext()
        {
            Database.SetInitializer<AppStoreRankContext>(null);
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ApplicationInfo> ApplicationInfos { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        public virtual DbSet<TaskInfo> TaskInfos { get; set; }
        public virtual DbSet<WithdrawRecord> WithdrawRecords { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<OperationRecord> OperationRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 禁用一对多级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 禁用多对多级联删除
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

      
    }
}
