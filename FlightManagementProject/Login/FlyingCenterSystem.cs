using FlightManagementProject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    public class FlyingCenterSystem
    {
        static private FlyingCenterSystem _instance;
        static object key = new object();
        static public LoginService ls = new LoginService();

        private FlyingCenterSystem()
        {
            Thread t = new Thread(UpdateFlightsAndTickets);
            t.Start();
        }

        // Function To Declare _instance Field, Only If It = null.
        static public FlyingCenterSystem GetInstance()
        {
            if (_instance == null)
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlyingCenterSystem();
                    }
                }
            }
            return _instance;
        }

        // Only Here Is Place To Get Any Facade(Because It's Singelton)
        static public bool TryLogin(string userName, string password, out ILogin token, out FacadeBase facade)
        {
            if (ls.TryAdminLogin(userName, password, out LoginToken<Administrator> adminToken))
            {
                token = adminToken;
                facade = new LoggedInAdministratorFacade();
                return true;
            }
            else if (ls.TryAirlineLogin(userName, password, out LoginToken<AirlineCompany> airlineToken))
            {
                token = airlineToken;
                facade = new LoggedInAirlineFacade();
                return true;
            }
            else if (ls.TryCustomerLogin(userName, password, out LoginToken<Customer> customerToken))
            {
                token = customerToken;
                facade = new LoggedInAirlineFacade(); facade = new LoggedInCustomerFacade();
                return true;
            }
            token = null;
            facade = new AnonymousUserFacade();
            return false;
        }
        void UpdateFlightsAndTickets()
        {
            Thread.Sleep(FlyingCenterConfig.TIMEFORTHREADHISTORY);
            //Update Flights And Tickets.
            UpdateFlightsAndTickets();
        }
    }
}
