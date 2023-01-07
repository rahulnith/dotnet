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
    public class degree_master_tableController : Controller
    {
        private Incedo_Octavius_Demo_2_degree_master_table_Context db = new Incedo_Octavius_Demo_2_degree_master_table_Context();

        // GET: degree_master_table
        public ActionResult Index()
        {
            List<degree_master_table> degrees = new List<degree_master_table>();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_degree_master_table_Context"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM octavius.ui_dg_master";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //string name = sdr["first_name"].ToString();

                            degrees.Add(new degree_master_table
                            {
                                DegreeID = Convert.ToInt32(sdr["degree_id"]),
                                Degree = sdr["degree"].ToString()

                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(degrees);
            //return View(db.KOL_DATA.ToList());
        }

        // GET: degree_master_table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            degree_master_table degree_master_table = db.degree_master_table.Find(id);
            if (degree_master_table == null)
            {
                return HttpNotFound();
            }
            return View(degree_master_table);
        }

        // GET: degree_master_table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: degree_master_table/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DegreeID,Degree")] degree_master_table degree_master_table)
        {
            if (ModelState.IsValid)
            {
                db.degree_master_table.Add(degree_master_table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(degree_master_table);
        }

        // GET: degree_master_table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            degree_master_table degree_master_table = db.degree_master_table.Find(id);
            if (degree_master_table == null)
            {
                return HttpNotFound();
            }
            return View(degree_master_table);
        }

        // POST: degree_master_table/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DegreeID,Degree")] degree_master_table degree_master_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(degree_master_table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(degree_master_table);
        }

        // GET: degree_master_table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            degree_master_table degree_master_table = db.degree_master_table.Find(id);
            if (degree_master_table == null)
            {
                return HttpNotFound();
            }
            return View(degree_master_table);
        }

        // POST: degree_master_table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            degree_master_table degree_master_table = db.degree_master_table.Find(id);
            db.degree_master_table.Remove(degree_master_table);
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
