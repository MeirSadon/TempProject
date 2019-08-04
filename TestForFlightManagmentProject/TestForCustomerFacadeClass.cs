using System;
using System.Collections.Generic;
using FlightManagementProject;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForCustomerFacadeClass
    {
        /*  ======= All Tests =======

    1. CancelTicket          -- CancelTicketSuccessfuly + TooLateToCancelTicketWhenTryCancelTicket.
    2. GetAllMyTickets       -- CancelTicketSuccessfuly.
    3. GetAllMyFlights       -- CancelTicketSuccessfuly.
    4. PurchaseTicket        -- CancelTicketSuccessfuly.
    5. MofidyCustomerDetails  -- "TestForAdminFacadeClass"(UpdateCustomer).
    6. ChangeMyPassword      -- ChangePasswordForCustomer + WrongPasswordWhenTryChangePasswordForCustomer.

    ======= All Tests ======= */

        [TestMethod]
        public void CancelTicketSuccessfuly()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsCustomer(out LoginToken<Customer> customerToken, out LoggedInCustomerFacade customerFacade);
            TestCenter.CreateAndLogAsAirline(out LoginToken<AirlineCompany> airlineToken, out LoggedInAirlineFacade airlineFacade);
            Flight flight = new Flight {AirLineCompany_Id = airlineToken.User.Id, Departure_Time = DateTime.Now + TimeSpan.FromDays(1), Landing_Time = DateTime.Now + TimeSpan.FromDays(2), Origin_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Destination_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = airlineFacade.CreateFlight(airlineToken, flight);
            customerFacade.PurchaseTicket(customerToken, flight);
            Assert.AreEqual(customerFacade.GetAllMyFlights(customerToken).Count, 1);
            customerFacade.CancelTicket(customerToken, customerFacade.GetAllMyTickets(customerToken)[0]);
            Assert.AreEqual(customerFacade.GetAllMyFlights(customerToken).Count, 0);
        }

        //Supposed To Get "TooLateToCancelTicket" Exception.
        [TestMethod]
        public void TooLateToCancelTicketWhenTryCancelTicket()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsCustomer(out LoginToken<Customer> customerToken, out LoggedInCustomerFacade customerFacade);
            TestCenter.CreateAndLogAsAirline(out LoginToken<AirlineCompany> airlineToken, out LoggedInAirlineFacade airlineFacade);
            Flight flight = new Flight { AirLineCompany_Id = airlineToken.User.Id, Departure_Time = DateTime.Now - TimeSpan.FromDays(1), Landing_Time = DateTime.Now + TimeSpan.FromDays(2), Origin_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Destination_Country_Code = TestCenter.defaultFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = airlineFacade.CreateFlight(airlineToken, flight);
            customerFacade.PurchaseTicket(customerToken, flight);
            customerFacade.CancelTicket(customerToken, customerFacade.GetAllMyTickets(customerToken)[0]);
        }

        // Change Password Successfuly For Customer.
        [TestMethod]
        public void ChangePasswordForCustomer()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsCustomer(out LoginToken<Customer> customerToken, out LoggedInCustomerFacade customerFacade);
            int pass = new Random().Next(1000);
            customerFacade.ChangeMyPassword(customerToken, customerToken.User.Password, $"{pass}".ToUpper());
            Assert.AreEqual(customerToken.User.Password, $"{pass}".ToUpper());
        }

        // Supposed To Get "WrongPasswordException" When Try Change Password For Customer.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryChangePasswordForCustomer()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            TestCenter.CreateAndLogAsCustomer(out LoginToken<Customer> customerToken, out LoggedInCustomerFacade customerFacade);
            customerFacade.ChangeMyPassword(customerToken, "123456", "newPassword");
        }
    }
}
