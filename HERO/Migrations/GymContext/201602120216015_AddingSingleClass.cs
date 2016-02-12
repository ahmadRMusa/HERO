namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSingleClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SingleClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.Single(nullable: false),
                        MaxAttendance = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Athletes", "SingleClass_Id", c => c.Int());
            CreateIndex("dbo.Athletes", "SingleClass_Id");
            AddForeignKey("dbo.Athletes", "SingleClass_Id", "dbo.SingleClasses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Athletes", "SingleClass_Id", "dbo.SingleClasses");
            DropIndex("dbo.Athletes", new[] { "SingleClass_Id" });
            DropColumn("dbo.Athletes", "SingleClass_Id");
            DropTable("dbo.SingleClasses");
        }
    }
}
