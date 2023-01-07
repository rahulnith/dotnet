using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2.Models
{
    public class BusinessUserSpecialityErrorModel
    {
        [Key]
        public int mdm_id { get; set; }
        public string speciality { get; set; }

        [ScaffoldColumn(false)]
        public int? speciality_id;

        [ScaffoldColumn(false)]
        public List<SelectListItem> SpecialitiesList { get; set; }
        [ScaffoldColumn(false)]
        public int parent_speciality_id { get; set; }
    }
}