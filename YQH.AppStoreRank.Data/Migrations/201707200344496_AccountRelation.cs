namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WithdrawRecords", "Account_Id", c => c.Guid());
            CreateIndex("dbo.WithdrawRecords", "Account_Id");
            AddForeignKey("dbo.WithdrawRecords", "Account_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WithdrawRecords", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.WithdrawRecords", new[] { "Account_Id" });
            DropColumn("dbo.WithdrawRecords", "Account_Id");
        }
    }
}
