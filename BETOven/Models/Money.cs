using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BETOven.Models
{
    public class Money
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int CurrentAmount { get; set; }
    }
}