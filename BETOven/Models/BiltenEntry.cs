using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace BETOven.Models
{
    public class BiltenEntry
    {
        [Key]
        public int BiltenEntryID { get; set; }
        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime MatchStart { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }

        

        public float Team1Win { get; set; }
        public float Draw { get; set; }
        public float Team2Win { get; set; }
    }
}