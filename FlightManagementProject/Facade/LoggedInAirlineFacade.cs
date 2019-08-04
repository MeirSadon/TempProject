using FlightManagementProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    public class LoggedInAirlineFacade : AnonymousUserFacade,ILoggedInAirlineFacade
    {
        private new IAirlineDAO _airlineDAO = new AirlineDAOMSSQL();
        private new IAdministratorDAO _adminDAO = new AdministratorDAOMSSQL();
        private new ICustomerDAO _customerDAO = new CustomerDAOMSSQL();
        private new ICountryDAO _countryDAO = new CountryDAOMSSQL();
        private new ITicketDAO _ticketDAO = new TicketDAOMSSQL();
        private new IFlightDAO _flightDAO = new FlightDAOMSSQL();

        // Cancel Flight From This Airline.
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (UserIsValid(token) && token.User.Id == flight.AirLineCompany_Id)
            {
                    _flightDAO.Remove(flight);
            }
        }

        // Change Password.
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (UserIsValid(token))
            {
                if (token.User.Password.ToUpper() == oldPassword.ToUpper())
                {
                    token.User.Password = newPassword.ToUpper();
                    _airlineDAO.ChangePassword(token.User);
                }
                else
                    throw new WrongPasswordException("Your Old Password Is Incorrent!");
            }
        }

        // Create New Flight For Current Airline.
        public long CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            long newId = 0;
            if (UserIsValid(token) && token.User.Id == flight.AirLineCompany_Id)
            {
                   newId = _flightDAO.Add(flight);
            }
            return newId;
        }

        // Search All Flights Of Current Airline.
        public IList<Flight> GetAllFlightsByAirline(LoginToken<AirlineCompany> token)
        {
            List<Flight> flights = new List<Flight>();
            if (UserIsValid(token))
            {
                flights = _flightDAO.GetFlightsByAirlineCompany(token.User).ToList();
            }
            return flights;
        }
        
        // Search All Tickets Of Current Airline.
        public IList<Ticket> GetAllTicketsByAirline(LoginToken<AirlineCompany> token)
        {
            List<Ticket> tickets = null;
            if (UserIsValid(token))
            {
                tickets = _ticketDAO.GetTicketsByAirlineComapny(token.User).ToList();
            }
            return tickets;
        }

        // Change Details Of Current Airline (Without Password).
        public void MofidyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (UserIsValid(token) && token.User.Id == airline.Id)
            {
                    _airlineDAO.Update(airline);
            }
        }

        //Update Flight Of Current Airline.
        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (UserIsValid(token) && token.User.Id == flight.AirLineCompany_Id)
            {
                    _flightDAO.Update(flight);
            }
        }

        // Check If User Admin That Sent Is Valid.
        public bool UserIsValid(LoginToken<AirlineCompany> token)
        {
            if (token != null && token.User != null)
                return true;
            return false;
        }
    }
}