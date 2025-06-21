using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace SeleniumLoginTest
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver;
        private string baseUrl;

        [SetUp]
        public void Setup()
        {
            // Set up ChromeDriver. 
            driver = new ChromeDriver();
            baseUrl = "YOUR_LOGIN_PAGE_URL"; 
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Implicit wait
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void SuccessfulLoginTest()
        {
            driver.Navigate().GoToUrl(baseUrl);

            // Locate username and password fields and enter credentials
            IWebElement usernameField = driver.FindElement(By.Id("username")); // Replace with the actual ID or other locator
            IWebElement passwordField = driver.FindElement(By.Id("password")); // Replace with the actual ID or other locator
            IWebElement loginButton = driver.FindElement(By.Id("loginButton")); // Replace with the actual ID or other locator

            usernameField.SendKeys("YOUR_USERNAME"); // Replace with your username
            passwordField.SendKeys("YOUR_PASSWORD"); // Replace with your password
            loginButton.Click();

            // Verification: Check for a successful login indicator (e.g., a welcome message, a specific element on the logged-in page)
            try
            {
                IWebElement welcomeMessage = driver.FindElement(By.Id("welcomeMessage")); // Replace with the actual ID or other locator of the element that appears after succesful login.
                Assert.IsTrue(welcomeMessage.Displayed);
                Console.WriteLine("Login successful!");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Login failed: Welcome message not found.");
                Console.WriteLine("Login failed: Welcome message not found.");
            }
        }

        [Test]
        public void FailedLoginTest()
        {
            driver.Navigate().GoToUrl(baseUrl);

            IWebElement usernameField = driver.FindElement(By.Id("username"));
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("loginButton"));

            usernameField.SendKeys("invalid_username");
            passwordField.SendKeys("invalid_password");
            loginButton.Click();

            // Verification: Check for an error message or other indication of a failed login
            try
            {
                IWebElement errorMessage = driver.FindElement(By.Id("errorMessage")); // Replace with the actual ID or other locator
                Assert.IsTrue(errorMessage.Displayed);
                Console.WriteLine("Login failed as expected.");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Login did not fail as expected: Error message not found.");
                Console.WriteLine("Login did not fail as expected: Error message not found.");
            }
        }
    }
}