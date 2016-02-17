namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequireds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Performances", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.Performances", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Performances", "WOD_Id", "dbo.WODs");
            DropIndex("dbo.Performances", new[] { "Athlete_Id" });
            DropIndex("dbo.Performances", new[] { "Class_Id" });
            DropIndex("dbo.Performances", new[] { "WOD_Id" });
            AlterColumn("dbo.Performances", "Athlete_Id", c => c.Int());
            AlterColumn("dbo.Performances", "Class_Id", c => c.Int());
            AlterColumn("dbo.Performances", "WOD_Id", c => c.Int());
            CreateIndex("dbo.Performances", "Athlete_Id");
            CreateIndex("dbo.Performances", "Class_Id");
            CreateIndex("dbo.Performances", "WOD_Id");
            AddForeignKey("dbo.Performances", "Athlete_Id", "dbo.Athletes", "Id");
            AddForeignKey("dbo.Performances", "Class_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Performances", "WOD_Id", "dbo.WODs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Performances", "WOD_Id", "dbo.WODs");
            DropForeignKey("dbo.Performances", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Performances", "Athlete_Id", "dbo.Athletes");
            DropIndex("dbo.Performances", new[] { "WOD_Id" });
            DropIndex("dbo.Performances", new[] { "Class_Id" });
            DropIndex("dbo.Performances", new[] { "Athlete_Id" });
            AlterColumn("dbo.Performances", "WOD_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Performances", "Class_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Performances", "Athlete_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Performances", "WOD_Id");
            CreateIndex("dbo.Performances", "Class_Id");
            CreateIndex("dbo.Performances", "Athlete_Id");
            AddForeignKey("dbo.Performances", "WOD_Id", "dbo.WODs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Performances", "Class_Id", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Performances", "Athlete_Id", "dbo.Athletes", "Id", cascadeDelete: true);
        }
    }
}
