namespace BETOven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        BetID = c.Int(nullable: false, identity: true),
                        Option = c.String(),
                        Entry_BiltenEntryID = c.Int(),
                        Ticket_TicketID = c.Int(),
                    })
                .PrimaryKey(t => t.BetID)
                .ForeignKey("dbo.BiltenEntries", t => t.Entry_BiltenEntryID)
                .ForeignKey("dbo.Tickets", t => t.Ticket_TicketID)
                .Index(t => t.Entry_BiltenEntryID)
                .Index(t => t.Ticket_TicketID);
            
            CreateTable(
                "dbo.BiltenEntries",
                c => new
                    {
                        BiltenEntryID = c.Int(nullable: false, identity: true),
                        ValidUntil = c.DateTime(nullable: false),
                        Team1 = c.String(),
                        Team2 = c.String(),
                        Team1Win = c.Single(nullable: false),
                        Draw = c.Single(nullable: false),
                        Team2Win = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.BiltenEntryID);
            
            CreateTable(
                "dbo.Moneys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        CurrentAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        BetAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TicketID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "Ticket_TicketID", "dbo.Tickets");
            DropForeignKey("dbo.Bets", "Entry_BiltenEntryID", "dbo.BiltenEntries");
            DropIndex("dbo.Bets", new[] { "Ticket_TicketID" });
            DropIndex("dbo.Bets", new[] { "Entry_BiltenEntryID" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Moneys");
            DropTable("dbo.BiltenEntries");
            DropTable("dbo.Bets");
        }
    }
}
