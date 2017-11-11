namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoneyFieldToOrderInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInfoes", "Money", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderInfoes", "Money");
        }
    }
}
