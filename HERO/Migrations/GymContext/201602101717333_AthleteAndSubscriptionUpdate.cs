namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AthleteAndSubscriptionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Athletes", "EmailAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Athletes", "EmailAddress");
        }
    }
}
