using System.Web;
using System.Web.Mvc;

namespace KIS.AI.Hackathon.Template
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
