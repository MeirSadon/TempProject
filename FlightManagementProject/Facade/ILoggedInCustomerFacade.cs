using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    interface ILoggedInCustomerFacade
    {
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
        long PurchaseTicket(LoginToken<Customer> token, Flight flight);
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword);
        bool UserIsValid(LoginToken<Customer> token);
    }
}
