namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OperationRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsAdd = c.Boolean(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 50),
                        OperUserId = c.Guid(nullable: false),
                        OperUserName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TaskInfoes", "InitNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskInfoes", "InitNumber");
            DropTable("dbo.OperationRecords");
        }
    }
}
