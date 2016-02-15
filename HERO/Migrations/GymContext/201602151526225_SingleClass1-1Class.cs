namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SingleClass11Class : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SingleClassSetups", "GeneratedClass_Id", "dbo.Classes");
            RenameColumn(table: "dbo.SingleClassSetups", name: "GeneratedClass_Id", newName: "ClassId");
            RenameIndex(table: "dbo.SingleClassSetups", name: "IX_GeneratedClass_Id", newName: "IX_ClassId");
            DropPrimaryKey("dbo.SingleClassSetups");
            AddPrimaryKey("dbo.SingleClassSetups", "ClassId");
            AddForeignKey("dbo.SingleClassSetups", "ClassId", "dbo.Classes", "Id");
            DropColumn("dbo.SingleClassSetups", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SingleClassSetups", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.SingleClassSetups", "ClassId", "dbo.Classes");
            DropPrimaryKey("dbo.SingleClassSetups");
            AddPrimaryKey("dbo.SingleClassSetups", "Id");
            RenameIndex(table: "dbo.SingleClassSetups", name: "IX_ClassId", newName: "IX_GeneratedClass_Id");
            RenameColumn(table: "dbo.SingleClassSetups", name: "ClassId", newName: "GeneratedClass_Id");
            AddForeignKey("dbo.SingleClassSetups", "GeneratedClass_Id", "dbo.Classes", "Id", cascadeDelete: true);
        }
    }
}
