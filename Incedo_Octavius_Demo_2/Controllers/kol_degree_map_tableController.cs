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
    public class kol_degree_map_tableController : Controller
    {
        private Incedo_Octavius_Demo_2_kol_degree_map_table_Context db = new Incedo_Octavius_Demo_2_kol_degree_map_table_Context();

        // GET: kol_degree_map_table
        public ActionResult Index()
        {
            List<kol_degree_map_table> maps = new List<kol_degree_map_table>();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_degree_map_table_Context"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM octavius_db.kol_degree_map_table";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //string name = sdr["first_name"].ToString();

                            maps.Add(new kol_degree_map_table
                            {
                                ID = Convert.ToInt32(sdr["ID"]),
                                KOLID = Convert.ToInt32(sdr["KOLID"]),
                                DegreeID = Convert.ToInt32(sdr["DegreeID"])

                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(maps);
            //return View(db.kol_degree_map_table.ToList());
        }

        // GET: kol_degree_map_table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kol_degree_map_table kol_degree_map_table = db.kol_degree_map_table.Find(id);
            if (kol_degree_map_table == null)
            {
                return HttpNotFound();
            }
            return View(kol_degree_map_table);
        }

        // GET: kol_degree_map_table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kol_degree_map_table/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KOLID,DegreeID")] kol_degree_map_table kol_degree_map_table)
        {
            if (ModelState.IsValid)
            {
                db.kol_degree_map_table.Add(kol_degree_map_table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kol_degree_map_table);
        }

        // GET: kol_degree_map_table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kol_degree_map_table kol_degree_map_table = db.kol_degree_map_table.Find(id);
            if (kol_degree_map_table == null)
            {
                return HttpNotFound();
            }
            return View(kol_degree_map_table);
        }

        // POST: kol_degree_map_table/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KOLID,DegreeID")] kol_degree_map_table kol_degree_map_table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kol_degree_map_table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kol_degree_map_table);
        }

        // GET: kol_degree_map_table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kol_degree_map_table kol_degree_map_table = db.kol_degree_map_table.Find(id);
            if (kol_degree_map_table == null)
            {
                return HttpNotFound();
            }
            return View(kol_degree_map_table);
        }

        // POST: kol_degree_map_table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kol_degree_map_table kol_degree_map_table = db.kol_degree_map_table.Find(id);
            db.kol_degree_map_table.Remove(kol_degree_map_table);
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
