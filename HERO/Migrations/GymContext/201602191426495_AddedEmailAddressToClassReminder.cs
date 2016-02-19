namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailAddressToClassReminder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassReminders", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassReminders", "EmailAddress");
        }
    }
}
