using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    class LoggedInAirlineFacade : AnonymousUserFacade,ILoggedInAirlineFacade
    {
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
            if (UserIsValid(token) && token.User.Password.ToUpper() == oldPassword.ToUpper())
            {
                    token.User.Password = newPassword.ToUpper();
                    _airlineDAO.ChangePassword(token.User);
            }
        }

        // Create New Flight For Current Airline.
        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (UserIsValid(token) && token.User.Id == flight.AirLineCompany_Id)
            {
                    _flightDAO.Add(flight);
            }
        }

        // Search All Flights Of Current Airline.
        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            List<Flight> flights = null;
            if (UserIsValid(token))
            {
                flights = _flightDAO.GetFlightsByAirlineCompany(token.User).ToList();
            }
            return flights;
        }
        
        // Search All Tickets Of Current Airline.
        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
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