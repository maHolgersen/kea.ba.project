using System.Web;
using System.Web.Mvc;

namespace KEA.Batchalor.Schedule
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
