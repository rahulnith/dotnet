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
    public class BusinessUserSpecialityErrorModelsController : Controller
    {
        private Incedo_Octavius_Demo_2_sp_error_Context db = new Incedo_Octavius_Demo_2_sp_error_Context();
        public string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_sp_error_Context"].ConnectionString;

        int PageSizeBU = 10;

        // GET: BusinessUserSpecialityErrorModels
        public ActionResult Index(int? i)
        {
            List<BusinessUserSpecialityErrorModel> UnmappedSpecialityList = new List<BusinessUserSpecialityErrorModel>();
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "rules_bu_error_sp";

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            BusinessUserSpecialityErrorModel unmapped_speciality = new BusinessUserSpecialityErrorModel();
                            unmapped_speciality.mdm_id = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["mdm_id"]);
                            unmapped_speciality.speciality = dataSetObject.Tables[0].Rows[iCout]["speciality"].ToString();

                            UnmappedSpecialityList.Add(unmapped_speciality);
                        }
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }
            return View(UnmappedSpecialityList.ToPagedList(i ?? 1, PageSizeBU));
            //return View(db.BusinessUserDegreeErrorModels.ToList());
        }

        // GET: BusinessUserSpecialityErrorModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessUserSpecialityErrorModel businessUserSpecialityErrorModel = db.BusinessUserSpecialityErrorModels.Find(id);
            if (businessUserSpecialityErrorModel == null)
            {
                return HttpNotFound();
            }
            return View(businessUserSpecialityErrorModel);
        }

        // GET: BusinessUserSpecialityErrorModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessUserSpecialityErrorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mdm_id,speciality,parent_speciality_id")] BusinessUserSpecialityErrorModel businessUserSpecialityErrorModel)
        {
            if (ModelState.IsValid)
            {
                db.BusinessUserSpecialityErrorModels.Add(businessUserSpecialityErrorModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessUserSpecialityErrorModel);
        }

        // GET: BusinessUserSpecialityErrorModels/Edit/5
        public ActionResult Edit(int? id)
        {
            List<BusinessUserSpecialityErrorModel> UnmappedSpecialityList = new List<BusinessUserSpecialityErrorModel>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.msg = id;
            //BusinessUserDegreeModel businessUserDegreeModel = db.BusinessUserDegreeModels.Find(id);
            //BusinessUserDegreeModel model = new BusinessUserDegreeModel();
            //model.DegreesList = PopulateDegrees();
            List<SpecialtyModel> specialitiesList = GetSpecialties();
            ViewBag.DegreeListItem = ToSelectList(specialitiesList);

            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Rules_BU_error_sp_pm";
                    cmd.Parameters.AddWithValue("q_id", id);

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            BusinessUserSpecialityErrorModel unmapped_speciality = new BusinessUserSpecialityErrorModel();
                            unmapped_speciality.mdm_id = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["mdm_id"]);
                            unmapped_speciality.speciality = dataSetObject.Tables[0].Rows[iCout]["speciality"].ToString();

                            UnmappedSpecialityList.Add(unmapped_speciality);
                        }
                    }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }


            return View(UnmappedSpecialityList[0]);
            //return View();
        }

        // POST: BusinessUserSpecialityErrorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mdm_id,speciality,parent_speciality_id")] BusinessUserSpecialityErrorModel businessUserSpecialityErrorModel, FormCollection form)
        {
            List<SpecialtyModel> specialitiesList = GetSpecialties();
            ViewBag.DegreeListItem = ToSelectList(specialitiesList);

            //businessUserSpecialityErrorModel.SpecialitiesList = ToSelectListItem(GetDegrees());

            if (ModelState.IsValid)
            {
                ViewBag.map_id = businessUserSpecialityErrorModel.mdm_id;
                ViewBag.deg_text = businessUserSpecialityErrorModel.speciality;
                ViewBag.deg_id = businessUserSpecialityErrorModel.speciality_id;
                ViewBag.deg_id_form = form["sp_id"];
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
                        cmd.CommandText = "Rules_BU_error_sp_UPDATE";
                        cmd.Parameters.AddWithValue("mdm_id", businessUserSpecialityErrorModel.mdm_id);
                        cmd.Parameters.AddWithValue("p_sp_id", Convert.ToInt32(form["sp_id"]));
                        cmd.Parameters.AddWithValue("sp_text", businessUserSpecialityErrorModel.speciality);

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
            return View(businessUserSpecialityErrorModel);
        }

        // GET: BusinessUserSpecialityErrorModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessUserSpecialityErrorModel businessUserSpecialityErrorModel = db.BusinessUserSpecialityErrorModels.Find(id);
            if (businessUserSpecialityErrorModel == null)
            {
                return HttpNotFound();
            }
            return View(businessUserSpecialityErrorModel);
        }

        // POST: BusinessUserSpecialityErrorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessUserSpecialityErrorModel businessUserSpecialityErrorModel = db.BusinessUserSpecialityErrorModels.Find(id);
            db.BusinessUserSpecialityErrorModels.Remove(businessUserSpecialityErrorModel);
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
