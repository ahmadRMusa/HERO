namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedSingleClass1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SingleClassSetups", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SingleClassSetups", "Time", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SingleClassSetups", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.SingleClassSetups", "Date");
        }
    }
}
