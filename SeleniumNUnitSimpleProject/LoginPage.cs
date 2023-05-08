using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitSimpleProject
{
    public class LoginPage : BasePage
    {
        IWebDriver driver = null;
        By pageTitle = By.ClassName("login_logo");
        By usernameField = By.Id("user-name");
        By passwordField = By.Id("password");
        By loginButton = By.Id("login-button");
        By errorMessage = By.XPath("//*[@id=\"login_button_container\"]/div/form/div[3]/h3");

        public LoginPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        //Wait until username field is visible, set username, set password and click login
        public void Login(string username, string password)
        {
            WaitForElementToBeVisible(usernameField);
            driver.FindElement(usernameField).SendKeys(username);
            driver.FindElement(passwordField).SendKeys(password);
            driver.FindElement(loginButton).Click();
        }
        //Check if error message is dispayed
        public bool IsErrorMessageDisplayed()
        {
            try
            {
                return driver.FindElement(errorMessage).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        //Retrieve the text of the error message
        public string GetErrorMessage()
        {
            return driver.FindElement(errorMessage).Text;
        }
        //Retrieve the text of the page title
        public string GetLoginPageTitle()
        {
            return driver.FindElement(pageTitle).Text;
        }

        public bool IsDisplayed(By element)
        {
            return driver.FindElement(element).Displayed;
        }
    }
}
