using System.Web;
using System.Web.Mvc;

namespace WebBanHang_63130306
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
