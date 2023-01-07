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
using PagedList;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class BusinessUserDegreeErrorModelsController : Controller
    {
        private Incedo_Octavius_Demo_2_Deg_Err_Context db = new Incedo_Octavius_Demo_2_Deg_Err_Context();
        public string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_Deg_Err_Context"].ConnectionString;

        int PageSizeBU = 10;
        // GET: BusinessUserDegreeErrorModels
        public ActionResult Index(int? i)
        {
            List<BusinessUserDegreeErrorModel> UnmappedDegreeList = new List<BusinessUserDegreeErrorModel>();
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "rules_bu_error_dg";

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            BusinessUserDegreeErrorModel unmapped_degree = new BusinessUserDegreeErrorModel();
                            unmapped_degree.mdm_id= Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["mdm_id"]);
                            unmapped_degree.degree = dataSetObject.Tables[0].Rows[iCout]["degree"].ToString();

                            UnmappedDegreeList.Add(unmapped_degree);
                        }
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }
            return View(UnmappedDegreeList.ToPagedList(i ?? 1, PageSizeBU));
            //return View(db.BusinessUserDegreeErrorModels.ToList());
        }

        // GET: BusinessUserDegreeErrorModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessUserDegreeErrorModel businessUserDegreeErrorModel = db.BusinessUserDegreeErrorModels.Find(id);
            if (businessUserDegreeErrorModel == null)
            {
                return HttpNotFound();
            }
            return View(businessUserDegreeErrorModel);
        }

        // GET: BusinessUserDegreeErrorModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessUserDegreeErrorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mdm_id,degree")] BusinessUserDegreeErrorModel businessUserDegreeErrorModel)
        {
            if (ModelState.IsValid)
            {
                db.BusinessUserDegreeErrorModels.Add(businessUserDegreeErrorModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessUserDegreeErrorModel);
        }

        // GET: BusinessUserDegreeErrorModels/Edit/5
        public ActionResult Edit(int? id)
        {
            List<BusinessUserDegreeErrorModel> UnmappedDegreeList = new List<BusinessUserDegreeErrorModel>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.msg = id;
            //BusinessUserDegreeModel businessUserDegreeModel = db.BusinessUserDegreeModels.Find(id);
            //BusinessUserDegreeModel model = new BusinessUserDegreeModel();
            //model.DegreesList = PopulateDegrees();
            List<DegreeModel> degreeList = GetDegrees();
            ViewBag.DegreeListItem = ToSelectList(degreeList);

            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Rule_BU_error_dg_pm";
                    cmd.Parameters.AddWithValue("q_id", id);

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            BusinessUserDegreeErrorModel unmapped_degree = new BusinessUserDegreeErrorModel();
                            unmapped_degree.mdm_id = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["mdm_id"]);
                            unmapped_degree.degree = dataSetObject.Tables[0].Rows[iCout]["degree"].ToString();

                            UnmappedDegreeList.Add(unmapped_degree);
                        }
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }


            return View(UnmappedDegreeList[0]);
            //return View();
        }

        // POST: BusinessUserDegreeErrorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mdm_id,degree,degree_id")] BusinessUserDegreeErrorModel businessUserDegreeErrorModel, FormCollection form)
        {
            List<DegreeModel> degreeList = GetDegrees();
            ViewBag.DegreeListItem = ToSelectList(degreeList);

            businessUserDegreeErrorModel.DegreesList = ToSelectListItem(GetDegrees());

            if (ModelState.IsValid)
            {
                ViewBag.map_id = businessUserDegreeErrorModel.mdm_id;
                ViewBag.deg_text = businessUserDegreeErrorModel.degree;
                ViewBag.deg_id = businessUserDegreeErrorModel.degree_id;
                ViewBag.deg_id_form = form["degree_id"];
                //var selectedItem = businessUserDegreeErrorModel.DegreesList.Find(p => p.Value == businessUserDegreeErrorModel.degree_id.ToString());

                /*if (businessUserDegreeModel == null)
                {
                    return HttpNotFound();
                }*/
                using (MySqlConnection dbConnection = new MySqlConnection(constr))
                {
                    try
                    {
                        dbConnection.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = dbConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Rules_BU_error_dg_UPDATE";
                        cmd.Parameters.AddWithValue("mdm_id", businessUserDegreeErrorModel.mdm_id);
                        cmd.Parameters.AddWithValue("p_deg_id", Convert.ToInt32(form["degree_id"]));
                        cmd.Parameters.AddWithValue("deg_text", businessUserDegreeErrorModel.degree);

                        cmd.ExecuteNonQuery();
                        dbConnection.Close();

                        /*MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                        DataSet dataSetObject = new DataSet();
                        dataAdapter.Fill(dataSetObject);

                        if (dataSetObject.Tables[0].Rows.Count > 0)
                        {
                            for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                            {
                                BusinessUserDegreeModel RuleDegBU = new BusinessUserDegreeModel();
                                RuleDegBU.MapID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["MapID"]);
                                RuleDegBU.DegreeID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["DegreeID"]);
                                RuleDegBU.Degree_Map = dataSetObject.Tables[0].Rows[iCout]["Degree_Map"].ToString();
                                RuleDegBU.Parent_Degree_ID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["Parent_Degree_ID"]);
                                RuleDegBU.Degree_Master = dataSetObject.Tables[0].Rows[iCout]["Degree_Master"].ToString();

                                RuleDegBU_List.Add(RuleDegBU);
                            }
                        }*/

                    }
                    catch (Exception Ex)
                    {

                        Console.WriteLine("Error : " + Ex.Message);
                    }

                }
                //return View();
                //db.Entry(businessUserDegreeModel).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businessUserDegreeErrorModel);
        }

        // GET: BusinessUserDegreeErrorModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessUserDegreeErrorModel businessUserDegreeErrorModel = db.BusinessUserDegreeErrorModels.Find(id);
            if (businessUserDegreeErrorModel == null)
            {
                return HttpNotFound();
            }
            return View(businessUserDegreeErrorModel);
        }

        // POST: BusinessUserDegreeErrorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessUserDegreeErrorModel businessUserDegreeErrorModel = db.BusinessUserDegreeErrorModels.Find(id);
            db.BusinessUserDegreeErrorModels.Remove(businessUserDegreeErrorModel);
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

        private List<DegreeModel> GetDegrees()
        {
            List<DegreeModel> items = new List<DegreeModel>();
            //string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            // Stored Procedures
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    string query = "Select degree_id, degree from ui_dg_master";
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        cmd.Connection = dbConnection;
                        dbConnection.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                items.Add(new DegreeModel
                                {
                                    DegreeID = Convert.ToInt32(sdr["degree_id"]),
                                    Degree = sdr["degree"].ToString()
                                });

                            }
                        }
                        dbConnection.Close();
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }
            return items;
        }

        private SelectList ToSelectList(List<DegreeModel> degree)
        {
            List<SelectListItem> degreeList = new List<SelectListItem>();
            foreach (DegreeModel deg in degree)
            {
                degreeList.Add(new SelectListItem()
                {
                    Text = deg.Degree,
                    Value = Convert.ToString(deg.DegreeID)
                });
            }
            return new SelectList(degreeList, "Value", "Text");
        }

        private List<SelectListItem> ToSelectListItem(List<DegreeModel> degree)
        {
            List<SelectListItem> degreeList = new List<SelectListItem>();
            foreach (DegreeModel deg in degree)
            {
                degreeList.Add(new SelectListItem()
                {
                    Text = deg.Degree,
                    Value = Convert.ToString(deg.DegreeID)
                });
            }
            return degreeList;
        }
    }
}
