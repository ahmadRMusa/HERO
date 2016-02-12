namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DaysOfWeekToClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "DayOfWeekModel_Id", "dbo.DayOfWeekModels");
            DropIndex("dbo.Classes", new[] { "DayOfWeekModel_Id" });
            CreateTable(
                "dbo.DayOfWeekModelClasses",
                c => new
                    {
                        DayOfWeekModel_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DayOfWeekModel_Id, t.Class_Id })
                .ForeignKey("dbo.DayOfWeekModels", t => t.DayOfWeekModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.DayOfWeekModel_Id)
                .Index(t => t.Class_Id);
            
            DropColumn("dbo.Classes", "DayOfWeekModel_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "DayOfWeekModel_Id", c => c.Int());
            DropForeignKey("dbo.DayOfWeekModelClasses", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.DayOfWeekModelClasses", "DayOfWeekModel_Id", "dbo.DayOfWeekModels");
            DropIndex("dbo.DayOfWeekModelClasses", new[] { "Class_Id" });
            DropIndex("dbo.DayOfWeekModelClasses", new[] { "DayOfWeekModel_Id" });
            DropTable("dbo.DayOfWeekModelClasses");
            CreateIndex("dbo.Classes", "DayOfWeekModel_Id");
            AddForeignKey("dbo.Classes", "DayOfWeekModel_Id", "dbo.DayOfWeekModels", "Id");
        }
    }
}
