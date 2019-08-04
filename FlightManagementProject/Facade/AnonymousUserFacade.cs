using FlightManagementProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    // Basic Facade(Without Login) With The Simple Options.
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {

        private new IAirlineDAO _airlineDAO = new AirlineDAOMSSQL();
        private new IAdministratorDAO _adminDAO = new AdministratorDAOMSSQL();
        private new ICustomerDAO _customerDAO = new CustomerDAOMSSQL();
        private new ICountryDAO _countryDAO = new CountryDAOMSSQL();
        private new ITicketDAO _ticketDAO = new TicketDAOMSSQL();
        private new IFlightDAO _flightDAO = new FlightDAOMSSQL();

        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }

        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }

        public Flight GetFlightById(int id)
        {
            return _flightDAO.GetById(id);
        }

        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepartureDate(departureDate);
        }

        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByOriginCounty(countryCode);
        }

        public Country GetCountryByName(string name)
        {
            return _countryDAO.GetByName(name);
        }

        public AirlineCompany GetAirlineByName(string name)
        {
            return _airlineDAO.GetAirlineByUserame(name);
        }

    }
}
