namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyForPerformances : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Performances", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.Performances", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Performances", new[] { "Athlete_Id" });
            DropIndex("dbo.Performances", new[] { "Class_Id" });
            RenameColumn(table: "dbo.Performances", name: "Athlete_Id", newName: "AthleteId");
            RenameColumn(table: "dbo.Performances", name: "Class_Id", newName: "ClassId");
            AlterColumn("dbo.Performances", "AthleteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Performances", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Performances", "ClassId");
            CreateIndex("dbo.Performances", "AthleteId");
            AddForeignKey("dbo.Performances", "AthleteId", "dbo.Athletes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Performances", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Performances", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Performances", "AthleteId", "dbo.Athletes");
            DropIndex("dbo.Performances", new[] { "AthleteId" });
            DropIndex("dbo.Performances", new[] { "ClassId" });
            AlterColumn("dbo.Performances", "ClassId", c => c.Int());
            AlterColumn("dbo.Performances", "AthleteId", c => c.Int());
            RenameColumn(table: "dbo.Performances", name: "ClassId", newName: "Class_Id");
            RenameColumn(table: "dbo.Performances", name: "AthleteId", newName: "Athlete_Id");
            CreateIndex("dbo.Performances", "Class_Id");
            CreateIndex("dbo.Performances", "Athlete_Id");
            AddForeignKey("dbo.Performances", "Class_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Performances", "Athlete_Id", "dbo.Athletes", "Id");
        }
    }
}
