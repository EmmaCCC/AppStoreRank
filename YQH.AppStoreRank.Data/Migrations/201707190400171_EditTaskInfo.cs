namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTaskInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskInfoes", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskInfoes", "CreateTime");
        }
    }
}
