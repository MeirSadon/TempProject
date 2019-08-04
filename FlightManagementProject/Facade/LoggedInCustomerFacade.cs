using FlightManagementProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {

        private new IAirlineDAO _airlineDAO = new AirlineDAOMSSQL();
        private new IAdministratorDAO _adminDAO = new AdministratorDAOMSSQL();
        private new ICustomerDAO _customerDAO = new CustomerDAOMSSQL();
        private new ICountryDAO _countryDAO = new CountryDAOMSSQL();
        private new ITicketDAO _ticketDAO = new TicketDAOMSSQL();
        private new IFlightDAO _flightDAO = new FlightDAOMSSQL();

        // Cancel Ticket From Current Customer.
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (UserIsValid(token) && token.User.Id == ticket.Customer_Id)
                {
                    if (_flightDAO.GetById((int)ticket.Flight_Id).Departure_Time > DateTime.Now)
                        throw new TooLateToCancelTicketException("You Can't Cancel Your Ticket Because The Flight Has Already Begun");
                    if (_flightDAO.GetById((int)ticket.Flight_Id).Departure_Time > DateTime.Now.Add(TimeSpan.FromHours(1)))
                        throw new TooLateToCancelTicketException("You Can't Cancel Your Ticket One Hour Before The Flight");
                    _ticketDAO.Remove(ticket);
                }
        }

        // Search All The Flights For Current Customer.
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            IList<Flight> flightsByCustomer = null;
            if (UserIsValid(token))
            {
                flightsByCustomer = _flightDAO.GetFlightsByCustomer(token.User);
            }
            return flightsByCustomer;
        }

        // Search All The Tickets For Current Customer.
        public IList<Ticket> GetAllMyTickets(LoginToken<Customer> token)
        {
            IList<Ticket> ticketsByCustomer = null;
            if (UserIsValid(token))
            {
                ticketsByCustomer = _ticketDAO.GetTicketsByCustomer(token.User);
            }
            return ticketsByCustomer;
        }

        // Buy New Ticket For Current Customer.
        public long PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            long newId = 0;
            if (UserIsValid(token))
            {
                if (flight.Remaining_Tickets > 0)
                {
                    newId  = _ticketDAO.Add(new Ticket { Customer_Id = token.User.Id, Flight_Id = flight.Id });
                    flight.Remaining_Tickets--;
                    _flightDAO.Update(flight);
                }
                else
                {
                    throw new OutOfTicketsException("Sorry But The Tickets For This Flight Is Over");
                }
            }
            return newId;
        }

        // Change Details Of Current Customer (Without Password).
        public void MofidyCustomerDetails(LoginToken<Customer> token, Customer customer)
        {
            if (UserIsValid(token) && token.User.Id == customer.Id)
            {
                    _customerDAO.Update(customer);
            }
        }

        // Change Password For Current Customer.
        public void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword)
        {
            if (UserIsValid(token))
            {
                if (token.User.Password.ToUpper() == oldPassword.ToUpper())
                {
                    token.User.Password = newPassword.ToUpper();
                    _customerDAO.ChangePassword(token.User);
                }
                else
                    throw new WrongPasswordException("Your Old Password Is Incorrent!");
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