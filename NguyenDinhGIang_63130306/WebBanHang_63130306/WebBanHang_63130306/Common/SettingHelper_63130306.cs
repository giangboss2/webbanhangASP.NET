using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang_63130306.Models;

namespace WebBanHang_63130306.Common
{
    public class SettingHelper_63130306
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetValue(string key)
        {
            var item = db.SystemSettings.SingleOrDefault(x => x.SettingKey == key);
            if (item != null)
            {
                return item.SettingValue;
            }
            return "";
        }
    }
}