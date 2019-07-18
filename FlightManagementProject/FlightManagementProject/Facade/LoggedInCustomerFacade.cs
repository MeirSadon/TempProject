using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {

        // Cancel Ticket From Current Customer.
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (UserIsValid(token) && token.User.Id == ticket.Id)
                {
                    if (_flightDAO.GetById((int)ticket.Flight_Id).Departure_Time > DateTime.Now)
                        throw new TooLateToCancelTicketException("You Can't Cancel Your Ticket Because The Flight Has Already Begun");
                    if (_flightDAO.GetById((int)ticket.Flight_Id).Departure_Time > DateTime.Now + TimeSpan.FromHours(1))
                        throw new TooLateToCancelTicketException("You Can't Cancel Your Ticket One Hour Before The Flight");
                    _ticketDAO.Remove(ticket);
                }
        }

        // Search All The Flights For Current Customer.
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            List<Flight> flightsByCustomer = null;
            if (UserIsValid(token))
            {
                flightsByCustomer = _flightDAO.GetFlightsByCustomer(token.User).ToList();
            }
            return flightsByCustomer;
        }

        // Buy New Ticket For Current Customer.
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            Ticket newTicket = null;
            if (UserIsValid(token))
            {
                if (flight.Remaining_Tickets > 0)
                {
                    _ticketDAO.Add(new Ticket { Customer_Id = token.User.Id, Flight_Id = flight.Id });
                    flight.Remaining_Tickets--;
                    _flightDAO.Update(flight);
                }
                else
                {
                    throw new OutOfTicketsException("Sorry But The Tickets For This Flight Is Over");
                }
            }
            return newTicket;
        }

        // Change Details Of Current Airline (Without Password).
        public void MofidyAirlineDetails(LoginToken<Customer> token, Customer customer)
        {
            if (UserIsValid(token) && token.User.Id == customer.Id)
            {
                    _customerDAO.Update(customer);
            }
        }

        // Change Password For Current Customer.
        public void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword)
        {
            if (UserIsValid(token) && token.User.Password.ToUpper() == oldPassword.ToUpper())
            {
                    token.User.Password = newPassword.ToUpper();
                    _customerDAO.ChangePassword(token.User);
            }
        }

        // Check If Customer User That Sent Is Valid.
        public bool UserIsValid(LoginToken<Customer> token)
        {
            if (token != null && token.User != null)
                return true;
            return false;
        }

    }
}