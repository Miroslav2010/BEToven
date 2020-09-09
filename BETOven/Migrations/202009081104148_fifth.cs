namespace BETOven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BiltenEntries", "Team1Points", c => c.Int(nullable: false));
            AddColumn("dbo.BiltenEntries", "Team2Points", c => c.Int(nullable: false));
            AddColumn("dbo.BiltenEntries", "GameStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BiltenEntries", "GameStatus");
            DropColumn("dbo.BiltenEntries", "Team2Points");
            DropColumn("dbo.BiltenEntries", "Team1Points");
        }
    }
}
