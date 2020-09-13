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
        Wrong,
        Canceled
    }
    public class Bet
    {
        [Key]
        public int BetID { get; set; }
        public virtual BiltenEntry Entry { get; set; }
        public string Option { get; set; }
        public Status Status { get; set; }

        public void CheckBet()
        {
            if(Status == Status.InProgress)
            {
                if(Entry.GameStatus == GameStatus.Finished)
                {
                    if((Entry.Team1Points > Entry.Team2Points && Option == "1") ||
                        (Entry.Team1Points == Entry.Team2Points && Option == "x") ||
                         (Entry.Team1Points == Entry.Team2Points && Option == "2"))
                    {
                        Status = Status.Right;
                    }
                    else
                    {
                        Status = Status.Wrong;
                    }
                }
                else if(Entry.GameStatus == GameStatus.Canceled)
                {
                    Status = Status.Canceled;
                }
            }
        }
    }
}