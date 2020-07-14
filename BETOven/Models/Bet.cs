using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BETOven.Models
{

    public enum Status
    {
        Right,
        InProgress,
        Wrong
    }
    public class Bet
    {
        [Key]
        public int BetID { get; set; }
        public virtual BiltenEntry Entry { get; set; }
        public string Option { get; set; }
        public Status Status { get; set; }
    }
}