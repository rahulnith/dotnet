using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2.Models
{
    public class BusinessUserDegreeModel
    {
        [Key]
        public int? MapID { get; set; }
        public string Degree_Map { get; set; }

        [ScaffoldColumn(false)]
        public int? Parent_Degree_ID { get; set; }
        
        [ScaffoldColumn(false)]
        public int? DegreeID { get; set; }
        
        public string Degree_Master { get; set; }

        [ScaffoldColumn(false)]
        public List<SelectListItem> DegreesList { get; set; }

    }
}