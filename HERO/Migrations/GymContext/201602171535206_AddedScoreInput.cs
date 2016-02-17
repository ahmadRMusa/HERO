namespace HERO.Migrations.GymContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScoreInput : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Performances", "ScoreInput", c => c.String());
            AddColumn("dbo.Performances", "ScoreActual", c => c.Double(nullable: false));
            DropColumn("dbo.Performances", "Score");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Performances", "Score", c => c.Double(nullable: false));
            DropColumn("dbo.Performances", "ScoreActual");
            DropColumn("dbo.Performances", "ScoreInput");
        }
    }
}
