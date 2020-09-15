using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BETOven.Models
{
    public class BetovenDBContext : DbContext
    {
        public DbSet<BiltenEntry> Bilten { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Money> Money { get; set; }
    }
}