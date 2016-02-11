namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        MaxAttendance = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Athletes", "Class_Id", c => c.Int());
            CreateIndex("dbo.Athletes", "Class_Id");
            AddForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Athletes", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Athletes", new[] { "Class_Id" });
            DropColumn("dbo.Athletes", "Class_Id");
            DropTable("dbo.Classes");
        }
    }
}
