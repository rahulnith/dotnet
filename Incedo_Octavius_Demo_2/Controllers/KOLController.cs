using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Incedo_Octavius_Demo_2.Data;
using Incedo_Octavius_Demo_2.Models;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class KOLController : Controller
    {
        private Incedo_Octavius_Demo_2Context db = new Incedo_Octavius_Demo_2Context();

        // GET: KOLs
        public ActionResult Index()
        {
            return View(db.KOL.ToList());
        }

        // GET: KOLs/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL kOL = db.KOL.Find(id);
            if (kOL == null)
            {
                return HttpNotFound();
            }
            return View(kOL);
        }

        // GET: KOLs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KOLs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KolID,First_Name,Last_Name,Mail,Address,City,State,Zip,Profile_Status")] KOL kOL)
        {
            if (ModelState.IsValid)
            {
                db.KOL.Add(kOL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kOL);
        }

        // GET: KOLs/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL kOL = db.KOL.Find(id);
            if (kOL == null)
            {
                return HttpNotFound();
            }
            return View(kOL);
        }

        // POST: KOLs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KolID,First_Name,Last_Name,Mail,Address,City,State,Zip,Profile_Status")] KOL kOL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kOL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kOL);
        }

        // GET: KOLs/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL kOL = db.KOL.Find(id);
            if (kOL == null)
            {
                return HttpNotFound();
            }
            return View(kOL);
        }

        // POST: KOLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            KOL kOL = db.KOL.Find(id);
            db.KOL.Remove(kOL);
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
