namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOrderAttr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderInfoes", "StartTime", c => c.DateTime());
            AlterColumn("dbo.OrderInfoes", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderInfoes", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrderInfoes", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
