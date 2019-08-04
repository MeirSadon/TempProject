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
        long CreateNewAdmin(LoginToken<Administrator> token, Administrator admin);
        long CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline);
        long CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany customer);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
        Administrator GetAdminByUserName(LoginToken<Administrator> token, string userName);
        AirlineCompany GetAirlineByUserName(LoginToken<Administrator> token, string userName);
        Customer GetCustomerByUserName(LoginToken<Administrator> token, string userName);
        void ChangeMyPassword(LoginToken<Administrator> token, string oldPassword, string newPassword);
        bool UserIsValid(LoginToken<Administrator> token);
    }
}
