using FlightManagementProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    interface ILoggedInAdministratorFacade
    {
        void CreateNewAdmin(LoginToken<Administrator> token, Administrator admin);
        void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany customer);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
        void ChangeMyPassword(LoginToken<Administrator> token, string oldPassword, string newPassword);
        bool UserIsValid(LoginToken<Administrator> token);
    }
}
