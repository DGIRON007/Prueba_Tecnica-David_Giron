using System.Web;
using System.Web.Mvc;

namespace PRUEBATECNICA_DAVID_GIRON
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
