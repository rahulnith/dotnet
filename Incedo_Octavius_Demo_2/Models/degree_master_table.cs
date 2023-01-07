using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    public class degree_master_table
    {
        [Key]
        public int DegreeID { get; set; }
        public string Degree { get; set; }
    }
}