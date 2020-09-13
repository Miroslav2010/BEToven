using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BETOven.Models;

namespace BETOven.Controllers
{
    public class BiltenController : Controller
    {
        private BetovenDBContext db = new BetovenDBContext();

        // GET: Bilten
        public ActionResult Index(string Sport = null)
        {
            if (string.IsNullOrEmpty(Sport))
            {
                return View(db.Bilten.Where(x => x.MatchStart.CompareTo(DateTime.Now) > 0 && x.GameStatus!=GameStatus.Canceled).ToList());
            }
            else
            {
                return View(db.Bilten.Where(x => x.MatchStart.CompareTo(DateTime.Now) > 0 && ((int)x.Sport).ToString() == Sport).ToList());
            }
        }

        public ActionResult SetResults(string Sport = null)
        {
            if (string.IsNullOrEmpty(Sport))
            {
                return View("Results",db.Bilten.Where(x => x.MatchStart.CompareTo(DateTime.Now) < 0 && x.GameStatus != GameStatus.Canceled).ToList());
            }
            else
            {
                return View("Results",db.Bilten.Where(x => x.MatchStart.CompareTo(DateTime.Now) < 0 && ((int)x.Sport).ToString() == Sport).ToList());
            }
        }

        // GET: Bilten/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiltenEntry biltenEntry = db.Bilten.Find(id);
            if (biltenEntry == null)
            {
                return HttpNotFound();
            }
            return View(biltenEntry);
        }

        // GET: Bilten/Create
        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bilten/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "BiltenEntryID,Sport,MatchStart,Team1,Team2,Team1Win,Draw,Team2Win")] BiltenEntry biltenEntry)
        {
            if (ModelState.IsValid)
            {
                biltenEntry.GameStatus = GameStatus.InProgress;
                db.Bilten.Add(biltenEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(biltenEntry);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Result(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiltenEntry biltenEntry = db.Bilten.Find(id);
            if (biltenEntry == null)
            {
                return HttpNotFound();
            }
            return View(biltenEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Result(BiltenEntry biltenEntry)
        {
            if (ModelState.IsValid)
            {
                BiltenEntry be = db.Bilten.Find(biltenEntry.BiltenEntryID);
                if(be == null)
                {
                    return HttpNotFound();
                }
                be.Team1Points = biltenEntry.Team1Points;
                be.Team2Points = biltenEntry.Team2Points;
                be.GameStatus = GameStatus.Finished;
                db.SaveChanges();
                return RedirectToAction("SetResults");
            }
            return View(biltenEntry);
        }

        // GET: Bilten/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiltenEntry biltenEntry = db.Bilten.Find(id);
            if (biltenEntry == null)
            {
                return HttpNotFound();
            }
            return View(biltenEntry);
        }

        // POST: Bilten/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "BiltenEntryID,Sport,MatchStart,Team1,Team2,Team1Win,Draw,Team2Win")] BiltenEntry biltenEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biltenEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(biltenEntry);
        }

        // GET: Bilten/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BiltenEntry biltenEntry = db.Bilten.Find(id);
            if (biltenEntry == null)
            {
                return HttpNotFound();
            }
            return View(biltenEntry);
        }

        // POST: Bilten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BiltenEntry biltenEntry = db.Bilten.Find(id);
            biltenEntry.GameStatus = GameStatus.Canceled;
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
