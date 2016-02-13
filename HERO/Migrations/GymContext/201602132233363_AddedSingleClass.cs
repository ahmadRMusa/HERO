namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSingleClass : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WeeklyClasses", newName: "WeeklyClassSetups");
            RenameColumn(table: "dbo.DayOfWeekModels", name: "WeeklyClass_Id", newName: "WeeklyClassSetup_Id");
            RenameIndex(table: "dbo.DayOfWeekModels", name: "IX_WeeklyClass_Id", newName: "IX_WeeklyClassSetup_Id");
            CreateTable(
                "dbo.SingleClassSetups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Duration = c.Single(nullable: false),
                        Type = c.String(nullable: false),
                        MaxAttendance = c.Int(nullable: false),
                        GeneratedClass_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.GeneratedClass_Id, cascadeDelete: true)
                .Index(t => t.GeneratedClass_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SingleClassSetups", "GeneratedClass_Id", "dbo.Classes");
            DropIndex("dbo.SingleClassSetups", new[] { "GeneratedClass_Id" });
            DropTable("dbo.SingleClassSetups");
            RenameIndex(table: "dbo.DayOfWeekModels", name: "IX_WeeklyClassSetup_Id", newName: "IX_WeeklyClass_Id");
            RenameColumn(table: "dbo.DayOfWeekModels", name: "WeeklyClassSetup_Id", newName: "WeeklyClass_Id");
            RenameTable(name: "dbo.WeeklyClassSetups", newName: "WeeklyClasses");
        }
    }
}
