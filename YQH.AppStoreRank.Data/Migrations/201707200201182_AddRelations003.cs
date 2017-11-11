namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelations003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInfoes", "Account_Id", c => c.Guid());
            CreateIndex("dbo.OrderInfoes", "Account_Id");
            AddForeignKey("dbo.OrderInfoes", "Account_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderInfoes", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.OrderInfoes", new[] { "Account_Id" });
            DropColumn("dbo.OrderInfoes", "Account_Id");
        }
    }
}
