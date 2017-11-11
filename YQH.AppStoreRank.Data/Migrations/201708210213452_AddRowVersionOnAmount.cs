namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowVersionOnAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "RowVersion");
        }
    }
}
