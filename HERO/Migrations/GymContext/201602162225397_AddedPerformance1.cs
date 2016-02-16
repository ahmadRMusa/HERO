namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPerformance1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Performances", "Wod_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Performances", "Wod_Id");
            AddForeignKey("dbo.Performances", "Wod_Id", "dbo.WODs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Performances", "Wod_Id", "dbo.WODs");
            DropIndex("dbo.Performances", new[] { "Wod_Id" });
            DropColumn("dbo.Performances", "Wod_Id");
        }
    }
}
