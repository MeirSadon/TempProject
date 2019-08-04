using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FlightManagementProject.Facade;
using FlightManagementProject;

namespace TestForFlightManagmentProject
{
    static class TestCenter
    {
        static public LoggedInAdministratorFacade defaultFacade = new LoggedInAdministratorFacade();
        static public LoginToken<Administrator> defaultToken = new LoginToken<Administrator> { User = new Administrator { User_Name = FlyingCenterConfig.ADMIN_NAME, Password = FlyingCenterConfig.ADMIN_PASSWORD } };
    


        static public string UserTest()
        {
            return "Test " + new Random().Next(100000);
        }



        static public void PrepareDBForTests(out LoginToken<Administrator> token, out LoggedInAdministratorFacade facade)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Delete From Tickets;" +
                    "Delete From Flights;" +
                    "Delete From AirlineCompanies;" +
                    "Delete From Customers;" +
                    "Delete From Countries;" +
                    "Delete From Administrators;", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            defaultFacade.CreateNewCountry(defaultToken, new Country() { Country_Name = "Israel", });
            facade = defaultFacade;
            token = defaultToken;
        }

        // Create And Log As Airline For The Tests.
        static public void CreateAndLogAsAirline(out LoginToken<AirlineCompany> token, out LoggedInAirlineFacade facade)
        {
            facade = new LoggedInAirlineFacade();
            token = new LoginToken<AirlineCompany> { User = new AirlineCompany { Airline_Name = "TestAirline", User_Name = UserTest(), Password = "Arkia123", Country_Code = defaultFacade.GetCountryByName("Israel").Id } };
            defaultFacade.CreateNewAirline(defaultToken, token.User);
            token.User = defaultFacade.GetAirlineByUserName(defaultToken, token.User.User_Name);
        }

        // Create And Log As Customer For The Tests.
        static public void CreateAndLogAsCustomer(out LoginToken<Customer> token, out LoggedInCustomerFacade facade)
        {
            facade = new LoggedInCustomerFacade();
            token = new LoginToken<Customer> { User = new Customer { First_Name = "TestCustomer", Last_Name = "Cohen", User_Name = UserTest(), Password = "Yoss123", Address = "Ashkelon", Phone_No = "05411111111", Credit_Card_Number = "34233423" } };
            defaultFacade.CreateNewCustomer(defaultToken, token.User);
            token.User = defaultFacade.GetCustomerByUserName(defaultToken, token.User.User_Name);
        }
    }
}
