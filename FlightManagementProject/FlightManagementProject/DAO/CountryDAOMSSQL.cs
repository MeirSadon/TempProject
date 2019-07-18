using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    // Class With All Funtions(Of Country) For MSSQL Data Base.
    class CountryDAOMSSQL : ICountryDAO
    {

        // Add New Country.
        public void Add(Country t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Insert Into Countries(Country_Name) Values ('{t.Country_Name}')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Search Country By Id.
        public Country GetById(int id)
        {
            Country country = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * From Countries Where Id = {id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            country = new Country
                            {
                                Id = (long)reader["Id"],
                                Country_Name = (string)reader["Country_Name"],
                            };
                        }

                    }
                }
            }
            return country;
        }

        // Search All Countries.
        public IList<Country> GetAll()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"select * from countries", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        countries.Add(new Country
                        {
                            Id = (long)reader["Id"],
                            Country_Name = (string)reader["Country_Name"],
                        });
                    }
                }
            }
            return countries;
        }

        // Get Name Of Country By Id.
        public string GetNameCountryById(int id)
        {
            Country country = null;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Select * From Countries Where Id = {id}"))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read() == true)
                    {
                        country.Id = (long)reader["Id"];
                        country.Country_Name = (string)reader["Country_Name"];
                    }
                }
            }
            return country.Country_Name;
        }

        // Remove Country.
        public void Remove(Country t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Delete From Countries where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            throw new ArgumentException($"Sorry, But We Don't Found Country With This Id.");
        }

        // Update Country.
        public void Update(Country t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand($"Update Countries Set Country_Name = '{t.Country_Name}' where Id = {t.Id}", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            throw new ArgumentException($"Sorry, But We Don't Found Country With This Id.");
        }
    }
}
