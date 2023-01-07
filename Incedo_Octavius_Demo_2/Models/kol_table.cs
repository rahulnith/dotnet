using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    [Table("kol_table")]
    public class kol_table
    {
        [Key]
        public int KolID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int Profile_Status { get; set; }
        public int MDMID { get; set; }
    }
}