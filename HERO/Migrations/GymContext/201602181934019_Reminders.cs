namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reminders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Athletes", "WeeklyClassSetup_Id", c => c.Int());
            CreateIndex("dbo.Athletes", "WeeklyClassSetup_Id");
            AddForeignKey("dbo.Athletes", "WeeklyClassSetup_Id", "dbo.WeeklyClassSetups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Athletes", "WeeklyClassSetup_Id", "dbo.WeeklyClassSetups");
            DropIndex("dbo.Athletes", new[] { "WeeklyClassSetup_Id" });
            DropColumn("dbo.Athletes", "WeeklyClassSetup_Id");
        }
    }
}
