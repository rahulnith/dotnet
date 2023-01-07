using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    [Table("KOL_DATA")]
    public class KOL_DATA
    {
        [Key]
        public int kol_id { get; set; }
        public int mdm_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string mail { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public int profile_status { get; set; }
    }
}