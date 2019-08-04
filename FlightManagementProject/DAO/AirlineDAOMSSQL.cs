using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    // Class With All Funtions(Of Airline) For MSSQL Data Base.
    class AirlineDAOMSSQL : IAirlineDAO
    {

        // Add New Airline Company.
        public long Add(AirlineCompany t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd1 = new SqlCommand($"Select * from UserNames where User_Names = '{t.User_Name.ToUpper()}'", conn))
                {
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.Read() == true || t.User_Name.ToUpper() == FlyingCenterConfig.ADMIN_NAME.ToUpper())
                        throw new UserNameIsAlreadyExistException($"Sorry But '{t.User_Name}' Is Already Exist");
                    conn.Close();
                }
                using (SqlCommand cmd2 = new SqlCommand($"Insert Into AirlineCompanies(Airline_Name,User_Name,Password,Country_Code) Values" +
                    $"('{t.Airline_Name}','{t.User_Name}','{t.Password}', {t.Country_Code}); SELECT CAST(scope_identity() AS bigint)", conn))
                {
                    conn.Open();
                    cmd2.Parameters.AddWithValue($"{t.Airline_Name}", "bar");
                    newId = (long)cmd2.ExecuteScalar();
                    UserNames.AddUserName(t.User_Name);
                }
            }
            return newId;
        }

        // Serach AirLine Company By Id.
        public AirlineCompany GetById(int id)
        {
            AirlineCompany airline = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From AirlineCompanies Where Id = {id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            airline = new AirlineCompany
                            {
                                Id = (long)reader["Id"],
                                Airline_Name = (string)reader["Airline_Name"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                                Country_Code = (int)reader["Country_Code"],
                            };
                        }

                    }
                }
                return airline;
            }
        }

        // Search Airline Company By UserName.
        public AirlineCompany GetAirlineByUserame(string userName)
        {
            AirlineCompany airline = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From AirlineCompanies Where User_Name = '{userName}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            airline = new AirlineCompany
                            {
                                Id = (long)reader["Id"],
                                Airline_Name = (string)reader["Airline_Name"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                                Country_Code = (long)reader["Country_Code"],
                            };
                        }

                    }
                }
                return airline;
            }
        }

        // Search All Airline Company.
        public IList<AirlineCompany> GetAll()
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From AirlineCompanies", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            AirlineCompany airline = new AirlineCompany
                            {
                                Id = (long)reader["Id"],
                                Airline_Name = (string)reader["Airline_Name"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                                Country_Code = (long)reader["Country_Code"],
                            };
                            airlineCompanies.Add(airline);
                        }

                    }
                }
                return airlineCompanies;
            }
        }

        // Search All Airline Company By Country Code.
        public IList<AirlineCompany> GetAllAirlinesByCountry(int countryId)
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From AirlineCompanies Where Country_Code = {countryId}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            AirlineCompany airline = new AirlineCompany
                            {
                                Id = (long)reader["Id"],
                                Airline_Name = (string)reader["Airline_Name"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                                Country_Code = (long)reader["Country_Code"],
                            };
                            airlineCompanies.Add(airline);
                        }

                    }
                }
                return airlineCompanies;
            }
        }
        
        // Remove Airline Company.
        public void Remove(AirlineCompany t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from Tickets Where Flight_Id = (select  TOP 1 Id from Flights where AirlineCompany_Id like (select Id from AirlineCompanies where User_Name like '{t.User_Name}')); " +
                    $"Delete from Flights Where AirlineCompany_Id = (select Id from AirlineCompanies where User_Name like '{t.User_Name}');" +
                    $"Delete from AirlineCompanies Where User_Name like '{t.User_Name}'",conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.RecordsAffected > 0)
                        {
                            UserNames.RemoveUserName(t.User_Name);
                            return;
                        }
                    }
                }
            }
            throw new UserNotExistException($"Sorry, But We Don't Found {t.User_Name}.");
        }

        // Update Airline Company (Except Password).
        public void Update(AirlineCompany t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update AirLineCompanies Set Airline_Name = '{t.Airline_Name}'," +
                 $"Country_Code = '{t.Country_Code}' Where Id = {t.Id}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Change Password Of Current Airline.
        public void ChangePassword(AirlineCompany airline)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update AirLineCompanies Set Password = '{airline.Password}' Where Id = {airline.Id}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
