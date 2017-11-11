namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountRelation1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.WithdrawRecords", new[] { "Account_Id" });
            DropColumn("dbo.WithdrawRecords", "UserId");
            RenameColumn(table: "dbo.WithdrawRecords", name: "Account_Id", newName: "UserId");
            AlterColumn("dbo.WithdrawRecords", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.WithdrawRecords", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.WithdrawRecords", new[] { "UserId" });
            AlterColumn("dbo.WithdrawRecords", "UserId", c => c.Guid());
            RenameColumn(table: "dbo.WithdrawRecords", name: "UserId", newName: "Account_Id");
            AddColumn("dbo.WithdrawRecords", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.WithdrawRecords", "Account_Id");
        }
    }
}
