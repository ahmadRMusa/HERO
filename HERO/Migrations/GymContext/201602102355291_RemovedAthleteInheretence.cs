namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAthleteInheretence : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Athletes", "Subscription_Id", "dbo.Subscriptions");
            DropIndex("dbo.Athletes", new[] { "Subscription_Id" });
            AddColumn("dbo.Athletes", "Biography", c => c.String(maxLength: 500));
            AddColumn("dbo.Athletes", "VerifiedUser", c => c.Boolean(nullable: false));
            AddColumn("dbo.Athletes", "ApplicationUserId", c => c.String());
            AlterColumn("dbo.Athletes", "Subscription_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Athletes", "Subscription_Id");
            AddForeignKey("dbo.Athletes", "Subscription_Id", "dbo.Subscriptions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Athletes", "Subscription_Id", "dbo.Subscriptions");
            DropIndex("dbo.Athletes", new[] { "Subscription_Id" });
            AlterColumn("dbo.Athletes", "Subscription_Id", c => c.Int());
            DropColumn("dbo.Athletes", "ApplicationUserId");
            DropColumn("dbo.Athletes", "VerifiedUser");
            DropColumn("dbo.Athletes", "Biography");
            CreateIndex("dbo.Athletes", "Subscription_Id");
            AddForeignKey("dbo.Athletes", "Subscription_Id", "dbo.Subscriptions", "Id");
        }
    }
}
