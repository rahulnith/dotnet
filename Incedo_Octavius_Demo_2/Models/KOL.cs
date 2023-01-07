using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    [Table("KOL")]
    public class KOL
    {
        [Key]
        public byte KolID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public byte Profile_Status { get; set; }
    }
}