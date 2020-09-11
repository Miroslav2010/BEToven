using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using BETOven.Enums;

namespace BETOven.Models
{

    public enum GameStatus
    {
        InProgress,
        Finished
    }
    public class BiltenEntry
    {
        [Key]
        public int BiltenEntryID { get; set; }
        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime MatchStart { get; set; }
        [Display(Name = "Home")]
        public string Team1 { get; set; }
        [Display(Name = "Away")]
        public string Team2 { get; set; }


        [Display(Name = "1")]
        public float Team1Win { get; set; }
        [Display(Name = "x")]
        public float Draw { get; set; }
        [Display(Name = "2")]
        public float Team2Win { get; set; }

        public int Team1Points { get; set; }
        public int Team2Points { get; set; }

        public GameStatus GameStatus { get; set; }
        public SportsEnum Sport { get; set; }


    }
}