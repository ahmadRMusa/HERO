namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescToSubscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "Description", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "Description");
        }
    }
}
