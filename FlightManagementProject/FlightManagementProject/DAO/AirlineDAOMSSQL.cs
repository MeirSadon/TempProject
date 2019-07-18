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
        public void Add(AirlineCompany t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd1 = new SqlCommand($"Select * from UserNames where User_Names = {t.User_Name.ToUpper()}", conn))
                {
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.Read() == true || t.User_Name.ToUpper() == FlyingCenterConfig.ADMIN_NAME.ToUpper())
                        throw new UserNameIsAlreadyExistException($"Sorry But '{t.User_Name}' Is Already Exist");
                }
                using (SqlCommand cmd2 = new SqlCommand($"Insert Into AirlineCompanies(Airline_Name,User_Name,Password,Country_Code) Values" +
                    $"('{t.Airline_Name}','{t.User_Name}','{t.Password}','{t.Country_Code}')", conn))
                {
                    cmd2.ExecuteNonQuery();
                    UserNames.AddUserName(t.User_Name);
                }
            }
        }

        // Serach AirLine Company By Id.
        public AirlineCompany GetById(int id)
        {
            AirlineCompany airline = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
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
                using (SqlCommand cmd = new SqlCommand($"Select * From AirlineCompanies Where User_Name = {userName}", conn))
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

        // Search All Airline Company.
        public IList<AirlineCompany> GetAll()
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
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
                                Country_Code = (int)reader["Country_Code"],
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
                                Country_Code = (int)reader["Country_Code"],
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
                using (SqlCommand cmd = new SqlCommand($"Delete From AirLineCompanies where Id = {t.Id}"))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd.ExecuteNonQuery();
                        UserNames.RemoveUserName(t.User_Name);
                        return;
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
                using (SqlCommand cmd1 = new SqlCommand($"Select User_Name from AirlineCompanies where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        UserNames.RemoveUserName((string)reader["User_Name"]);
                    }
                }
                using (SqlCommand cmd2 = new SqlCommand($"Update AirLineCompanies Set Airline_Name = '{t.Airline_Name}', User_Name = '{t.User_Name}'," +
                 $"Country_Code = '{t.Country_Code}' Where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd2.ExecuteNonQuery();
                        UserNames.AddUserName(t.User_Name);
                        return;
                    }
                }
            }
            throw new UserNotExistException($"Sorry, But We Don't Found {t.User_Name}.");
        }

        // Change Password Of Current Airline.
        public void ChangePassword(AirlineCompany airline)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Update AirLineCompanies Set Password = '{airline.Password}' Where Id = {airline.Id}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
//update AirlineCompanies set Remaining_Tickets = Remaining_Tickets-1 Where Id = {t.Flight_Id}
