namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1to1ReminderAthlete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "ClassReminders_Id", "dbo.ClassReminders");
            DropIndex("dbo.ClassReminders", new[] { "Athlete_Id" });
            RenameColumn(table: "dbo.ClassReminders", name: "Athlete_Id", newName: "AthleteId");
            RenameColumn(table: "dbo.Classes", name: "ClassReminders_Id", newName: "ClassReminders_AthleteId");
            RenameIndex(table: "dbo.Classes", name: "IX_ClassReminders_Id", newName: "IX_ClassReminders_AthleteId");
            DropPrimaryKey("dbo.ClassReminders");
            AlterColumn("dbo.ClassReminders", "AthleteId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ClassReminders", "AthleteId");
            CreateIndex("dbo.ClassReminders", "AthleteId");
            AddForeignKey("dbo.Classes", "ClassReminders_AthleteId", "dbo.ClassReminders", "AthleteId");
            DropColumn("dbo.ClassReminders", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassReminders", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Classes", "ClassReminders_AthleteId", "dbo.ClassReminders");
            DropIndex("dbo.ClassReminders", new[] { "AthleteId" });
            DropPrimaryKey("dbo.ClassReminders");
            AlterColumn("dbo.ClassReminders", "AthleteId", c => c.Int());
            AddPrimaryKey("dbo.ClassReminders", "Id");
            RenameIndex(table: "dbo.Classes", name: "IX_ClassReminders_AthleteId", newName: "IX_ClassReminders_Id");
            RenameColumn(table: "dbo.Classes", name: "ClassReminders_AthleteId", newName: "ClassReminders_Id");
            RenameColumn(table: "dbo.ClassReminders", name: "AthleteId", newName: "Athlete_Id");
            CreateIndex("dbo.ClassReminders", "Athlete_Id");
            AddForeignKey("dbo.Classes", "ClassReminders_Id", "dbo.ClassReminders", "Id");
        }
    }
}
