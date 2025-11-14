using FluentAssertions;
using OpenQA.Selenium;
using SauceDemoTests.Pages;
using SauceDemoTests.Utilities;

namespace SauceDemoTests
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver = null!;
        private LoginPage loginPage = null!;

        [ClassCleanup]
        public static void ClassCleanup()
        {
            LoggerManager.Instance!.Logger.Information("All tests completed.");
        }

        [TestInitialize]
        public void TestInit()
        {
            LoggerManager.Instance!.Logger.Information("Initializing test environment...");
        }

        [DataTestMethod]
        [DataRow("Firefox")]
        [DataRow("Edge")]
        public void UC1_TestLoginWithEmptyCredentials(string browser)
        {
            this.Initialize(browser);
            LoggerManager.Instance!.Logger.Information($"Starting UC-1 on {browser}");

            this.loginPage.EnterUsername("any_username");
            this.loginPage.EnterPassword("any_password");
            this.loginPage.ClearUsername();
            this.loginPage.ClearPassword();
            this.loginPage.ClickLogin();

            string error = this.loginPage.GetErrorMessage();
            error.Should().Contain("Username is required");

            this.Cleanup();
        }

        [DataTestMethod]
        [DataRow("Firefox")]
        [DataRow("Edge")]
        public void UC2_TestLoginWithMissingPassword(string browser)
        {
            this.Initialize(browser);
            LoggerManager.Instance!.Logger.Information($"Starting UC-2 on {browser}");

            this.loginPage.EnterUsername("any_username");
            this.loginPage.EnterPassword("any_password");
            this.loginPage.ClearPassword();
            this.loginPage.ClickLogin();

            string error = this.loginPage.GetErrorMessage();
            error.Should().Contain("Password is required");

            this.Cleanup();
        }

        [DataTestMethod]
        [DataRow("Firefox", "standard_user")]
        [DataRow("Firefox", "problem_user")]
        [DataRow("Firefox", "performance_glitch_user")]
        [DataRow("Firefox", "error_user")]
        [DataRow("Firefox", "visual_user")]
        [DataRow("Edge", "standard_user")]
        [DataRow("Edge", "problem_user")]
        [DataRow("Edge", "performance_glitch_user")]
        [DataRow("Edge", "error_user")]
        [DataRow("Edge", "visual_user")]
        public void UC3_TestSuccessfulLogin(string browser, string username)
        {
            this.Initialize(browser);
            LoggerManager.Instance!.Logger.Information($"Starting UC-3 on {browser} with username: {username}");

            this.loginPage.EnterUsername(username);
            this.loginPage.EnterPassword("secret_sauce");
            this.loginPage.ClickLogin();

            this.loginPage.IsDashboardLoaded().Should().BeTrue();
            this.loginPage.GetDashboardTitle().Should().Be("Swag Labs");

            this.Cleanup();
        }

        private void Initialize(string browser)
        {
            this.driver = BrowserFactory.CreateDriver(browser);
            this.driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            this.loginPage = new LoginPage(this.driver);
        }

        private void Cleanup()
        {
            this.driver.Quit();
        }
    }
}