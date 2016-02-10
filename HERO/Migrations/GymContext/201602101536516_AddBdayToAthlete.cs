namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBdayToAthlete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Athletes", "BirthDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Athletes", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Athletes", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Athletes", "BirthDate");
        }
    }
}
