using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    public class KOL_With_Degree
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DegreeID { get; set; }
        public string Degree { get; set; }
        public int SpecialtyID { get; set; }
        public string SpecialityName { get; set; }
    }
}