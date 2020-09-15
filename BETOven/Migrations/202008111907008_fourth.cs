namespace BETOven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BiltenEntries", "Sport", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BiltenEntries", "Sport");
        }
    }
}
