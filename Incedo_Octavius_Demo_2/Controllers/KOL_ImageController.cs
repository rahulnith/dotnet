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
using PagedList.Mvc;
using PagedList;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class KOL_ImageController : Controller
    {
        private Incedo_Octavius_Demo_2_kol_degree_map_table_Context db = new Incedo_Octavius_Demo_2_kol_degree_map_table_Context();
        ProfileStatusModel model = new ProfileStatusModel();
        List<ProfileStatusModel> profiles = new List<ProfileStatusModel>();
        List<KOL_Image> kolList = new List<KOL_Image>();

        int PageSizeKOL = 12;
        int ta_id;
        int user_type;



        int chosenProfileID;
        List<int> KOL_Count = new List<int>();

        [ChildActionOnly]
        public ActionResult RenderProfile()
        {
            return PartialView("ProfileIndex");
        }

        // GET: ProfileStatus
        public ActionResult ProfileIndex()
        {
            Console.WriteLine("Inside profileIndex");
            //KOL_With_Degree_List kolList = new KOL_With_Degree_List();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            GetProfiles();
            GetTA(Convert.ToInt32(Session["ta_id"]));

            for (int i = profiles.Count; i > 0; i--)
            {

                using (MySqlConnection dbConnection = new MySqlConnection(constr))
                {
                    try
                    {
                        dbConnection.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = dbConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ProfileCount";
                        cmd.Parameters.AddWithValue("profile_id", i-1);
                        cmd.Parameters.AddWithValue("TA_ID", Convert.ToInt32(Session["ta_id"]));
                        //cmd.ExecuteReader();

                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                        DataSet dataSetObject = new DataSet();
                        dataAdapter.Fill(dataSetObject);

                        //ViewData[i.ToString()] = Convert.ToInt32(dataSetObject.Tables[0].Rows[0]["kolCount"]);
                        KOL_Count.Add(Convert.ToInt32(dataSetObject.Tables[0].Rows[0]["kolCount"]));
                        //profiles.
                    }
                    catch (Exception Ex)
                    {

                        Console.WriteLine("Error : " + Ex.Message);
                    }

                }
            }
            
            ViewBag.KOLCount = KOL_Count;
            return View(profiles);
        }



        // GET: KOL_Image
        public ActionResult Index(int? i)
        {
            int profile;
            Console.WriteLine("Inside Index GEt");
            if(Session["selectedProfileID"]!=null)
            {
                profile = Convert.ToInt32(Session["selectedProfileID"]);
            }
            else
            {
                profile = 2;
            }
            List<KOL_Image> kolNameImageList = new List<KOL_Image>();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            GetTA(Convert.ToInt32(Session["ta_id"]));
            GetProfiles();
            SetProfileId(profile);
            GetKOLNameImage(profile);
            ViewBag.Profiles = profiles;
            ViewBag.Profile = profiles[chosenProfileID].ProfileStatus;
            ViewBag.ProfileID = profiles[chosenProfileID].ProfileStatusID;
            Session["Profile"] = profiles[chosenProfileID].ProfileStatus;
            //chosenProfileID = profile;
            // Stored Procedures
            /*using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "KOL_Image";
                    cmd.Parameters.AddWithValue("profileStatus",profile);
                    //cmd.ExecuteReader();

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            KOL_Image kolImage= new KOL_Image();
                            kolImage.kolID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["kolID"]);
                            kolImage.First_Name = dataSetObject.Tables[0].Rows[iCout]["First_Name"].ToString();
                            kolImage.Last_Name = dataSetObject.Tables[0].Rows[iCout]["Last_Name"].ToString();
                            kolImage.Image_URL = dataSetObject.Tables[0].Rows[iCout]["Image_Link"].ToString();

                            kolNameImageList.Add(kolImage);
                        }
                    }
                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }*/
            //profiles = ViewBag.Profiles;
            //ViewBag.Profile = profiles[chosenProfileID].ProfileStatus;
            //kolList = kolNameImageList;
            return View(kolList.ToPagedList(i ?? 1, PageSizeKOL));
        }

        

        [HttpPost]
        public ActionResult Index(int profile, int? i)
        {
            Console.WriteLine("Inside index post");
            Session["selectedProfileID"] = profile;
            GetTA(Convert.ToInt32(Session["ta_id"]));
            GetProfiles();
            SetProfileId(profile);
            GetKOLNameImage(profile);
            ViewBag.Profiles = profiles;
            ViewBag.Profile = profiles[chosenProfileID].ProfileStatus;
            ViewBag.ProfileID = profiles[chosenProfileID].ProfileStatusID;
            Session["Profile"] = profiles[chosenProfileID].ProfileStatus;
            //chosenProfileID = profile;
            // Stored Procedures

            //profiles = ViewBag.Profiles;
            //ViewBag.Profile = profiles[chosenProfileID].ProfileStatus;
            //return View(kolNameImageList.Where(x => x.First_Name.StartsWith(search) || search == null).ToList());
            return View(kolList.ToPagedList(i ?? 1, PageSizeKOL));
        }

        [HttpPost]
        public ActionResult Search(string search, int? i)
        {
            GetTA(Convert.ToInt32(Session["ta_id"]));
            SetProfileId(Convert.ToInt32(Session["profileID"]));
            GetKOLNameImage(chosenProfileID);
            List<KOL_Image> matchKOLs = new List<KOL_Image>();
            matchKOLs = kolList.Where(x => x.First_Name.ToLower().StartsWith(search.ToLower()) || search == null).ToList();
            return View(matchKOLs.ToPagedList(i ?? 1, PageSizeKOL));
        }
         

        public void GetKOLNameImage(int profile)
        {
            List<KOL_Image> kolNameImageList = new List<KOL_Image>();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            GetTA(Convert.ToInt32(Session["ta_id"]));
            ta_id = Convert.ToInt32(Session["ta_id"]);
            using (MySqlConnection dbConnection = new MySqlConnection(constr))
            {
                try
                {
                    dbConnection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = dbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "KOL_Name_Image";
                    //cmd.CommandText = "KOL_Image";
                    cmd.Parameters.AddWithValue("profileStatus", profile);
                    cmd.Parameters.AddWithValue("TA_ID", ta_id);

                    //cmd.ExecuteReader();

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataSet dataSetObject = new DataSet();
                    dataAdapter.Fill(dataSetObject);

                    if (dataSetObject.Tables[0].Rows.Count > 0)
                    {
                        for (int iCout = 0; iCout < dataSetObject.Tables[0].Rows.Count; iCout++)
                        {
                            KOL_Image kolImage = new KOL_Image();
                            kolImage.kolID = Convert.ToInt32(dataSetObject.Tables[0].Rows[iCout]["kolID"]);
                            kolImage.First_Name = dataSetObject.Tables[0].Rows[iCout]["First_Name"].ToString();
                            kolImage.Last_Name = dataSetObject.Tables[0].Rows[iCout]["Last_Name"].ToString();
                            kolImage.Image_URL = dataSetObject.Tables[0].Rows[iCout]["Image_Link"].ToString();

                            kolNameImageList.Add(kolImage);
                        }
                    }
                }
                catch (Exception Ex)
                {

                    Console.WriteLine("Error : " + Ex.Message);
                }

            }
            kolList = kolNameImageList;
        }

        public void GetProfiles()
        {
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            List<ProfileStatusModel> innerProfiles = new List<ProfileStatusModel>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM octavius.ui_pr_master order by profile_status_id desc";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //string name = sdr["first_name"].ToString();

                            innerProfiles.Add(new ProfileStatusModel
                            {
                                ProfileStatusID = Convert.ToInt32(sdr["profile_status_id"]),
                                ProfileStatus = sdr["profile_status"].ToString(),

                            });

                        }
                    }
                    con.Close();
                }
            }
            profiles = innerProfiles;
        }

        public void SetProfileId(int id)
        {
            Session["selectedProfileID"] = id;
            if (id==0)
            {
                id = 2;
            }
            else if(id==2)
            {
                id = 0;
            }
            Session["profileID"] = id;
            chosenProfileID = id;
        }

        public void GetTA(int id)
        {
            string ta = "";
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            List<ProfileStatusModel> innerProfiles = new List<ProfileStatusModel>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = $"SELECT ta_name from octavius.ta_master where ta_id = {id}";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ta = sdr["ta_name"].ToString();

                        }
                    }
                    con.Close();
                }
            }
            Session["ta_name"] = ta;
        }
        
    }
}