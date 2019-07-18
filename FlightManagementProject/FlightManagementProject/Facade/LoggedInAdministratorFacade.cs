using FlightManagementProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    // Class With All The Options That Admin Can Do.
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        private IAirlineDAO _airlineDAO;
        private IAdministratorDAO _adminDAO = new AdministratorDAOMSSQL();
        private ICustomerDAO _customerDAO;
        private ICountryDAO _countryDAO;
        private ITicketDAO _ticketDAO;
        private IFlightDAO _flightDAO;

        // Create New Administrator.
        public void CreateNewAdmin(LoginToken<Administrator> token, Administrator admin)
        {
            if (UserIsValid(token))
            {
                _adminDAO.Add(admin);
            }
        }

        // Create New Airline Company.
        public void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (UserIsValid(token))
            {
                _airlineDAO.Add(airline);
            }
        }

        // Create New Customer.
        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (UserIsValid(token))
            {
                _customerDAO.Add(customer);
            }
        }

        // Remove Some Airline Company.
        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (UserIsValid(token))
            {
                _airlineDAO.Remove(airline);
            }
        }

        //Remove Some Customer.
        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (UserIsValid(token))
            {
                _customerDAO.Remove(customer);
            }
        }

        // Update Details Of Some Airline Company.
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (UserIsValid(token))
            {
                _airlineDAO.Update(airline);
            }
        }

        // Update Details Of Some Customer.
        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if (UserIsValid(token))
            {
                _customerDAO.Update(customer);
            }
        }

        // Change Password.
        public void ChangeMyPassword(LoginToken<Administrator> token, string oldPassword, string newPassword)
        {
            if (UserIsValid(token) && token.User.Password.ToUpper() == oldPassword.ToUpper())
            {
                    token.User.Password = newPassword.ToUpper();
                    _adminDAO.ChangePassword(token.User);
            }
        }

        // Check If User Admin That Sent Is Valid.
        public bool UserIsValid(LoginToken<Administrator> token)
        {
            if (token != null && token.User != null)
                return true;
            return false;
        }
    }
}
