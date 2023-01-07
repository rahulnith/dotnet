using Incedo_Octavius_Demo_2.Data;
using Incedo_Octavius_Demo_2.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class KOL_DetailsController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
        private Incedo_Octavius_Demo_2_kol_degree_map_table_Context db = new Incedo_Octavius_Demo_2_kol_degree_map_table_Context();
        // GET: KOL_Details
        public ActionResult Index()
        {
            List<KOL_Details> kolDetailsList = new List<KOL_Details>();
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
                    cmd.CommandText = "KOL_Details";

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            KOL_Details KolDetails = new KOL_Details();
                            KolDetails.KolID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["KolID"]);
                            KolDetails.MDMID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["MDMID"]);
                            KolDetails.First_Name = dataSetObject.Tables[0].Rows[iCout]["First_Name"].ToString();
                            KolDetails.Last_Name = dataSetObject.Tables[0].Rows[iCout]["Last_Name"].ToString();
                            KolDetails.Mail = dataSetObject.Tables[0].Rows[iCout]["Mail"].ToString();
                            KolDetails.Address= dataSetObject.Tables[0].Rows[iCout]["Address"].ToString();
                            KolDetails.City = dataSetObject.Tables[0].Rows[iCout]["City"].ToString();
                            KolDetails.State = dataSetObject.Tables[0].Rows[iCout]["State"].ToString();
                            KolDetails.Zip = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["Zip"]);
                            KolDetails.ProfileStatus = dataSetObject.Tables[0].Rows[iCout]["ProfleStatus"].ToString();
                            KolDetails.Degree = dataSetObject.Tables[0].Rows[iCout]["Degree"].ToString();
                            KolDetails.SpecialityName = dataSetObject.Tables[0].Rows[iCout]["SpecialityName"].ToString();
                            KolDetails.Image_Link = dataSetObject.Tables[0].Rows[iCout]["Image_Link"].ToString();
                            KolDetails.ta_name = dataSetObject.Tables[0].Rows[iCout]["ta_name"].ToString();

                        

                            kolDetailsList.Add(KolDetails);
                        }
                    }
                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }

            return View(kolDetailsList);
            //return View();
        }

        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                List<KOL_Details> kolDetailsList = new List<KOL_Details>();
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
                        cmd.CommandText = "KOL_Details";
                        cmd.Parameters.AddWithValue("KOL_ID", id);

                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                        DataSet dataSetObject = new DataSet();
                        dataAdapter.Fill(dataSetObject);

                        if (dataSetObject.Tables[0].Rows.Count > 0)
                        {
                            for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                            {
                                KOL_Details KolDetails = new KOL_Details();
                                KolDetails.KolID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["KolID"]);
                                KolDetails.MDMID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["MDMID"]);
                                KolDetails.First_Name = dataSetObject.Tables[0].Rows[iCout]["First_Name"].ToString();
                                KolDetails.Last_Name = dataSetObject.Tables[0].Rows[iCout]["Last_Name"].ToString();
                                KolDetails.Mail = dataSetObject.Tables[0].Rows[iCout]["Mail"].ToString();
                                KolDetails.Address = dataSetObject.Tables[0].Rows[iCout]["Address"].ToString();
                                KolDetails.City = dataSetObject.Tables[0].Rows[iCout]["City"].ToString();
                                KolDetails.State = dataSetObject.Tables[0].Rows[iCout]["State"].ToString();
                                KolDetails.Zip = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["Zip"]);
                                KolDetails.ProfileStatus = dataSetObject.Tables[0].Rows[iCout]["profile_status"].ToString();
                                KolDetails.Degree = dataSetObject.Tables[0].Rows[iCout]["degree"].ToString();
                                KolDetails.SpecialityName = dataSetObject.Tables[0].Rows[iCout]["speciality"].ToString();
                                KolDetails.Image_Link = dataSetObject.Tables[0].Rows[iCout]["Image_Link"].ToString();
                                KolDetails.ta_name = dataSetObject.Tables[0].Rows[iCout]["ta_name"].ToString();



                                kolDetailsList.Add(KolDetails);
                            }
                        }
                    }
                    catch (Exception Ex)
                    {

                        Console.WriteLine("Error : " + Ex.Message);
                    }

                }

                return View(kolDetailsList);
            }
        }
    }
}