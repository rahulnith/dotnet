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
using PagedList.Mvc;
using PagedList;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class BusinessUserSpecialtyModelsController : Controller
    {
        private Incedo_Octavius_Demo_2_BU_Spec_Context db = new Incedo_Octavius_Demo_2_BU_Spec_Context();
        public string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_BU_Spec_Context"].ConnectionString;
        int pageSizeBU = 10;
        // GET: BusinessUserSpecialtyModels
        public ActionResult Index(int ? i)
        {
            List<BusinessUserSpecialtyModel> Spec_BU_List = new List<BusinessUserSpecialtyModel>();
            //string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            // Stored Procedures
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Rules_BU_Spec";

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            BusinessUserSpecialtyModel SpecBU = new BusinessUserSpecialtyModel();
                            SpecBU.MapID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["id"]);
                            SpecBU.SpecialtyMap = dataSetObject.Tables[0].Rows[iCout]["SpecialtyMap"].ToString();
                            SpecBU.Parent_Specialty_ID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["speciality_id_master"]);
                            SpecBU.SpecialtyID= Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["speciality_id"]);
                            SpecBU.SpecialtyMaster = dataSetObject.Tables[0].Rows[iCout]["SpecialtyMaster"].ToString();

                            Spec_BU_List.Add(SpecBU);
                        }
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }
            return View(Spec_BU_List.ToPagedList(i??1, pageSizeBU));
        }

        // GET: BusinessUserSpecialtyModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessUserSpecialtyModel businessUserSpecialtyModel = db.BusinessUserSpecialtyModels.Find(id);
            if (businessUserSpecialtyModel == null)
            {
                return HttpNotFound();
            }
            return View(businessUserSpecialtyModel);
        }

        // GET: BusinessUserSpecialtyModels/Create
        public ActionResult Create()
        {
            List<SpecialtyModel> specialtyList = GetSpecialties();
            ViewBag.SpecialtyListItem = ToSelectList(specialtyList);
            return View();
        }

        // POST: BusinessUserSpecialtyModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MapID,SpecialtyMap,Parent_Specialty_ID,SpecialtyID,SpecialtyMaster")] BusinessUserSpecialtyModel businessUserSpecialtyModel)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection dbConnection = new MySqlConnection(constr))
                {
                    try
                    {
                        dbConnection.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = dbConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Rules_BU_Spec_INSERT";
                        cmd.Parameters.AddWithValue("spec_text", businessUserSpecialtyModel.SpecialtyMap);
                        cmd.Parameters.AddWithValue("spec_id", businessUserSpecialtyModel.Parent_Specialty_ID);
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

                return RedirectToAction("Index");
            }

            return View(businessUserSpecialtyModel);
        }

        // GET: BusinessUserSpecialtyModels/Edit/5
        public ActionResult Edit(int? id)
        {
            List<BusinessUserSpecialtyModel> SpecBU_List = new List<BusinessUserSpecialtyModel>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<SpecialtyModel> specialtyList = GetSpecialties();
            ViewBag.SpecialtyListItem = ToSelectList(specialtyList);

            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Rules_BU_Spec_pm";
                    cmd.Parameters.AddWithValue("id", id);

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            BusinessUserSpecialtyModel SpecBU = new BusinessUserSpecialtyModel();
                            SpecBU.MapID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["id"]);
                            SpecBU.SpecialtyMap = dataSetObject.Tables[0].Rows[iCout]["SpecialtyMap"].ToString();
                            SpecBU.Parent_Specialty_ID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["speciality_id_master"]);
                            SpecBU.SpecialtyID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["speciality_id"]);
                            SpecBU.SpecialtyMaster = dataSetObject.Tables[0].Rows[iCout]["SpecialtyMaster"].ToString();

                            SpecBU_List.Add(SpecBU);
                        }
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }


            return View(SpecBU_List[0]);
        }

        // POST: BusinessUserSpecialtyModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MapID,SpecialtyMap,Parent_Specialty_ID,SpecialtyID,SpecialtyMaster")] BusinessUserSpecialtyModel businessUserSpecialtyModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.map_id = businessUserSpecialtyModel.MapID;
                ViewBag.spec_text = businessUserSpecialtyModel.SpecialtyMap;
                ViewBag.spec_id = businessUserSpecialtyModel.Parent_Specialty_ID;
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
                        cmd.CommandText = "Rules_BU_Spec_UPDATE";
                        cmd.Parameters.AddWithValue("map_id", businessUserSpecialtyModel.MapID);
                        cmd.Parameters.AddWithValue("spec_text", businessUserSpecialtyModel.SpecialtyMap);
                        cmd.Parameters.AddWithValue("spec_id", businessUserSpecialtyModel.Parent_Specialty_ID);

                        cmd.ExecuteNonQuery();
                        dbConnection.Close();


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
            return View(businessUserSpecialtyModel);
        }

        // GET: BusinessUserSpecialtyModels/Delete/5
        public ActionResult Delete(int? id)
        {
            List<BusinessUserSpecialtyModel> SpecBU_List = new List<BusinessUserSpecialtyModel>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Rules_BU_Spec_pm";
                    cmd.Parameters.AddWithValue("q_id", id);

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            BusinessUserSpecialtyModel SpecBU = new BusinessUserSpecialtyModel();
                            SpecBU.MapID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["id"]);
                            SpecBU.SpecialtyMap = dataSetObject.Tables[0].Rows[iCout]["SpecialtyMap"].ToString();
                            SpecBU.Parent_Specialty_ID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["speciality_id_master"]);
                            SpecBU.SpecialtyID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["speciality_id"]);
                            SpecBU.SpecialtyMaster = dataSetObject.Tables[0].Rows[iCout]["SpecialtyMaster"].ToString();

                            SpecBU_List.Add(SpecBU);
                        }
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }


            return View(SpecBU_List[0]);
        }

        // POST: BusinessUserSpecialtyModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Rules_BU_Spec_DELETE";
                    cmd.Parameters.AddWithValue("map_id", id);

                    cmd.ExecuteNonQuery();
                    dbConnection.Close();

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }
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

        private List<SpecialtyModel> GetSpecialties()
        {
            List<SpecialtyModel> items = new List<SpecialtyModel>();
            //string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            // Stored Procedures
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    string query = "Select speciality_id, speciality from ui_sp_master";
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        cmd.Connection = dbConnection;
                        dbConnection.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                items.Add(new SpecialtyModel
                                {
                                    SpecialtyID = Convert.ToInt32(sdr["speciality_id"]),
                                    SpecialtyName = sdr["speciality"].ToString()
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

        private SelectList ToSelectList(List<SpecialtyModel> specialties)
        {
            List<SelectListItem> specialtyList = new List<SelectListItem>();
            foreach (SpecialtyModel spec in specialties)
            {
                specialtyList.Add(new SelectListItem()
                {
                    Text = spec.SpecialtyName,
                    Value = Convert.ToString(spec.SpecialtyID)
                });
            }
            return new SelectList(specialtyList, "Value", "Text");
        }
    }
}
