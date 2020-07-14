using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BETOven.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public string UserID { get; set; }
        public virtual List<Bet> BetsOptions { get; set; }
        public int BetAmount { get; set; }
    }
}