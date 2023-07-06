using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumDemo;


namespace SeleniumDemo
{

    public class SauceDemoTests
    {
        private static IWebDriver driver;
        private LoginPage loginPage;

        [SetUp]
        public static void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void SetUp()
        {
            loginPage = new LoginPage(driver);
            loginPage.GoToLoginPage();
        }

        [Test]
        public void InvalidUsernameInvalidPassword()
        {
            loginPage.EnterUsername("invalidusername");
            loginPage.EnterPassword("invalidpassword");
            loginPage.ClickLoginButton();
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
        }

        [Test]
        public void ValidUsernameInvalidPassword()
        {
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("invalidpassword");
            loginPage.ClickLoginButton();
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
        }

        [Test]
        public void InvalidUsernameValidPassword()
        {
            loginPage.EnterUsername("invalidusername");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClickLoginButton();
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
        }

        [Test]
        public void EmptyUsername()
        {
            loginPage.EnterUsername("");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClickLoginButton();
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Epic sadface: Username is required"));
        }

        [Test]
        public void EmptyPassword()
        {
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("");
            loginPage.ClickLoginButton();
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Epic sadface: Password is required"));
        }

        [TearDown]
        public static void CloseBrowser()
        {
            driver.Quit();
        }
    }
}