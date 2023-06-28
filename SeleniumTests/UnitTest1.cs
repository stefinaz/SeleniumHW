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
        public void InvalidLogin()
        {
            IWebElement usernameInput = driver.FindElement(By.Id("user-name"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            usernameInput.SendKeys("invaliduser");
            passwordInput.SendKeys("invalidpass");
            loginButton.Click();

            IWebElement errorMessage = WaitUntilElementIsVisible(By.CssSelector(".error-message-container.error"));

            
            Assert.That(errorMessage.Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

            
        }


        [Test]
        public void ValidUsernameInvalidPassword()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("invalidpass");
            driver.FindElement(By.Id("login-button")).Click();

            IWebElement errorMessage = WaitUntilElementIsVisible(By.CssSelector(".error-message-container.error"));
            Assert.That(errorMessage.Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

            
        }

        [Test]
        public void InvalidUsernameValidPassword()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("invalidusername");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            IWebElement errorMessage = WaitUntilElementIsVisible(By.CssSelector(".error-message-container.error"));

            Assert.That(errorMessage.Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));

           
        }

        [Test]
        public void EmptyUsername()
        {
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            IWebElement errorMessage = WaitUntilElementIsVisible(By.CssSelector(".error-message-container.error"));
            Assert.That(errorMessage.Text, Is.EqualTo("Epic sadface: Username is required"));
           
            

        }

        [Test]
        public void EmptyPassword()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("login-button")).Click();

            String errorMessage = driver.FindElement(By.CssSelector(".error-message-container.error")).Text;
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Password is required"));
            
            

        }

        private IWebElement WaitUntilElementIsVisible(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        [TearDown]
        public static void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
