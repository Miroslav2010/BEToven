namespace BETOven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BiltenEntries", "MatchStart", c => c.DateTime(nullable: false));
            DropColumn("dbo.BiltenEntries", "ValidUntil");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BiltenEntries", "ValidUntil", c => c.DateTime(nullable: false));
            DropColumn("dbo.BiltenEntries", "MatchStart");
        }
    }
}
