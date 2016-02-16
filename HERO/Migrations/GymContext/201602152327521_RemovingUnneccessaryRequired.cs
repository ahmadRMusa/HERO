namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingUnneccessaryRequired : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WODs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Scoring = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Classes", "WOD_Id", c => c.Int());
            CreateIndex("dbo.Classes", "WOD_Id");
            AddForeignKey("dbo.Classes", "WOD_Id", "dbo.WODs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "WOD_Id", "dbo.WODs");
            DropIndex("dbo.Classes", new[] { "WOD_Id" });
            DropColumn("dbo.Classes", "WOD_Id");
            DropTable("dbo.WODs");
        }
    }
}
