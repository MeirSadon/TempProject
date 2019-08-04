using System;
using FlightManagementProject;
using FlightManagementProject.DAO;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForAdminFacadeClass
    {
        /* ========   All Tests ========

           1. CreateNewAdmin        -- "TestLogin Class" (LoginSuccesfullyAsDAOAdmin).
           2. CreateNewAirline      -- "TestLogin Class" (LoginSuccesfullyAsAirline).
           3. CreateNewCustomer     -- "TestLogin Class" (LoginSuccesfullyAsCustomer).
           4. RemoveAirline         -- RemoveAirlineSuccessfully + TryRemoveAirlineUserThatNotExist.
           5. RemoveCustomer        -- RemoveCustomerSuccessfully + TryRemoveCustomerUserThatNotExist.
           6. UpdateAirlineDetails  -- UpdateAirline.
           7. UpdateCustomerDetails -- UpdateCustomer.
           8. ChangeMyPassword      -- ChangePasswordForAdministrator + WrongPasswordWhenTryChangePasswordForAdmin.
           9. GetAirlineByUserName  -- UpdateAirline.
          10. GetCustomerByUserName -- UpdateCustomer.
          11. GetAdminByUserName    -- 

           ========   All Tests ======== */


        // Update Details For Airline Company.
        [TestMethod]
        public void UpdateAirline()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany airlineToUpdate = new AirlineCompany { Airline_Name = "AirlineForUpdate", User_Name = TestCenter.UserTest(), Password = "123", Country_Code = adminFacade.GetCountryByName("Israel").Id };
            adminFacade.CreateNewAirline(adminToken, airlineToUpdate);
            airlineToUpdate = adminFacade.GetAirlineByUserName(adminToken, airlineToUpdate.User_Name);
            airlineToUpdate.Airline_Name = "CHANGED!";
            adminFacade.UpdateAirlineDetails(adminToken, airlineToUpdate);
            Assert.AreEqual(adminFacade.GetAirlineByUserName(adminToken, airlineToUpdate.User_Name).Airline_Name, "CHANGED!");
        }

        // Remove Airline Successfully.
        [TestMethod]
        public void RemoveAirlineSuccessfully()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany airlineToRemove = new AirlineCompany { Airline_Name = "AirlineForRemove", User_Name = TestCenter.UserTest(), Password = "123", Country_Code = adminFacade.GetCountryByName("Israel").Id };
            adminFacade.CreateNewAirline(adminToken, airlineToRemove);
            adminFacade.RemoveAirline(adminToken, airlineToRemove);
            Assert.AreEqual(adminFacade.GetAirlineByUserName(adminToken,airlineToRemove.User_Name), null);
        }

        // Supposed To Get "UserNotExistException" When Try Remove Airline.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveAirlineUserThatNotExist()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany airlineToRemove = new AirlineCompany { Airline_Name = "AirlineForRemove", User_Name = TestCenter.UserTest(), Password = "123", Country_Code = adminFacade.GetCountryByName("Israel").Id };
            adminFacade.CreateNewAirline(adminToken, airlineToRemove);
            airlineToRemove.User_Name = "AirlineWithOtherName";
            adminFacade.RemoveAirline(adminToken, airlineToRemove);
        }

        // Update Details For Customer.
        [TestMethod]
        public void UpdateCustomer()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer customerToUpdate = new Customer { First_Name = "Shiran", Last_Name = "Ben Sadon", User_Name = TestCenter.UserTest(), Password = "123", Address = "Neria 28", Phone_No = "050", Credit_Card_Number = "3317" };
            adminFacade.CreateNewCustomer(adminToken, customerToUpdate);
            customerToUpdate = adminFacade.GetCustomerByUserName(adminToken, customerToUpdate.User_Name);
            customerToUpdate.First_Name = "CHANGED!";
            adminFacade.UpdateCustomerDetails(adminToken, customerToUpdate);
            Assert.AreEqual(adminFacade.GetCustomerByUserName(adminToken, customerToUpdate.User_Name).First_Name, "CHANGED!");
        }

        // Remove Customer Successfully.
        [TestMethod]
        public void RemoveCustomerSuccessfully()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer CustomerToRemove = new Customer { First_Name = "Shiran", Last_Name = "Ben Sadon", User_Name = TestCenter.UserTest(), Password = "123", Address = "Neria 28", Phone_No = "050", Credit_Card_Number = "3317" };
            adminFacade.CreateNewCustomer(adminToken, CustomerToRemove);
            adminFacade.RemoveCustomer(adminToken, CustomerToRemove);
            Assert.AreEqual(adminFacade.GetCustomerByUserName(adminToken, CustomerToRemove.User_Name), null);
        }

        // Supposed To Get "UserNotExistException" When Try Remove Customer.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveCustomerUserThatNotExist()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer CustomerToRemove = new Customer { First_Name = "Shiran", Last_Name = "Ben Sadon", User_Name = TestCenter.UserTest(), Password = "123", Address = "Neria 28", Phone_No = "050", Credit_Card_Number = "3317" };
            adminFacade.CreateNewCustomer(adminToken, CustomerToRemove);
            CustomerToRemove.User_Name = "CustomerWithOtherName";
            adminFacade.RemoveCustomer(adminToken, CustomerToRemove);
        }

        // Change Password Successfuly For Administrator.
        [TestMethod]
        public void ChangePasswordForAdministrator()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Administrator adminForPassword = new Administrator {User_Name = TestCenter.UserTest(), Password = "123" };
            adminFacade.CreateNewAdmin(adminToken, adminForPassword);
            FlyingCenterSystem.TryLogin(adminForPassword.User_Name, adminForPassword.Password, out ILogin token, out FacadeBase facade);
            LoginToken<Administrator> newToken = token as LoginToken<Administrator>;
            LoggedInAdministratorFacade newFacade = facade as LoggedInAdministratorFacade;
            newFacade.ChangeMyPassword(newToken, "123", "newPassword");
            Assert.AreEqual(newToken.User.Password, "newPassword".ToUpper());
        }

        // Supposed To Get "WrongPasswordException" When Try Change Password For Administrator.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryChangePasswordForAdmin()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            adminFacade.ChangeMyPassword(adminToken, "123456", "newPassword");

        }

        // Search Some Admin By User Name.
        [TestMethod]
        public void GetAdminByUserName()
        {
            TestCenter.PrepareDBForTests(out LoginToken<Administrator> adminToken, out LoggedInAdministratorFacade adminFacade);
            Administrator admin = new Administrator { User_Name = TestCenter.UserTest(), Password = "123" };
            admin.Id = adminFacade.CreateNewAdmin(adminToken, admin);
            Assert.AreNotEqual(adminFacade.GetAdminByUserName(adminToken, admin.User_Name), null);
        }
    }
}
