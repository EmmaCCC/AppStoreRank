namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnRecord002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OperationRecords", "BeforeMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OperationRecords", "AfterMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OperationRecords", "AfterMoney");
            DropColumn("dbo.OperationRecords", "BeforeMoney");
        }
    }
}
