namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWOD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WODs", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WODs", "Name");
        }
    }
}
