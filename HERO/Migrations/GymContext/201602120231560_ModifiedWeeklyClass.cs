namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedWeeklyClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeeklyClasses", "Type", c => c.String(nullable: false));
            DropColumn("dbo.WeeklyClasses", "SchedulingRange_Start");
            DropColumn("dbo.WeeklyClasses", "SchedulingRange_End");
            DropColumn("dbo.WeeklyClasses", "TimeOfDay");
            DropColumn("dbo.WeeklyClasses", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WeeklyClasses", "Name", c => c.String());
            AddColumn("dbo.WeeklyClasses", "TimeOfDay", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.WeeklyClasses", "SchedulingRange_End", c => c.DateTime(nullable: false));
            AddColumn("dbo.WeeklyClasses", "SchedulingRange_Start", c => c.DateTime(nullable: false));
            DropColumn("dbo.WeeklyClasses", "Type");
        }
    }
}
