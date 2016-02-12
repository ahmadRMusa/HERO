namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWeeklyClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayOfWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeeklyClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.Single(nullable: false),
                        MaxAttendance = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        SchedulingRange_Start = c.DateTime(nullable: false),
                        SchedulingRange_End = c.DateTime(nullable: false),
                        TimeOfDay = c.Time(nullable: false, precision: 7),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeeklyClassDayOfWeeks",
                c => new
                    {
                        WeeklyClass_Id = c.Int(nullable: false),
                        DayOfWeek_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WeeklyClass_Id, t.DayOfWeek_Id })
                .ForeignKey("dbo.WeeklyClasses", t => t.WeeklyClass_Id, cascadeDelete: true)
                .ForeignKey("dbo.DayOfWeeks", t => t.DayOfWeek_Id, cascadeDelete: true)
                .Index(t => t.WeeklyClass_Id)
                .Index(t => t.DayOfWeek_Id);
            
            AddColumn("dbo.Athletes", "WeeklyClass_Id", c => c.Int());
            CreateIndex("dbo.Athletes", "WeeklyClass_Id");
            AddForeignKey("dbo.Athletes", "WeeklyClass_Id", "dbo.WeeklyClasses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeeklyClassDayOfWeeks", "DayOfWeek_Id", "dbo.DayOfWeeks");
            DropForeignKey("dbo.WeeklyClassDayOfWeeks", "WeeklyClass_Id", "dbo.WeeklyClasses");
            DropForeignKey("dbo.Athletes", "WeeklyClass_Id", "dbo.WeeklyClasses");
            DropIndex("dbo.WeeklyClassDayOfWeeks", new[] { "DayOfWeek_Id" });
            DropIndex("dbo.WeeklyClassDayOfWeeks", new[] { "WeeklyClass_Id" });
            DropIndex("dbo.Athletes", new[] { "WeeklyClass_Id" });
            DropColumn("dbo.Athletes", "WeeklyClass_Id");
            DropTable("dbo.WeeklyClassDayOfWeeks");
            DropTable("dbo.WeeklyClasses");
            DropTable("dbo.DayOfWeeks");
        }
    }
}
