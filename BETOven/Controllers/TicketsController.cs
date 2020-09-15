using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BETOven.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Json.Net;
using Newtonsoft.Json;

namespace BETOven.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private BetovenDBContext db = new BetovenDBContext();

        // GET: Tickets
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            ICollection<Ticket> list = db.Ticket.Where(x => x.UserID == id).ToList();
            foreach(Ticket ticket in list)
            {
                ticket.CheckTicket();
            }
            db.SaveChanges();
            return View(list);
        }

        public ActionResult Claim(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            string userid = User.Identity.GetUserId();
            if(ticket.TicketStatus == Status.Right && ticket.MoneyClaimed == MoneyClaimed.No)
            {
                Money m = db.Money.Where(x => x.UserID == userid).FirstOrDefault();
                float coef = 1;
                foreach(Bet bet in ticket.BetsOptions)
                {
                    if(bet.Status == Status.Canceled)
                    {
                        continue;
                    }
                    if(bet.Option == "1")
                    {
                        coef *= (bet.Entry.Team1Win);

                    }
                    else if(bet.Option == "x")
                    {
                        coef *= (bet.Entry.Draw);
                    }
                    else if (bet.Option == "2")
                    {
                        coef *= (bet.Entry.Team2Win);
                    }
                }
                m.CurrentAmount += (int)(ticket.BetAmount * coef);
                ticket.MoneyClaimed = MoneyClaimed.Yes;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        class Item
        {
            public int id;
            public string pick;
        }
        

        // GET: Tickets/Create
        public ActionResult Create()
        {
            Ticket t = new Ticket();
            HttpCookieCollection c = Request.Cookies;
            HttpCookie k = c.Get("Ticket");
            if (k != null)
            {
                IList<Item> obj = JsonConvert.DeserializeObject<IList<Item>>(k.Value);
                t.UserID = User.Identity.GetUserId();
                foreach (Item item in obj)
                {
                    Bet bet = new Bet();
                    bet.Entry = db.Bilten.Where(x => x.BiltenEntryID == item.id).FirstOrDefault();
                    bet.Option = item.pick;
                    bet.Status = Status.InProgress;
                    t.BetsOptions.Add(bet);
                }
            }
            return View(t);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,UserID,BetAmount")] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }
            string id = User.Identity.GetUserId();
            if(db.Money.Where(x=>x.UserID == id).FirstOrDefault().CurrentAmount < ticket.BetAmount)
            {
                return View("OutofFunds");
            }

            ticket.UserID = User.Identity.GetUserId();

            HttpCookieCollection col = Request.Cookies;
            HttpCookie k = col.Get("Ticket");
            if (k != null)
            {
                IList<Item> obj = JsonConvert.DeserializeObject<IList<Item>>(k.Value);
                ticket.UserID = User.Identity.GetUserId();
                foreach (Item item in obj)
                {
                    Bet bet = new Bet();
                    bet.Entry = db.Bilten.Where(x => x.BiltenEntryID == item.id).FirstOrDefault();
                    bet.Option = item.pick;
                    bet.Status = Status.InProgress;
                    ticket.BetsOptions.Add(bet);
                }
            }

            foreach (Bet bet in ticket.BetsOptions)
            {
                db.Bets.Add(bet);
            }

            Money mn = db.Money.Where(x => x.UserID == id).FirstOrDefault();
            mn.CurrentAmount -= ticket.BetAmount;
            ticket.TicketStatus = Status.InProgress;
            db.Ticket.Add(ticket);
            db.SaveChanges();
            //DeleteCookie
            HttpCookie c = new HttpCookie("Ticket");
            c.Expires = DateTime.Now.AddSeconds(1);
            Response.Cookies.Set(c);

            return RedirectToAction("Index");
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "TicketID,UserID,BetAmount")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            while(ticket.BetsOptions.Count!=0)
            {
                db.Bets.Remove(ticket.BetsOptions[0]);
            }
            db.Ticket.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
