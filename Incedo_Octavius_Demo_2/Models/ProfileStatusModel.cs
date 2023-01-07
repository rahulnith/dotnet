using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Models
{
    public class ProfileStatusModel
    {
        [Key]
        public int ProfileStatusID { get; set; }
        public string ProfileStatus { get; set; }

        public int count { get; set; }
    }
}