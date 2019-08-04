using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public interface IAdministratorDAO : IBasicDB<Administrator>
    {
        Administrator GetByUserName(string userName);
        AirlineCompany GetAirlineById(int id);
        AirlineCompany GetAirlineByUserName(string userName);
        IList<AirlineCompany> GetAllAirlineByCountry(int countryId);
        IList<AirlineCompany> GetAllAirelineCompanies();
        Customer GetCustomerById(int id);
        Customer GetCustomerByUserName(string userName);
        IList<Customer> GetAllCustomerByAddress(string Address);
        IList<Customer> GetAllCustomerByCardNumber(int cardNumber);
        IList<Customer> GetAllCustomers();
        string GetAllActionHistory();
        void ChangePassword(Administrator administrator);
    }
}
