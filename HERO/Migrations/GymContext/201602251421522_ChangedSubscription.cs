namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSubscription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Subscriptions", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "Description", c => c.String(maxLength: 300));
        }
    }
}
