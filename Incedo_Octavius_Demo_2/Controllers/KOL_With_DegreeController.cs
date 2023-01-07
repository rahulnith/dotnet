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
    public class KOL_With_DegreeController : Controller
    {
        private Incedo_Octavius_Demo_2_KOL_With_Degree_Context db = new Incedo_Octavius_Demo_2_KOL_With_Degree_Context();

        // GET: KOL_With_Degree
        public ActionResult Index()
        {
            List<KOL_With_Degree> kolDegreeList = new List<KOL_With_Degree>();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_with_degree_Context"].ConnectionString;
            // Stored Procedures
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "KOL_With_Degree";

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if(dataSetObject.Tables[0].Rows.Count>0)
                    {
                        for(int iCout = 0;iCout<dataSetObject.Tables[0].Rows.Count;iCout++)
                        {
                            KOL_With_Degree kolDegree = new KOL_With_Degree();
                            kolDegree.FirstName = dataSetObject.Tables[0].Rows[iCout]["First_Name"].ToString();
                            kolDegree.LastName = dataSetObject.Tables[0].Rows[iCout]["Last_Name"].ToString();
                            kolDegree.DegreeID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["DegreeID"]);
                            kolDegree.Degree = dataSetObject.Tables[0].Rows[iCout]["Degree"].ToString();
                            kolDegree.SpecialtyID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["SpecialtyID"]);
                            kolDegree.SpecialityName = dataSetObject.Tables[0].Rows[iCout]["SpecialityName"].ToString();

                            kolDegreeList.Add(kolDegree);
                        }
                    }
                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }

            return View(kolDegreeList);
            //return View(db.KOL_With_Degree.ToList());
        }

        // GET: KOL_With_Degree/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL_With_Degree kOL_With_Degree = db.KOL_With_Degree.Find(id);
            if (kOL_With_Degree == null)
            {
                return HttpNotFound();
            }
            return View(kOL_With_Degree);
        }

        // GET: KOL_With_Degree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KOL_With_Degree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,DegreeID,Degree,SpecialtyID,SpecialityName")] KOL_With_Degree kOL_With_Degree)
        {
            if (ModelState.IsValid)
            {
                db.KOL_With_Degree.Add(kOL_With_Degree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kOL_With_Degree);
        }

        // GET: KOL_With_Degree/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL_With_Degree kOL_With_Degree = db.KOL_With_Degree.Find(id);
            if (kOL_With_Degree == null)
            {
                return HttpNotFound();
            }
            return View(kOL_With_Degree);
        }

        // POST: KOL_With_Degree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,DegreeID,Degree,SpecialtyID,SpecialityName")] KOL_With_Degree kOL_With_Degree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kOL_With_Degree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kOL_With_Degree);
        }

        // GET: KOL_With_Degree/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KOL_With_Degree kOL_With_Degree = db.KOL_With_Degree.Find(id);
            if (kOL_With_Degree == null)
            {
                return HttpNotFound();
            }
            return View(kOL_With_Degree);
        }

        // POST: KOL_With_Degree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KOL_With_Degree kOL_With_Degree = db.KOL_With_Degree.Find(id);
            db.KOL_With_Degree.Remove(kOL_With_Degree);
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
