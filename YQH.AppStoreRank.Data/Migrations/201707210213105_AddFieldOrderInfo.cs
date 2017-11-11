namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldOrderInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInfoes", "Evidence", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderInfoes", "Evidence");
        }
    }
}
