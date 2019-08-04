using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    public static class FlyingCenterConfig
    {
        public const string ADMIN_NAME = "ADMIN";
        public const string ADMIN_PASSWORD = "9999";

        public const string CONNECTION_STRING = @"Data Source=MEIR-PC\SQLEXPRESS ;Initial Catalog = FlightManagement; Integrated Security = true";

        public const int TIMEFORTHREADHISTORY = 36000 * 24;

    }
}
