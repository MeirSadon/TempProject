using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    // Class With All Funtions(Of Ticket) For MSSQL Data Base.
    class TicketDAOMSSQL : ITicketDAO
    {

        // Add New Ticket.
        public long Add(Ticket t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into Tickets(Flight_Id, Customer_Id) Values ({t.Flight_Id}, {t.Customer_Id}); SELECT CAST(scope_identity() AS bigint)", conn))
                {
                    cmd.Parameters.AddWithValue($"{t.Flight_Id}", "bar");
                    newId = (long)cmd.ExecuteScalar();
                }
            }
            return newId;
        }

        // Search Ticket By Id.
        public Ticket GetById(int id)
        {
            Ticket ticket = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Select * From Tickets Where Id = {id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            ticket = new Ticket
                            {
                                Id = (long)reader["Id"],
                                Customer_Id = (long)reader["Customer_Id"],
                                Flight_Id = (long)reader["Flight_Id"],
                            };
                        }

                    }
                }
            }
            return ticket;
        }

        // Search All Tickets.
        public IList<Ticket> GetAll()
        {
            List<Ticket> tickets = new List<Ticket>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from tickets", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        tickets.Add(new Ticket
                        {
                            Id = (long)reader["Id"],
                            Customer_Id = (long)reader["Customer_Id"],
                            Flight_Id = (long)reader["Flight_Id"],
                        });
                    }
                }
            }
            return tickets;
        }

        // Remove Ticket.
        public void Remove(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete From Tickets where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.RecordsAffected > 0)
                    {
                        return;
                    }
                }
            }
            throw new ArgumentException($"Sorry, But We Don't Found Ticket With This Id.");
        }

        // Update Ticket
        public void Update(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update Tickets Set Flight_Id = {t.Flight_Id}, Customer_Id = " +
                    $"{t.Customer_Id} where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            throw new ArgumentException($"Sorry, But We Don't Found Ticket With This Id.");
        }

        // Search All Tickets By Airline Company.
        public IList<Ticket> GetTicketsByAirlineComapny(AirlineCompany airline)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights f join Tickets t on f.Id = t.Flight_Id where" +
                    $" f.AirlineCompany_Id = {airline.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        tickets.Add(new Ticket
                        {
                            Id = (long)reader["Id"],
                            Customer_Id = (long)reader["Customer_Id"],
                            Flight_Id = (long)reader["Flight_Id"],
                        });
                    }
                }
            }
            return tickets;
        }
        
        // Search All Tickets By Customer.
        public IList<Ticket> GetTicketsByCustomer(Customer customer)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Tickets t where t.Customer_Id = {customer.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        tickets.Add(new Ticket
                        {
                            Id = (long)reader["Id"],
                            Customer_Id = (long)reader["Customer_Id"],
                            Flight_Id = (long)reader["Flight_Id"],
                        });
                    }
                }
            }
            return tickets;
        }
    }
}
