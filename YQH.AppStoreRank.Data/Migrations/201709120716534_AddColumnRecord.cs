namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OperationRecords", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OperationRecords", "CreateTime");
        }
    }
}
