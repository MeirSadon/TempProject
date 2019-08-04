using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    static public class UserNames
    {

        // Add New User Name To Table.
        static public void AddUserName(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into UserNames Values ('{userName}')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Remove Specific UserName From Table.
        static public void RemoveUserName(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete From UserNames Where User_Names = '{userName}'", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
