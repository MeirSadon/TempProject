using System;
using FlightManagementProject;
using FlightManagementProject.DAO;
using FlightManagementProject.Facade;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestLogin
    {
        private void LoginToDefaultAdmin(out LoginToken<Administrator> token, out LoggedInAdministratorFacade user)
        {
            FlyingCenterSystem.TryLogin(FlyingCenterConfig.ADMIN_NAME, FlyingCenterConfig.ADMIN_PASSWORD, out ILogin token1, out FacadeBase user1);
            token = token1 as LoginToken<Administrator>;
            user = user1 as LoggedInAdministratorFacade;
        }

        static int userNames = 0;
        private string CreateUserName()
        {
            ++userNames;
            return userNames.ToString();
        }
        // Supposed To Get Password Exception For Login To Default Admin.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsDefaultAdmin()
        {
            FlyingCenterSystem.TryLogin(FlyingCenterConfig.ADMIN_NAME, "99999", out ILogin token, out FacadeBase user);
        }

        // Supposed To Get Password Exception For Login To DAO Admin.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsAdmin()
        {
            FlyingCenterSystem.TryLogin(FlyingCenterConfig.ADMIN_NAME, FlyingCenterConfig.ADMIN_PASSWORD, out ILogin token, out FacadeBase user);
            LoginToken<Administrator> Mytoken = token as LoginToken<Administrator>;
            LoggedInAdministratorFacade MyUser = user as LoggedInAdministratorFacade;
            MyUser.CreateNewAdmin(Mytoken, new Administrator { User_Name = CreateUserName(), Password = "9999" });
            FlyingCenterSystem.TryLogin("SecondAdmin", "123", out ILogin token2, out FacadeBase user2);
        }

        // Login Succesfully As DAO Admin.
        [TestMethod]
        public void LoginSuccesfullyAsDAOAdmin()
        {
            LoginToDefaultAdmin(out LoginToken<Administrator> myToken, out LoggedInAdministratorFacade myUser);
            Administrator newAdmin = new Administrator() { User_Name = CreateUserName(), Password = "9999" };
            myUser.CreateNewAdmin(myToken,newAdmin);
            FlyingCenterSystem.TryLogin(newAdmin.User_Name, newAdmin.Password, out ILogin token2, out FacadeBase user2);
            myToken = token2 as LoginToken<Administrator>;

            Assert.AreNotEqual(myToken, null);
            Assert.AreNotEqual(myToken.User, null);
            Assert.AreEqual(newAdmin.Password, myToken.User.Password);
        }

        // Supposed To Get Password Exception For Login To Airline.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsAirline()
        {

            FlyingCenterSystem.TryLogin(FlyingCenterConfig.ADMIN_NAME, "99999", out ILogin token, out FacadeBase user);
        }

        // Supposed To Get Password Exception For Login To Customer.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsCustomer()
        {
            FlyingCenterSystem.TryLogin(FlyingCenterConfig.ADMIN_NAME, "99999", out ILogin token, out FacadeBase user);
        }
    }
}
