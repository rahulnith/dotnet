using Incedo_Octavius_Demo_2.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class ProfileStatusController : Controller
    {
        ProfileStatusModel model = new ProfileStatusModel();
        [ChildActionOnly]
        public ActionResult RenderProfile()
        {
            return PartialView("ProfileIndex");
        }

        // GET: ProfileStatus
        public ActionResult Index()
        {
            List<ProfileStatusModel> profiles = new List<ProfileStatusModel>();
            //KOL_With_Degree_List kolList = new KOL_With_Degree_List();
            string constr = ConfigurationManager.ConnectionStrings["Incedo_Octavius_Demo_2_kol_table_Context"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM octavius.ui_pr_master";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //string name = sdr["first_name"].ToString();

                            profiles.Add(new ProfileStatusModel
                            {
                                ProfileStatusID = Convert.ToInt32(sdr["profile_status_id"]),
                                ProfileStatus = sdr["profile_status"].ToString()
                            });

                        }
                    }
                    con.Close();
                }
            }
            return View(profiles);
        }
    }
}