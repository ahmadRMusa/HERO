namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPrescribedToPerformance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Performances", "Prescribed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Performances", "Prescribed");
        }
    }
}
