using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    // Class With All Funtions(Of Administrator) For MSSQL Data Base.
    class AdministratorDAOMSSQL : IAdministratorDAO
    {

        // Add New Admin.
        public void Add(Administrator t)
        {
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
                using (SqlCommand cmd2 = new SqlCommand($"Insert Into Administrators(User_Name,Password) Values('{t.User_Name}','{t.Password}')", conn))
                {
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    UserNames.AddUserName(t.User_Name);
                }
            }
        }

        // Search Admin By Id.
        public Administrator GetById(int id)
        {
            Administrator administrator = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From Administrators Where Id = {id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            administrator = new Administrator
                            {
                                Id = (long)reader["Id"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                            };
                        }

                    }
                }
                return administrator;
            }
        }

        // Search Airline Company By Id.
        public AirlineCompany GetAirlineById(int id)
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

        // Search Airline Compnay By User Name
        public AirlineCompany GetAirlineByUserName(string userName)
        {
            AirlineCompany airline = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
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

        // Search All Administrators.
        public IList<Administrator> GetAll()
        {
            List<Administrator> adminisitrators = new List<Administrator>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From Administrators", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            Administrator admin = new Administrator
                            {
                                Id = (long)reader["Id"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                            };
                            adminisitrators.Add(admin);
                        }

                    }
                }
                return adminisitrators;
            }
        }

        // Show Me The All Actions That Happened In The Project.
        public string GetAllActionHistory()
        {
            throw new NotImplementedException();
        }

        // Search All Airline Company By Country.
        public IList<AirlineCompany> GetAllAirlineByCountry(int countryId)
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From AirlineCompanies Where Country_Code = {countryId}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
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

        // Search All Customer By Card Number.
        public IList<Customer> GetAllCustomerByCardNumber(int cardNumber)
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Customers where Credit_Card_Number = '{cardNumber}' ", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        customers.Add(new Customer
                        {
                            Id = (long)reader["Id"],
                            First_Name = (string)reader["First_Name"],
                            Last_Name = (string)reader["Last_Name"],
                            User_Name = (string)reader["User_Name"],
                            Password = (string)reader["Password"],
                            Address = (string)reader["Address"],
                            Credit_Card_Number = (string)reader["Credit_Card_Number"],
                        });
                    }
                }
            }
            return customers;
        }

        // Search All Customer By Address.
        public IList<Customer> GetAllCustomerByAddress(string Address)
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from customers where Address = '{Address}' ",conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        customers.Add(new Customer
                        {
                            Id = (long)reader["Id"],
                            First_Name = (string)reader["First_Name"],
                            Last_Name = (string)reader["Last_Name"],
                            User_Name = (string)reader["User_Name"],
                            Password = (string)reader["Password"],
                            Address = (string)reader["Address"],
                            Credit_Card_Number = (string)reader["Credit_Card_Number"],
                        });
                    }
                }
            }
            return customers;
        }

        // Search Customer By Id.
        public Customer GetCustomerById(int id)
        {
            Customer customer = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From Customers Where Id = {id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            customer = new Customer
                            {
                                Id = (long)reader["Id"],
                                First_Name = (string)reader["First_Name"],
                                Last_Name = (string)reader["Last_Name"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                                Address = (string)reader["Address"],
                                Credit_Card_Number = (string)reader["Credit_Card_Number"]
                            };
                        }

                    }
                }
                return customer;
            }
        }
        
        // Search Customer By User Name.
        public Customer GetCustomerByUserName(string userName)
        {
            Customer customer = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From Customers Where User_Name = {userName}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            customer = new Customer
                            {
                                Id = (long)reader["Id"],
                                First_Name = (string)reader["First_Name"],
                                Last_Name = (string)reader["Last_Name"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                                Address = (string)reader["Address"],
                                Credit_Card_Number = (string)reader["Credit_Card_Number"]
                            };
                        }

                    }
                }
                return customer;
            }
        }

        // Remove Admin
        public void Remove(Administrator t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete From Administrators where User_Name = {t.User_Name}",conn))
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

        // Update Current Admin Wwithout Password.
        public void Update(Administrator t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd1 = new SqlCommand($"Select User_Name from Administrators where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        UserNames.RemoveUserName((string)reader["User_Name"]);
                    }
                }
                using (SqlCommand cmd2 = new SqlCommand($"Update Administrators Set User_Name = '{t.User_Name}' Where Id = {t.Id}", conn))
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

        // Search All Airline Companies.
        public IList<AirlineCompany> GetAllAirelineCompanies()
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From AirlineCompanies", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
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

        //Search All Customers.
        public IList<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from customers",conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        customers.Add(new Customer
                        {
                            Id = (long)reader["Id"],
                            First_Name = (string)reader["First_Name"],
                            Last_Name = (string)reader["Last_Name"],
                            User_Name = (string)reader["User_Name"],
                            Password = (string)reader["Password"],
                            Address = (string)reader["Address"],
                            Credit_Card_Number = (string)reader["Credit_Card_Number"],
                        });
                    }
                }
            }
            return customers;
        }

        // Change Password Of Current Administrator.
        public void ChangePassword(Administrator administrator)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update AirLineCompanies Set Password = '{administrator.Password}' Where Id = {administrator.Id}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Search Administrator By UserName.
        public Administrator GetByUserName(string userName)
        {
            Administrator admin = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From Customers Where User_Name = {userName}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            admin = new Administrator
                            {
                                Id = (long)reader["Id"],
                                User_Name = (string)reader["User_Name"],
                                Password = (string)reader["Password"],
                            };
                        }

                    }
                }
                return admin;
            }
        }
    }
}