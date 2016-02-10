namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubscriptionLengthToAthlete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Athletes", "SubscriptionLength", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Athletes", "SubscriptionLength");
        }
    }
}
