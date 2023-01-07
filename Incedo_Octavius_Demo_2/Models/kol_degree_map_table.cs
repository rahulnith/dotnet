using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    public class kol_degree_map_table
    {
        [Key]
        public int ID { get; set; }
        public int KOLID { get; set; }
        public int DegreeID { get; set; }
    }
}