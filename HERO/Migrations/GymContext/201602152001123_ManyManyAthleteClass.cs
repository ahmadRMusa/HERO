namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyManyAthleteClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Athletes", new[] { "Class_Id" });
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
            
            DropColumn("dbo.Athletes", "Class_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Athletes", "Class_Id", c => c.Int());
            DropForeignKey("dbo.ClassAthletes", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.ClassAthletes", "Class_Id", "dbo.Classes");
            DropIndex("dbo.ClassAthletes", new[] { "Athlete_Id" });
            DropIndex("dbo.ClassAthletes", new[] { "Class_Id" });
            DropTable("dbo.ClassAthletes");
            CreateIndex("dbo.Athletes", "Class_Id");
            AddForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
