using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2.Models
{
    public class BusinessUserDegreeErrorModel
    {
        [Key]
        public int mdm_id { get; set; }
        public string degree { get; set; }

        [ScaffoldColumn(false)]
        public int? degree_id;

        [ScaffoldColumn(false)]
        public List<SelectListItem> DegreesList { get; set; }
        [ScaffoldColumn(false)]
        public int parent_degree_id { get; set; }
    }
}