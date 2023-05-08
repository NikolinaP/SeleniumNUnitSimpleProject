using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitSimpleProject
{
    internal class LoginTests
    {


        static IWebDriver driver = new ChromeDriver();
        LoginPage loginPage = new LoginPage(driver);

        [SetUp]
        public void Setup()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);
        }
        [Test]
        public void LoginPageTitleCorrect()
        {
            string loginPageTitle = loginPage.GetLoginPageTitle();
            //Assert that login page title is correct
            Assert.IsTrue(loginPageTitle.Contains("Swag Labs"));
        }
        [Test]
        public void LoginWithValidCredentials()
        {
            loginPage.Login("standard_user", "secret_sauce");
            //Assert that entering valid credentials lead to correct page by checking the url
            Assert.IsTrue(driver.Url.Contains("/inventory"));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            loginPage.Login("myinvaliduser", "myinvalidpass");
            //Assert that error message is displayed when invalid credentials are entered
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());

        }
        [Test]
        public void LoginWithEmptyUserName()
        {
            loginPage.Login("", "secret_sauce");
            //Assert that correct error message is displayed when username is not entered
            Assert.IsTrue(loginPage.GetErrorMessage().Contains("Epic sadface: Username is required"));
        }
        [Test]
        public void LoginWithEmptyPassword()
        {
            loginPage.Login("standard_user", "");
            //Assert that correct error message is displayed when password is not entered
            Assert.IsTrue(loginPage.GetErrorMessage().Contains("Epic sadface: Password is required"));
        }
        [Test]
        public void LoginWithIncorrectUserName()
        {
            loginPage.Login("invaliduser", "secret_sauce");
            //Assert that error message is displayed when invalid credentials are entered
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }
        [Test]
        public void LoginWithIncorrectPassword()
        {
            loginPage.Login("standard_user", "incorrect");
            //Assert that error message is displayed when invalid password is entered
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }
        [Test]
        public void LoginWithCaseSensitiveUserName()
        {
            loginPage.Login("STaNDaRD_usER", "secret_sauce");
            //Assert that error message is displayed when username is entered with different capitalization
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }
        [Test]
        public void LoginWithCaseSensitivePassword()
        {
            loginPage.Login("standard_user", "SECREt_Sauce");
            //Assert that error message is displayed when password is entered with different capitalization
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }
        [Test]
        public void LoginWithSpecialCharacters()
        {
            loginPage.Login("standard^_user", "$ecre&t_~ser");
            //Assert that error message is displayed when username and password are entered with special characters
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }
        [Test]
        public void LoginWithWhitespace()
        {
            loginPage.Login("   standard user", "secret sauce  ");
            //Assert that error message is displayed when entered username and password with whitespace character
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }
        [Test]
        public void LoginWithLockedAccount()
        {
            loginPage.Login("locked_out_user", "secret_sauce");
            //Assert that error message is displayed when account is locked
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }
        [Test]
        public void LockedUserErrorMessageCorrect()
        {
            loginPage.Login("locked_out_user", "secret_sauce");
            //Assert that correct error message is displayed when account is locked
            Assert.IsTrue(loginPage.GetErrorMessage().Contains("Epic sadface: Sorry, this user has been locked out."));
        }
        [Test]
        public void InvalidUsernameAndPasswordErrorMessageCorrect()
        {
            loginPage.Login("invalid_user", "invalid_pass");
            //Assert that correct error message is displayed when invalid credentials are entered
            Assert.IsTrue(loginPage.GetErrorMessage().Contains("Epic sadface: Username and password do not match any user in this service"));
        }
        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }
    }
}
