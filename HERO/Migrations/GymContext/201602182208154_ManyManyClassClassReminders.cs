namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyManyClassClassReminders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "ClassReminders_AthleteId", "dbo.ClassReminders");
            DropIndex("dbo.Classes", new[] { "ClassReminders_AthleteId" });
            CreateTable(
                "dbo.ClassRemindersClasses",
                c => new
                    {
                        ClassReminders_AthleteId = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassReminders_AthleteId, t.Class_Id })
                .ForeignKey("dbo.ClassReminders", t => t.ClassReminders_AthleteId, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.ClassReminders_AthleteId)
                .Index(t => t.Class_Id);
            
            DropColumn("dbo.Classes", "ClassReminders_AthleteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "ClassReminders_AthleteId", c => c.Int());
            DropForeignKey("dbo.ClassRemindersClasses", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.ClassRemindersClasses", "ClassReminders_AthleteId", "dbo.ClassReminders");
            DropIndex("dbo.ClassRemindersClasses", new[] { "Class_Id" });
            DropIndex("dbo.ClassRemindersClasses", new[] { "ClassReminders_AthleteId" });
            DropTable("dbo.ClassRemindersClasses");
            CreateIndex("dbo.Classes", "ClassReminders_AthleteId");
            AddForeignKey("dbo.Classes", "ClassReminders_AthleteId", "dbo.ClassReminders", "AthleteId");
        }
    }
}
