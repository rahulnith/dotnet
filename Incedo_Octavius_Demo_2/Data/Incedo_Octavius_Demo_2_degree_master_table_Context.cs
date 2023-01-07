using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Incedo_Octavius_Demo_2.Data
{
    public class Incedo_Octavius_Demo_2_degree_master_table_Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Incedo_Octavius_Demo_2_degree_master_table_Context() : base("name=Incedo_Octavius_Demo_2_degree_master_table_Context")
        {
        }

        public System.Data.Entity.DbSet<Incedo_Octavius_Demo_2.Models.degree_master_table> degree_master_table { get; set; }
    }
}
