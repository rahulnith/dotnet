using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Incedo_Octavius_Demo_2.Data;
using Incedo_Octavius_Demo_2.Models;
using MySql.Data.MySqlClient;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class KOL_DATAController : Controller
    {
        private Incedo_Octavius_Demo_2_KOL_DATA_Context db = new Incedo_Octavius_Demo_2_KOL_DATA_Context();

        // GET: KOL_DATA
        public ActionResult Index()
        {
            List<KOL_DATA> customers = new List<KOL_DATA>();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_KOL_DATA_Context"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM octavius_db.kol_data";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //string name = sdr["first_name"].ToString();
                            
                            customers.Add(new KOL_DATA
                            {
                                kol_id = Convert.ToInt32(sdr["kol_id"]),
                                mdm_id = Convert.ToInt32(sdr["mdm_id"]),
                                first_name = sdr["first_name"].ToString(),
                                last_name = sdr["last_name"].ToString(),
                                address = sdr["address"].ToString(),
                                city = sdr["city"].ToString(),
                                state = sdr["state"].ToString(),
                                zip = Convert.ToInt32(sdr["zip"]),
                                profile_status = Convert.ToInt32(sdr["profile_status"])

                            }) ;
                        }
                    }
                    con.Close();
                }
            }

            return View(customers);
            //return View(db.KOL_DATA.ToList());
        }

        // GET: KOL_DATA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL_DATA kOL_DATA = db.KOL_DATA.Find(id);
            if (kOL_DATA == null)
            {
                return HttpNotFound();
            }
            return View(kOL_DATA);
        }

        // GET: KOL_DATA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KOL_DATA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kol_id,mdm_id,first_name,last_name,mail,address,city,state,zip,profile_status")] KOL_DATA kOL_DATA)
        {
            if (ModelState.IsValid)
            {
                db.KOL_DATA.Add(kOL_DATA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kOL_DATA);
        }

        // GET: KOL_DATA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL_DATA kOL_DATA = db.KOL_DATA.Find(id);
            if (kOL_DATA == null)
            {
                return HttpNotFound();
            }
            return View(kOL_DATA);
        }

        // POST: KOL_DATA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kol_id,mdm_id,first_name,last_name,mail,address,city,state,zip,profile_status")] KOL_DATA kOL_DATA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kOL_DATA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kOL_DATA);
        }

        // GET: KOL_DATA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL_DATA kOL_DATA = db.KOL_DATA.Find(id);
            if (kOL_DATA == null)
            {
                return HttpNotFound();
            }
            return View(kOL_DATA);
        }

        // POST: KOL_DATA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KOL_DATA kOL_DATA = db.KOL_DATA.Find(id);
            db.KOL_DATA.Remove(kOL_DATA);
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
