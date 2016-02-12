namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastMigrationBeforeCreateTest : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DayOfWeeks", newName: "DayOfWeekModels");
            DropForeignKey("dbo.WeeklyClassDayOfWeeks", "WeeklyClass_Id", "dbo.WeeklyClasses");
            DropForeignKey("dbo.WeeklyClassDayOfWeeks", "DayOfWeek_Id", "dbo.DayOfWeeks");
            DropIndex("dbo.WeeklyClassDayOfWeeks", new[] { "WeeklyClass_Id" });
            DropIndex("dbo.WeeklyClassDayOfWeeks", new[] { "DayOfWeek_Id" });
            AddColumn("dbo.Classes", "DayOfWeekModel_Id", c => c.Int());
            AddColumn("dbo.Classes", "WeeklyClass_Id", c => c.Int());
            AddColumn("dbo.DayOfWeekModels", "WeeklyClass_Id", c => c.Int());
            CreateIndex("dbo.Classes", "DayOfWeekModel_Id");
            CreateIndex("dbo.Classes", "WeeklyClass_Id");
            CreateIndex("dbo.DayOfWeekModels", "WeeklyClass_Id");
            AddForeignKey("dbo.Classes", "DayOfWeekModel_Id", "dbo.DayOfWeekModels", "Id");
            AddForeignKey("dbo.DayOfWeekModels", "WeeklyClass_Id", "dbo.WeeklyClasses", "Id");
            AddForeignKey("dbo.Classes", "WeeklyClass_Id", "dbo.WeeklyClasses", "Id");
            DropTable("dbo.WeeklyClassDayOfWeeks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WeeklyClassDayOfWeeks",
                c => new
                    {
                        WeeklyClass_Id = c.Int(nullable: false),
                        DayOfWeek_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WeeklyClass_Id, t.DayOfWeek_Id });
            
            DropForeignKey("dbo.Classes", "WeeklyClass_Id", "dbo.WeeklyClasses");
            DropForeignKey("dbo.DayOfWeekModels", "WeeklyClass_Id", "dbo.WeeklyClasses");
            DropForeignKey("dbo.Classes", "DayOfWeekModel_Id", "dbo.DayOfWeekModels");
            DropIndex("dbo.DayOfWeekModels", new[] { "WeeklyClass_Id" });
            DropIndex("dbo.Classes", new[] { "WeeklyClass_Id" });
            DropIndex("dbo.Classes", new[] { "DayOfWeekModel_Id" });
            DropColumn("dbo.DayOfWeekModels", "WeeklyClass_Id");
            DropColumn("dbo.Classes", "WeeklyClass_Id");
            DropColumn("dbo.Classes", "DayOfWeekModel_Id");
            CreateIndex("dbo.WeeklyClassDayOfWeeks", "DayOfWeek_Id");
            CreateIndex("dbo.WeeklyClassDayOfWeeks", "WeeklyClass_Id");
            AddForeignKey("dbo.WeeklyClassDayOfWeeks", "DayOfWeek_Id", "dbo.DayOfWeeks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WeeklyClassDayOfWeeks", "WeeklyClass_Id", "dbo.WeeklyClasses", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.DayOfWeekModels", newName: "DayOfWeeks");
        }
    }
}
