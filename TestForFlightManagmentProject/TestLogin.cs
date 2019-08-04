using System;
using System.Collections.Generic;
using FlightManagementProject;
using FlightManagementProject.DAO;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestLogin
    {
      /* ========   All Tests ========
        
         1. GetPasswordExceptionForDefaultAdmin -- WrongPasswordWhenTryLoginAsDefaultAdmin.
         2. GetPasswordExceptionForDAOAdmin     -- WrongPasswordWhenTryLoginAsAdmin.
         3. LoginSuccessfulyForDefaultAdmin     -- LoginSuccesfullyAsDefaultAdmin.
         4. LoginSuccessfulyForDAOAdmin         -- LoginSuccesfullyAsDAOAdmin.
         5. GetPasswordExceptionForAirline      -- WrongPasswordWhenTryLoginAsAirline.
         6. LoginSuccessfulyForAirline          -- LoginSuccesfullyAsAirline.
         7. GetPasswordExceptionForCustomer     -- WrongPasswordWhenTryLoginAsCustomer.
         8. LoginSuccessfulyForCustomer         -- LoginSuccesfullyAsCustomer.
         
         ======= All Tests ======= */


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
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Administrator newAdmin = new Administrator { User_Name = "Admin" + TestCenter.UserTest(), Password = "9999" };
            adminFacade.CreateNewAdmin(adminToken, newAdmin);
            FlyingCenterSystem.TryLogin(newAdmin.User_Name, "ErrorPassword", out ILogin token2, out FacadeBase user2);
        }

        // Login Succesfully As Dfault Admin.
        [TestMethod]
        public void LoginSuccesfullyAsDefaultAdmin()
        {
            FlyingCenterSystem.TryLogin(FlyingCenterConfig.ADMIN_NAME, FlyingCenterConfig.ADMIN_PASSWORD, out ILogin token2, out FacadeBase user2);
            LoginToken<Administrator> newAdminToken = token2 as LoginToken<Administrator>;
            Assert.AreNotEqual(newAdminToken, null);
        }

        // Login Succesfully As DAO Admin.
        [TestMethod]
        public void LoginSuccesfullyAsDAOAdmin()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Administrator newAdmin = new Administrator() { User_Name = "Admin" + TestCenter.UserTest(), Password = "9999" };
            adminFacade.CreateNewAdmin(adminToken,newAdmin);
            FlyingCenterSystem.TryLogin(newAdmin.User_Name, newAdmin.Password, out ILogin token2, out FacadeBase user2);
            LoginToken<Administrator> newAdminToken = token2 as LoginToken<Administrator>;
            Assert.AreNotEqual(newAdmin,null);
        }
        
        // Supposed To Get Password Exception For Login To Airline.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany newAirline = new AirlineCompany { Airline_Name = "Flighter", User_Name = "Airline" + TestCenter.UserTest(), Password = "5555", Country_Code = adminFacade.GetCountryByName("Israel").Id };
            adminFacade.CreateNewAirline(adminToken, newAirline);
            FlyingCenterSystem.TryLogin(newAirline.User_Name, "ErrorPassword", out ILogin token2, out FacadeBase user2);
        }
        
        // Login Succesfully As Airline.
        [TestMethod]
        public void LoginSuccesfullyAsAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany newAirline = new AirlineCompany { Airline_Name = "Flighter", User_Name = "Airline" + TestCenter.UserTest(), Password = "5555", Country_Code = adminFacade.GetCountryByName("Israel").Id };
            adminFacade.CreateNewAirline(adminToken,newAirline);
            FlyingCenterSystem.TryLogin(newAirline.User_Name, "5555", out ILogin token2, out FacadeBase user2);
            Assert.AreNotEqual(newAirline, null);
        }
        
        // Supposed To Get Password Exception For Login To Customer.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsCustomer()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer newCustomer = new Customer { First_Name = "Shiran", Last_Name = "Ben Sadon", User_Name = "Customer" + TestCenter.UserTest(), Password = "123", Address = "Neria 28", Phone_No = "050", Credit_Card_Number = "3317" };
            adminFacade.CreateNewCustomer(adminToken, newCustomer);
            FlyingCenterSystem.TryLogin(newCustomer.User_Name, "ErrorPassword", out ILogin token2, out FacadeBase user2);
        }
        
        // Login Succesfully As Customer.
        [TestMethod]
        public void LoginSuccesfullyAsCustomer()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer newCustomer = new Customer { First_Name = "Shiran", Last_Name = "Ben Sadon", User_Name = "Customer" + TestCenter.UserTest(), Password = "123", Address = "Neria 28", Phone_No = "050", Credit_Card_Number = "3317" };
            adminFacade.CreateNewCustomer(adminToken, newCustomer);
            FlyingCenterSystem.TryLogin(newCustomer.User_Name, "123", out ILogin token2, out FacadeBase user2);
            Assert.AreNotEqual(newCustomer, null);
        }
    }
}
