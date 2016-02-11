namespace HERO.Migrations.ApplicationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAthleteInfoId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AthleteInfoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AthleteInfoId");
        }
    }
}
