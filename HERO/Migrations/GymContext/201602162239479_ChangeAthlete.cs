namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAthlete : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Performances", new[] { "Wod_Id" });
            CreateIndex("dbo.Performances", "WOD_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Performances", new[] { "WOD_Id" });
            CreateIndex("dbo.Performances", "Wod_Id");
        }
    }
}
