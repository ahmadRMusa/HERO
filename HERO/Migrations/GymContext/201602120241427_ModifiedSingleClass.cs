namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedSingleClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SingleClasses", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SingleClasses", "Type");
        }
    }
}
