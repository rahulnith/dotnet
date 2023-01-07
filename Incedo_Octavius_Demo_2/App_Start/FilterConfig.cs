using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
