using System;
using System.Collections.Generic;
using FlightManagementProject;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForAirlineFacadeClass
    {
        /*  ======= All Tests =======
           
            1. CancelFlight           -- CancelFlightForCurrentAirline.
            2. ChangeMyPassword       -- ChangePasswordForAirline + WrongPasswordWhenTryChangePasswordForAirline.
            3. CreateFlight           -- GetAllFlightsForCurrentAirline.
            4. GetAllFlightsByAirline -- GetAllFlightsForCurrentAirline.
            5. GetAllTicketsByAirline -- GetAllTicketsForCurrentAirline. 
            6. MofidyAirlineDetails   -- "TestForAdminFacadeClass"(UpdateAirline).
            7. UpdateFlight           -- UpdateFlightForCurrentAirline.

            ======= All Tests ======= */


        // Try To Get All Tickets For Some Airline.
        [TestMethod]
        public void GetAllTicketsForCurrentAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsAirline(out LoginToken<AirlineCompany> airlineToken, out LoggedInAirlineFacade airlineFacade);
            TestCenter.CreateAndLogAsCustomer(out LoginToken<Customer> customerToken, out LoggedInCustomerFacade customerFacade);
            Flight flight = new Flight { AirLineCompany_Id = airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1), Origin_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Destination_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = airlineFacade.CreateFlight(airlineToken, flight);
            customerFacade.PurchaseTicket(customerToken, flight);
            IList<Ticket> tickets = airlineFacade.GetAllTicketsByAirline(airlineToken);
            Assert.AreEqual(1, tickets.Count);
        }

        // Try To Cancel Flight For Current Airline.
        [TestMethod]
        public void CancelFlightForCurrentAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsAirline(out LoginToken<AirlineCompany> airlineToken, out LoggedInAirlineFacade airlineFacade);
            Flight flight = new Flight { AirLineCompany_Id = airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1), Origin_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Destination_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = airlineFacade.CreateFlight(airlineToken, flight);
            IList<Flight> flights = airlineFacade.GetAllFlights();
            Assert.AreEqual(1, flights.Count);
            airlineFacade.CancelFlight(airlineToken, flights[0]);
            flights = airlineFacade.GetAllFlightsByAirline(airlineToken);
            Assert.AreEqual(0, flights.Count);
        }

        // Try To Update Flight For Current Airline.
        [TestMethod]
        public void UpdateFlightForCurrentAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsAirline(out LoginToken<AirlineCompany> airlineToken, out LoggedInAirlineFacade airlineFacade);
            Flight flight = new Flight { AirLineCompany_Id = airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1), Origin_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Destination_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = airlineFacade.CreateFlight(airlineToken, flight);
            flight.Remaining_Tickets = 555;
            airlineFacade.UpdateFlight(airlineToken, flight);
            IList<Flight> flights = airlineFacade.GetAllFlightsByAirline(airlineToken);
            Assert.AreEqual(flights[0].Remaining_Tickets, 555);
        }

        // Change Password Successfuly For Airline.
        [TestMethod]
        public void ChangePasswordForAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsAirline(out LoginToken<AirlineCompany> airlineToken, out LoggedInAirlineFacade airlineFacade);
            airlineFacade.ChangeMyPassword(airlineToken, "Arkia123", "NewPassword");
            Assert.AreEqual(airlineToken.User.Password, "NewPassword".ToUpper());
        }

        // Supposed To Get "WrongPasswordException" When Try Change Password For Airline.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryChangePasswordForAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsAirline(out LoginToken<AirlineCompany> airlineToken, out LoggedInAirlineFacade airlineFacade);
            airlineFacade.ChangeMyPassword(airlineToken, "123456", "newPassword");
        }
    }
}
