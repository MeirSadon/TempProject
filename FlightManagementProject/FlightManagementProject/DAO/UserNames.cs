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
        static public void AddUserName(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into UserNames Values ('{userName.ToUpper()}')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        static public void RemoveUserName(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Delete From UserNames Where User_Names = {userName.ToUpper()}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
