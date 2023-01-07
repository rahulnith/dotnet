using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2.Models
{
    public class BusinessUserSpecialtyModel
    {
        [Key]
        public int MapID { get; set; }
        public string SpecialtyMap { get; set; }
        [ScaffoldColumn(false)]
        public int Parent_Specialty_ID { get; set; }
        [ScaffoldColumn(false)]
        public int SpecialtyID { get; set; }
        public string SpecialtyMaster { get; set; }
        
        [ScaffoldColumn(false)]
        List<SelectListItem> SpecialtiesList { get; set; }
    }
}