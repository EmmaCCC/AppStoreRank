namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateTimeFiledToCommentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CreateTime");
        }
    }
}
