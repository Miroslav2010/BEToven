using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BETOven.Models
{
    public enum MoneyClaimed
    {
        Yes,No
    }
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public string UserID { get; set; }
        public virtual List<Bet> BetsOptions { get; set; }
        public int BetAmount { get; set; }
        public Status TicketStatus { get; set; }
        public MoneyClaimed MoneyClaimed { get; set; }
        public Ticket()
        {
            BetsOptions = new List<Bet>();
            MoneyClaimed = MoneyClaimed.Yes;
        }
        public void CheckTicket()
        {
            if(TicketStatus == Status.InProgress)
            {
                bool RightFlag = true;
                bool InProgressFlag = true;
                foreach(Bet bet in BetsOptions)
                {
                    bet.CheckBet();
                    if(bet.Status == Status.InProgress)
                    {
                        InProgressFlag = false;
                    }
                    else if(bet.Status == Status.Wrong)
                    {
                        RightFlag = false;
                    }
                }
                if(InProgressFlag)
                {
                    if(RightFlag)
                    {
                        TicketStatus = Status.Right;
                        MoneyClaimed = MoneyClaimed.No;
                    }
                    else
                    {
                        TicketStatus = Status.Wrong;
                    }
                }
                else
                {
                    TicketStatus = Status.InProgress;
                }
            }
        }
        public override string ToString()
        {
            string a =  $"{TicketID} {UserID} ";
            foreach(Bet b in BetsOptions)
            {
                a = a + $"{b.Entry.Team1} {b.Entry.Team2} {b.Option} {b.Status}";
            }
            return a;
        }
    }
}