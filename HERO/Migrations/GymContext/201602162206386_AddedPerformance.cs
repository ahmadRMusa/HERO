namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPerformance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Performances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Double(nullable: false),
                        Athlete_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Athlete_Id)
                .Index(t => t.Class_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Performances", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Performances", "Athlete_Id", "dbo.Athletes");
            DropIndex("dbo.Performances", new[] { "Class_Id" });
            DropIndex("dbo.Performances", new[] { "Athlete_Id" });
            DropTable("dbo.Performances");
        }
    }
}
