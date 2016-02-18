namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReminderclass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Athletes", "Class_Id1", "dbo.Classes");
            DropForeignKey("dbo.Classes", "Athlete_Id", "dbo.Athletes");
            DropIndex("dbo.Athletes", new[] { "Class_Id" });
            DropIndex("dbo.Athletes", new[] { "Class_Id1" });
            DropIndex("dbo.Classes", new[] { "Athlete_Id" });
            CreateTable(
                "dbo.ClassReminders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Athlete_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id)
                .Index(t => t.Athlete_Id);
            
            CreateTable(
                "dbo.ClassAthletes",
                c => new
                    {
                        Class_Id = c.Int(nullable: false),
                        Athlete_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Class_Id, t.Athlete_Id })
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id, cascadeDelete: true)
                .Index(t => t.Class_Id)
                .Index(t => t.Athlete_Id);
            
            AddColumn("dbo.Classes", "ClassReminders_Id", c => c.Int());
            CreateIndex("dbo.Classes", "ClassReminders_Id");
            AddForeignKey("dbo.Classes", "ClassReminders_Id", "dbo.ClassReminders", "Id");
            DropColumn("dbo.Athletes", "Class_Id");
            DropColumn("dbo.Athletes", "Class_Id1");
            DropColumn("dbo.Classes", "Athlete_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "Athlete_Id", c => c.Int());
            AddColumn("dbo.Athletes", "Class_Id1", c => c.Int());
            AddColumn("dbo.Athletes", "Class_Id", c => c.Int());
            DropForeignKey("dbo.Classes", "ClassReminders_Id", "dbo.ClassReminders");
            DropForeignKey("dbo.ClassReminders", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.ClassAthletes", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.ClassAthletes", "Class_Id", "dbo.Classes");
            DropIndex("dbo.ClassAthletes", new[] { "Athlete_Id" });
            DropIndex("dbo.ClassAthletes", new[] { "Class_Id" });
            DropIndex("dbo.ClassReminders", new[] { "Athlete_Id" });
            DropIndex("dbo.Classes", new[] { "ClassReminders_Id" });
            DropColumn("dbo.Classes", "ClassReminders_Id");
            DropTable("dbo.ClassAthletes");
            DropTable("dbo.ClassReminders");
            CreateIndex("dbo.Classes", "Athlete_Id");
            CreateIndex("dbo.Athletes", "Class_Id1");
            CreateIndex("dbo.Athletes", "Class_Id");
            AddForeignKey("dbo.Classes", "Athlete_Id", "dbo.Athletes", "Id");
            AddForeignKey("dbo.Athletes", "Class_Id1", "dbo.Classes", "Id");
            AddForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
