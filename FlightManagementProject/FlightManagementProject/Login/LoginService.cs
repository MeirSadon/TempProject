using FlightManagementProject.DAO;
using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public class LoginService : ILoginService
    {
        private IAirlineDAO _airlineDAO = new AirlineDAOMSSQL();
        private ICustomerDAO _customerDAO = new CustomerDAOMSSQL();
        private IAdministratorDAO _administratorDAO = new AdministratorDAOMSSQL();


        // Try To Login As Airline User.
        public bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            token = null;
            AirlineCompany airlineCompany = _airlineDAO.GetAirlineByUserame(userName);
            if (airlineCompany != null)
            {
                if (airlineCompany.Password.ToUpper() == password.ToUpper())
                {
                    token = new LoginToken<AirlineCompany> { User = airlineCompany };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Your Password Isn't Match To Your UserName!");
                }
            }
            return false;
        }

        // Try To Login As Admin User.
        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            token = null;
            // Default Admin.
            if (userName.ToUpper() == FlyingCenterConfig.ADMIN_NAME)
            {
                if (password == FlyingCenterConfig.ADMIN_PASSWORD)
                {
                    token = new LoginToken<Administrator> { User = new Administrator {Id = 0, User_Name = FlyingCenterConfig.ADMIN_NAME, Password = FlyingCenterConfig.ADMIN_PASSWORD } };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Your Password Isn't Match To Your UserName!");
                }
            }
            //DAO Admin.
            Administrator admin = _administratorDAO.GetByUserName(userName);

            if (admin.User_Name.ToUpper() == userName.ToUpper())
            {
                if (admin.Password.ToUpper() == password.ToUpper())
                {
                    token = new LoginToken<Administrator> { User = admin };
                    return true;
                }
                else
                    throw new WrongPasswordException("Your Password Isn't Match To Your UserName!");
            }
            return false;
        }

        // Try To Login As Customer User.
        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            token = null;
            Customer customer = _customerDAO.GetCustomerByUserName(userName);
                if (customer != null)
                {
                    if (customer.Password.ToUpper() == password.ToUpper())
                    {
                        token = new LoginToken<Customer>() { User = customer };
                    }
                    else
                        throw new WrongPasswordException("Your Password Isn't Match To Your UserName!");
                }
            return false;
        }
    }
}
