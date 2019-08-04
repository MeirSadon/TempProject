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
        private new IAirlineDAO _airlineDAO = new AirlineDAOMSSQL();
        private new IAdministratorDAO _adminDAO = new AdministratorDAOMSSQL();
        private new ICustomerDAO _customerDAO = new CustomerDAOMSSQL();
        private new ICountryDAO _countryDAO = new CountryDAOMSSQL();
        private new ITicketDAO _ticketDAO = new TicketDAOMSSQL();
        private new IFlightDAO _flightDAO = new FlightDAOMSSQL();

        // Create New Administrator.
        public long CreateNewAdmin(LoginToken<Administrator> token, Administrator admin)
        {
            long newId = 0;
            if (UserIsValid(token))
            {
                newId = _adminDAO.Add(admin);
            }
            return newId;
        }

        // Create New Airline Company.
        public long CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            long newId = 0;
            if (UserIsValid(token))
            {
                newId = _airlineDAO.Add(airline);
            }
            return newId;
        }

        // Create New Customer.
        public long CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            long newId = 0;
            if (UserIsValid(token))
            {
                newId = _customerDAO.Add(customer);
            }
            return newId;
        }

        // Create New Country.
        public long CreateNewCountry(LoginToken<Administrator> token, Country country)
        {
            long newId = 0;
            if (UserIsValid(token))
            {
               newId = _countryDAO.Add(country);
            }
            return newId;
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
            if (UserIsValid(token))
            {
                if (token.User.Password.ToUpper() == oldPassword.ToUpper())
                {
                    token.User.Password = newPassword.ToUpper();
                    _adminDAO.ChangePassword(token.User);
                }
                else
                    throw new WrongPasswordException("Your Old Password Is Incorrent!");
            }
        }
        
        // Search Airline Company By UserName.
        public AirlineCompany GetAirlineByUserName(LoginToken<Administrator> token, string userName)
        {
            AirlineCompany airline = null;
            if (UserIsValid(token))
            {
                airline = _airlineDAO.GetAirlineByUserame(userName);
            }
            return airline;
        }

        // Search Customer By UserName.
        public Customer GetCustomerByUserName(LoginToken<Administrator> token, string userName)
        {
            Customer customer = null;
            if (UserIsValid(token))
            {
                customer = _customerDAO.GetCustomerByUserName(userName);
            }
            return customer;
        }

        // Search Customer By UserName.
        public Administrator GetAdminByUserName(LoginToken<Administrator> token, string userName)
        {
            Administrator admin = null;
            if (UserIsValid(token))
            {
                admin = _adminDAO.GetByUserName(userName);
            }
            return admin;
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
