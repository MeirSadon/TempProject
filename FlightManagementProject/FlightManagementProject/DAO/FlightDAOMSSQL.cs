using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    // Class With All Funtions(Of Flight) For MSSQL Data Base.
    class FlightDAOMSSQL : IFlightDAO
    {
        
        // Add New Flight.
        public void Add(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Insert Into Flights(AirLineCompany_Id, Origin_Country_Code, Destination_Country_Code, Departure_Time," +
                    $" Landing_Time, Remaining_Tickets) Values ({t.AirLineCompany_Id},{t.Origin_Country_Code},{t.Destination_Country_Code},{t.Departure_Time}," +
                    $"{t.Landing_Time},{t.Remaining_Tickets})", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Search All Flights.
        public IList<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"select * from Flights", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        flights.Add(new Flight
                        {
                            Id = (long)reader["Id"],
                            AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                            Origin_Country_Code = (long)reader["Origin_Country_Code"],
                            Destination_Country_Code = (long)reader["Destination_Country_Code"],
                            Departure_Time = (DateTime)reader["Departure_Time"],
                            Landing_Time = (DateTime)reader["Landing_Time"],
                            Remaining_Tickets = (int)reader["Remaining_Tickets"],

                        });
                    }
                }
            }
            return flights;
        }

        // Search All Flights By Airline Company.
        public IList<Flight> GetFlightsByAirlineCompany(AirlineCompany airline)
        {
            List<Flight> flightsByAirline = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * from Flights f where f.AirlineCompany_Id = {airline.Id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            flightsByAirline.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                                Origin_Country_Code = (long)reader["Origin_Country_Code"],
                                Destination_Country_Code = (long)reader["Destination_Country_Code"],
                                Departure_Time = (DateTime)reader["Departure_Time"],
                                Landing_Time = (DateTime)reader["Landing_Time"],
                                Remaining_Tickets = (int)reader["Remaining_Tickets"],
                            });
                        }
                    }
                }
            }
            return flightsByAirline;
        }

        // Search How Much Tickets Not Buy Yet From Each Flight.
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> ticketsByFlight = new Dictionary<Flight, int>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"select * from Flights", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        ticketsByFlight.Add(new Flight
                        {
                            Id = (long)reader["Id"],
                            AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                            Origin_Country_Code = (long)reader["Origin_Country_Code"],
                            Destination_Country_Code = (long)reader["Destination_Country_Code"],
                            Departure_Time = (DateTime)reader["Departure_Time"],
                            Landing_Time = (DateTime)reader["Landing_Time"],
                            Remaining_Tickets = (int)reader["Remaining_Tickets"],
                        },(int)reader["Remaining_Tickets"]);
                    }
                }
            }
            return ticketsByFlight;
        }

        // Search Flight By Id.
        public Flight GetById(int id)
        {
            Flight flight = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * From Flights Where Id = {id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            flight = new Flight
                            {
                                Id = (long)reader["Id"],
                                AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                                Origin_Country_Code = (long)reader["Origin_Country_Code"],
                                Destination_Country_Code = (long)reader["Destination_Country_Code"],
                                Departure_Time = (DateTime)reader["Departure_Time"],
                                Landing_Time = (DateTime)reader["Landing_Time"],
                                Remaining_Tickets = (int)reader["Remaining_Tickets"],
                            };
                        }
                    }
                }
            }
            return flight;
        }

        // Search All Flights By Customer.
        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> flightsByCustomer = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"select * from flights f join tickets t on f.Id = t.Flight_Id where t.Customer_Id = 1", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            flightsByCustomer.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                                Origin_Country_Code = (long)reader["Origin_Country_Code"],
                                Destination_Country_Code = (long)reader["Destination_Country_Code"],
                                Departure_Time = (DateTime)reader["Departure_Time"],
                                Landing_Time = (DateTime)reader["Landing_Time"],
                                Remaining_Tickets = (int)reader["Remaining_Tickets"],
                            });
                        }
                    }
                }
            }
            return flightsByCustomer;
        }

        // Search All Flights By Departue Time.
        public IList<Flight> GetFlightsByDepartureDate(DateTime date)
        {
            List<Flight> flightsByDepartueTime = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * from Flights f where f.Departue_Time = '{date}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            flightsByDepartueTime.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                                Origin_Country_Code = (long)reader["Origin_Country_Code"],
                                Destination_Country_Code = (long)reader["Destination_Country_Code"],
                                Departure_Time = (DateTime)reader["Departure_Time"],
                                Landing_Time = (DateTime)reader["Landing_Time"],
                                Remaining_Tickets = (int)reader["Remaining_Tickets"],
                            });
                        }
                    }
                }
            }
            return flightsByDepartueTime;
        }

        //// Search All Flights By Destination Country.
        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flight> flightsByDestinationCountry = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * from Flights f where f.Destination_Country_Code = {countryCode}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            flightsByDestinationCountry.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                                Origin_Country_Code = (long)reader["Origin_Country_Code"],
                                Destination_Country_Code = (long)reader["Destination_Country_Code"],
                                Departure_Time = (DateTime)reader["Departure_Time"],
                                Landing_Time = (DateTime)reader["Landing_Time"],
                                Remaining_Tickets = (int)reader["Remaining_Tickets"],
                            });
                        }
                    }
                }
            }
            return flightsByDestinationCountry;
        }

        // Search All Flights By Landing Date.
        public IList<Flight> GetFlightsByLandingDate(DateTime date)
        {
            List<Flight> flightsByLandingTime = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * from Flights f where f.Landing_Time = '{date}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            flightsByLandingTime.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                                Origin_Country_Code = (long)reader["Origin_Country_Code"],
                                Destination_Country_Code = (long)reader["Destination_Country_Code"],
                                Departure_Time = (DateTime)reader["Departure_Time"],
                                Landing_Time = (DateTime)reader["Landing_Time"],
                                Remaining_Tickets = (int)reader["Remaining_Tickets"],
                            });
                        }
                    }
                }
            }
            return flightsByLandingTime;
        }

        // Search All Flights By Origin County.
        public IList<Flight> GetFlightsByOriginCounty(int countryCode)
        {
            List<Flight> flightsByOriginCountry = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * from Flights f where f.Origin_Country_Code = {countryCode}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            flightsByOriginCountry.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirLineCompany_Id = (long)reader["AirLineCompany_Id"],
                                Origin_Country_Code = (long)reader["Origin_Country_Code"],
                                Destination_Country_Code = (long)reader["Destination_Country_Code"],
                                Departure_Time = (DateTime)reader["Departure_Time"],
                                Landing_Time = (DateTime)reader["Landing_Time"],
                                Remaining_Tickets = (int)reader["Remaining_Tickets"],
                            });
                        }
                    }
                }
            }
            return flightsByOriginCountry;
        }

        // Remove Flight.
        public void Remove(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Delete From Flights where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
        }
        throw new ArgumentException($"Sorry, But We Don't Found Flight With This Id.");
    }

        // Update Flight.
        public void Update(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Update Flights Set AirLineCompany_Id = {t.AirLineCompany_Id}, Origin_Country_Code = {t.Origin_Country_Code}," +
                    $" Destination_Country_Code = {t.Destination_Country_Code}, Departure_Time = {t.Departure_Time}," +
                    $" Landing_Time = {t.Landing_Time},Remaining_Tickets = {t.Remaining_Tickets} where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            throw new ArgumentException($"Sorry, But We Don't Found Flight With This Id.");

        }
    }
}
