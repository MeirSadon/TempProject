using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
     public abstract class FacadeBase
    {
        protected IAirlineDAO _airlineDAO;
        protected IAdministratorDAO _adminDAO;
        protected ICustomerDAO _customerDAO;
        protected ICountryDAO _countryDAO;
        protected ITicketDAO _ticketDAO;
        protected IFlightDAO _flightDAO;

    }
}
