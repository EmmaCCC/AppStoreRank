namespace YQH.AppStoreRank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 32),
                        Phone = c.String(maxLength: 20),
                        WithdrawPwd = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationInfoes",
                c => new
                    {
                        AppId = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 200),
                        Logo = c.String(nullable: false, maxLength: 200),
                        Developer = c.String(nullable: false, maxLength: 200),
                        ClassName = c.String(nullable: false, maxLength: 50),
                        Size = c.Double(nullable: false),
                        Bundleid = c.String(nullable: false, maxLength: 100),
                        Status = c.String(nullable: false, maxLength: 10),
                        CreateTime = c.DateTime(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AppId);
            
            CreateTable(
                "dbo.OrderInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskInfoId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        IDFA = c.String(nullable: false, maxLength: 128),
                        IPAddress = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskInfoes", t => t.TaskInfoId)
                .Index(t => t.TaskInfoId);
            
            CreateTable(
                "dbo.TaskInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AppId = c.String(maxLength: 100),
                        Type = c.Int(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KeyWords = c.String(nullable: false, maxLength: 50),
                        Number = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Position = c.String(nullable: false, maxLength: 20),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationInfoes", t => t.AppId)
                .Index(t => t.AppId);
            
            CreateTable(
                "dbo.WithdrawRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Operator = c.Guid(),
                        OperateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderInfoes", "TaskInfoId", "dbo.TaskInfoes");
            DropForeignKey("dbo.TaskInfoes", "AppId", "dbo.ApplicationInfoes");
            DropIndex("dbo.TaskInfoes", new[] { "AppId" });
            DropIndex("dbo.OrderInfoes", new[] { "TaskInfoId" });
            DropTable("dbo.WithdrawRecords");
            DropTable("dbo.TaskInfoes");
            DropTable("dbo.OrderInfoes");
            DropTable("dbo.ApplicationInfoes");
            DropTable("dbo.Accounts");
        }
    }
}
