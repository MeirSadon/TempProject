using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    // Class With All Funtions(Of Customer) For MSSQL Data Base.
    class CustomerDAOMSSQL : ICustomerDAO
    {

        // Add New Customer.
        public long Add(Customer t)
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
                using (SqlCommand cmd2 = new SqlCommand($"Insert Into Customers(First_Name, Last_Name, User_Name, Password, Address, Phone_No, Credit_Card_Number) Values" +
                    $"('{t.First_Name}','{t.Last_Name}','{t.User_Name}','{t.Password}','{t.Address}', '{t.Phone_No}','{t.Credit_Card_Number}'); SELECT CAST(scope_identity() AS bigint)", conn))
                {
                    conn.Open();
                    cmd2.Parameters.AddWithValue($"{t.First_Name}", "bar");
                    newId = (long)cmd2.ExecuteScalar();
                    UserNames.AddUserName(t.User_Name);
                }
            }
            return newId;
        }

        //Get Customer By Id.
        public Customer GetById(int id)
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
            }  return customer;
        }

        // Get All Customers.
        public IList<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from customers", conn))
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

        //Search Customer By User Name.
        public Customer GetCustomerByUserName(string userName)
        {
            Customer customer = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
           {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From Customers Where User_Name = '{userName}'", conn))
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

        //Remove Customer.
        public void Remove(Customer t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from Tickets Where Customer_Id = (select Top 1 Id from Customers where User_Name like '{t.User_Name}');" +
                    $"Delete From Customers where User_Name like '{t.User_Name}'", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.RecordsAffected > 0)
                    {
                        UserNames.RemoveUserName(t.User_Name);
                        return;
                    }
                }
            }
            throw new UserNotExistException($"Sorry, But We Don't Found {t.User_Name}.");
        }

        //Update Details Of Current Customer (Without Password).
        public void Update(Customer t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd1 = new SqlCommand($"Select User_Name from Customers where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        UserNames.RemoveUserName((string)reader["User_Name"]);
                    }
                    conn.Close();
                }
                using (SqlCommand cmd2 = new SqlCommand($"Update Customers Set First_Name = '{t.First_Name}', Last_Name = '{t.Last_Name}', User_Name = '{t.User_Name}'," +
                    $"Address = '{t.Address}', Credit_Card_Number = '{t.Credit_Card_Number}' Where Id = {t.Id}", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd2.ExecuteNonQuery();
                        UserNames.AddUserName(t.User_Name);
                        return;
                    }
                }
            }
        }

        // Change Password Of Current Customer.
        public void ChangePassword(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update AirLineCompanies Set Password = '{customer.Password}' Where Id = {customer.Id}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
