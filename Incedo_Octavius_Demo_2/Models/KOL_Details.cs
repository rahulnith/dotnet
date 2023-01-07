using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    public class KOL_Details
    {
        
        public int KolID { get; set; }
        public int MDMID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string ProfileStatus { get; set; }
        public string Degree { get; set; }
        public string SpecialityName { get; set; }
        public string Image_Link { get; set; }
        public string ta_name { get; set; }
    }
}