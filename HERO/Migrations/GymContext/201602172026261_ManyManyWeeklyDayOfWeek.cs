namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyManyWeeklyDayOfWeek : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DayOfWeekModels", "WeeklyClassSetup_Id", "dbo.WeeklyClassSetups");
            DropIndex("dbo.DayOfWeekModels", new[] { "WeeklyClassSetup_Id" });
            CreateTable(
                "dbo.DayOfWeekModelWeeklyClassSetups",
                c => new
                    {
                        DayOfWeekModel_Id = c.Int(nullable: false),
                        WeeklyClassSetup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DayOfWeekModel_Id, t.WeeklyClassSetup_Id })
                .ForeignKey("dbo.DayOfWeekModels", t => t.DayOfWeekModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.WeeklyClassSetups", t => t.WeeklyClassSetup_Id, cascadeDelete: true)
                .Index(t => t.DayOfWeekModel_Id)
                .Index(t => t.WeeklyClassSetup_Id);
            
            DropColumn("dbo.DayOfWeekModels", "WeeklyClassSetup_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DayOfWeekModels", "WeeklyClassSetup_Id", c => c.Int());
            DropForeignKey("dbo.DayOfWeekModelWeeklyClassSetups", "WeeklyClassSetup_Id", "dbo.WeeklyClassSetups");
            DropForeignKey("dbo.DayOfWeekModelWeeklyClassSetups", "DayOfWeekModel_Id", "dbo.DayOfWeekModels");
            DropIndex("dbo.DayOfWeekModelWeeklyClassSetups", new[] { "WeeklyClassSetup_Id" });
            DropIndex("dbo.DayOfWeekModelWeeklyClassSetups", new[] { "DayOfWeekModel_Id" });
            DropTable("dbo.DayOfWeekModelWeeklyClassSetups");
            CreateIndex("dbo.DayOfWeekModels", "WeeklyClassSetup_Id");
            AddForeignKey("dbo.DayOfWeekModels", "WeeklyClassSetup_Id", "dbo.WeeklyClassSetups", "Id");
        }
    }
}
