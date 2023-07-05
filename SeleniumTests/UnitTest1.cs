using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumDemo
{
    public class SauceDemoTests
    {
        private static IWebDriver driver;
       

        [SetUp]
        public static void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.saucedemo.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }

       

        [Test]
        public void InvalidUsernameInvalidPassword()
        {

            Login("invalidusername", "invalidpassword");
            
            Assert.That(GetErrorMessage(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

            
        }


        [Test]
        public void ValidUsernameInvalidPassword()
        {
            Login("standard_user", "invalidpassword");

            Assert.That(GetErrorMessage(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

            
        }

        [Test]
        public void InvalidUsernameValidPassword()
        {
            Login("invalidusername", "secret_sauce");

            Assert.That(GetErrorMessage(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

           
        }

        [Test]
        public void EmptyUsername()
        {
            
            Login("", "secret_sauce");

            Assert.That(GetErrorMessage(), Is.EqualTo("Epic sadface: Username is required"));
           
            

        }

        [Test]
        public void EmptyPassword()
        {
            

            Login("standard_user", "");

            Assert.That(GetErrorMessage(), Is.EqualTo("Epic sadface: Password is required"));

            
            

        }


        private IWebElement WaitUntilElementIsVisible(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void Login(string username, string password)
        {
            IWebElement usernameInput = driver.FindElement(By.Id("user-name"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            usernameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            loginButton.Click();


        }

        private string GetErrorMessage()
        {
            IWebElement errorMessage = WaitUntilElementIsVisible(By.CssSelector(".error-message-container.error"));
            return errorMessage.Text;
        }

        [TearDown]
        public static void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
