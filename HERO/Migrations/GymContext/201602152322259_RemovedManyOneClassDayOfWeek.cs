namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedManyOneClassDayOfWeek : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DayOfWeekModelClasses", "DayOfWeekModel_Id", "dbo.DayOfWeekModels");
            DropForeignKey("dbo.DayOfWeekModelClasses", "Class_Id", "dbo.Classes");
            DropIndex("dbo.DayOfWeekModelClasses", new[] { "DayOfWeekModel_Id" });
            DropIndex("dbo.DayOfWeekModelClasses", new[] { "Class_Id" });
            DropTable("dbo.DayOfWeekModelClasses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DayOfWeekModelClasses",
                c => new
                    {
                        DayOfWeekModel_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DayOfWeekModel_Id, t.Class_Id });
            
            CreateIndex("dbo.DayOfWeekModelClasses", "Class_Id");
            CreateIndex("dbo.DayOfWeekModelClasses", "DayOfWeekModel_Id");
            AddForeignKey("dbo.DayOfWeekModelClasses", "Class_Id", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DayOfWeekModelClasses", "DayOfWeekModel_Id", "dbo.DayOfWeekModels", "Id", cascadeDelete: true);
        }
    }
}
