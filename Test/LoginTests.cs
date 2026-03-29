using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemoTests.Pages;
using SauceDemoTests.Utilities;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
[assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

namespace SauceDemoTests.Test
{
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    public class LoginTests(string browser)
    {
        private readonly string browser = browser;
        private IWebDriver driver = null!;

        [SetUp]
        public void Setup()
        {
            this.driver = DriverSingleton.GetDriver(this.browser);
            this.driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            TestContext.Progress.WriteLine($"[INFO] Navigated to SauceDemo using {this.browser}");
        }

        [TearDown]
        public void TearDown()
        {
            DriverSingleton.QuitDriver();
            TestContext.Progress.WriteLine($"[INFO] Browser {this.browser} closed");
        }

        [TestCase("standard_user", "any_password", "Epic sadface: Password is required")]
        public void UC1_TestLoginWithOnlyUsername(string username, string password, string expectedError)
        {
            var loginPage = new LoginPage(this.driver);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearPassword();
            loginPage.ClickLogin();

            loginPage.GetErrorMessage().Should().Be(expectedError);
        }

        [TestCase("standard_user", "secret_sauce")]
        public void UC2_TestLoginWithValidCredentials(string username, string password)
        {
            var loginPage = new LoginPage(this.driver);
            var inventoryPage = new InventoryPage(this.driver);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin();

            inventoryPage.BurgerMenu.Displayed.Should().BeTrue();
            inventoryPage.AppLogo.Text.Should().Be("Swag Labs");
            inventoryPage.ShoppingCartIcon.Displayed.Should().BeTrue();
            inventoryPage.SortDropdown.Displayed.Should().BeTrue();
            inventoryPage.InventoryItems.Should().NotBeEmpty();
        }

        [TestCase("standard_user", "secret_sauce")]
        public void UC3_TestAddingProductsToShoppingCart(string username, string password)
        {
            var loginPage = new LoginPage(this.driver);
            var inventoryPage = new InventoryPage(this.driver);
            var productDetailsPage = new ProductDetailsPage(this.driver);

            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin();

            inventoryPage.OpenAnyProductDetails();
            productDetailsPage.ClickAddToCart();

            inventoryPage.GetCartBadgeCount().Should().Be("1");
        }
    }
}