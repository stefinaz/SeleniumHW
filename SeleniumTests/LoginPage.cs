using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumDemo
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly By usernameInputLocator = By.Id("user-name");
        private readonly By passwordInputLocator = By.Id("password");
        private readonly By loginButtonLocator = By.Id("login-button");
        private readonly By errorMessageLocator = By.CssSelector(".error-message-container.error");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoToLoginPage()
        {
            driver.Url = "https://www.saucedemo.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public void EnterUsername(string username)
        {
            driver.FindElement(usernameInputLocator).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(passwordInputLocator).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            driver.FindElement(loginButtonLocator).Click();
        }

        public string GetErrorMessage()
        {
            var errorMessage = WaitUntilElementIsVisible(errorMessageLocator);
            return errorMessage.Text;
        }

        private IWebElement WaitUntilElementIsVisible(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }
    }
}