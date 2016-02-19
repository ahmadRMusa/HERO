namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailAddressToClassReminder1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClassReminders", "EmailAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassReminders", "EmailAddress", c => c.String());
        }
    }
}
