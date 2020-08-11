using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MSOFT.Common
{
    public class Common
    {
        public Common()
        {

        }
        public static string TimeZoneId = string.Empty;
        public static string GetConnectionString()
        {
            var keyConnectionString = GetAppsettingbyKey("KeyConnection");
            return ConfigurationManager.ConnectionStrings[keyConnectionString].ConnectionString;
        }

        public static string GetAppsettingbyKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
