namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAthleteSignupKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AthleteSignupKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false, maxLength: 50),
                        Athlete_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id, cascadeDelete: true)
                .Index(t => t.Athlete_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AthleteSignupKeys", "Athlete_Id", "dbo.Athletes");
            DropIndex("dbo.AthleteSignupKeys", new[] { "Athlete_Id" });
            DropTable("dbo.AthleteSignupKeys");
        }
    }
}
