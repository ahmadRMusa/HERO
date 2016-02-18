namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReminderListToClasses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassAthletes", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.ClassAthletes", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.Athletes", "WeeklyClassSetup_Id", "dbo.WeeklyClassSetups");
            DropIndex("dbo.Athletes", new[] { "WeeklyClassSetup_Id" });
            DropIndex("dbo.ClassAthletes", new[] { "Class_Id" });
            DropIndex("dbo.ClassAthletes", new[] { "Athlete_Id" });
            AddColumn("dbo.Athletes", "Class_Id", c => c.Int());
            AddColumn("dbo.Athletes", "Class_Id1", c => c.Int());
            AddColumn("dbo.Classes", "Athlete_Id", c => c.Int());
            CreateIndex("dbo.Athletes", "Class_Id");
            CreateIndex("dbo.Athletes", "Class_Id1");
            CreateIndex("dbo.Classes", "Athlete_Id");
            AddForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Athletes", "Class_Id1", "dbo.Classes", "Id");
            AddForeignKey("dbo.Classes", "Athlete_Id", "dbo.Athletes", "Id");
            DropColumn("dbo.Athletes", "WeeklyClassSetup_Id");
            DropTable("dbo.ClassAthletes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClassAthletes",
                c => new
                    {
                        Class_Id = c.Int(nullable: false),
                        Athlete_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Class_Id, t.Athlete_Id });
            
            AddColumn("dbo.Athletes", "WeeklyClassSetup_Id", c => c.Int());
            DropForeignKey("dbo.Classes", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.Athletes", "Class_Id1", "dbo.Classes");
            DropForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Classes", new[] { "Athlete_Id" });
            DropIndex("dbo.Athletes", new[] { "Class_Id1" });
            DropIndex("dbo.Athletes", new[] { "Class_Id" });
            DropColumn("dbo.Classes", "Athlete_Id");
            DropColumn("dbo.Athletes", "Class_Id1");
            DropColumn("dbo.Athletes", "Class_Id");
            CreateIndex("dbo.ClassAthletes", "Athlete_Id");
            CreateIndex("dbo.ClassAthletes", "Class_Id");
            CreateIndex("dbo.Athletes", "WeeklyClassSetup_Id");
            AddForeignKey("dbo.Athletes", "WeeklyClassSetup_Id", "dbo.WeeklyClassSetups", "Id");
            AddForeignKey("dbo.ClassAthletes", "Athlete_Id", "dbo.Athletes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassAthletes", "Class_Id", "dbo.Classes", "Id", cascadeDelete: true);
        }
    }
}
