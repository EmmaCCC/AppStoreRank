namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditWithdrawAddField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WithdrawRecords", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WithdrawRecords", "Remark");
        }
    }
}
