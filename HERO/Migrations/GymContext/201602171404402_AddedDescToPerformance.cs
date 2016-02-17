namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDescToPerformance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Performances", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Performances", "Description");
        }
    }
}
