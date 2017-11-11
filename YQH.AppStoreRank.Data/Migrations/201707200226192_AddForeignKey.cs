namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OrderInfoes", new[] { "Account_Id" });
            DropColumn("dbo.OrderInfoes", "UserId");
            RenameColumn(table: "dbo.OrderInfoes", name: "Account_Id", newName: "UserId");
            AlterColumn("dbo.OrderInfoes", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.OrderInfoes", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderInfoes", new[] { "UserId" });
            AlterColumn("dbo.OrderInfoes", "UserId", c => c.Guid());
            RenameColumn(table: "dbo.OrderInfoes", name: "UserId", newName: "Account_Id");
            AddColumn("dbo.OrderInfoes", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.OrderInfoes", "Account_Id");
        }
    }
}
