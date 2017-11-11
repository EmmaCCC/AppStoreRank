namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRowVersionAndAddConcurrencyCheck : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
    }
}
