namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSingleClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Athletes", "WeeklyClass_Id", "dbo.WeeklyClasses");
            DropForeignKey("dbo.Athletes", "SingleClass_Id", "dbo.SingleClasses");
            DropIndex("dbo.Athletes", new[] { "WeeklyClass_Id" });
            DropIndex("dbo.Athletes", new[] { "SingleClass_Id" });
            DropColumn("dbo.Athletes", "WeeklyClass_Id");
            DropColumn("dbo.Athletes", "SingleClass_Id");
            DropTable("dbo.SingleClasses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SingleClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.Single(nullable: false),
                        Type = c.String(nullable: false),
                        MaxAttendance = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Athletes", "SingleClass_Id", c => c.Int());
            AddColumn("dbo.Athletes", "WeeklyClass_Id", c => c.Int());
            CreateIndex("dbo.Athletes", "SingleClass_Id");
            CreateIndex("dbo.Athletes", "WeeklyClass_Id");
            AddForeignKey("dbo.Athletes", "SingleClass_Id", "dbo.SingleClasses", "Id");
            AddForeignKey("dbo.Athletes", "WeeklyClass_Id", "dbo.WeeklyClasses", "Id");
        }
    }
}
