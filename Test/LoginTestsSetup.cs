using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemoTests.Configuration;
using SauceDemoTests.Pages;
using SauceDemoTests.Utilities;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(8)]

namespace SauceDemoTests.Test
{
    [TestFixtureSource(typeof(ConfigReader), nameof(ConfigReader.GetBrowsers))]
    public class LoginTests
    {
        private readonly string browser;

        private LoginPage loginPage = null!;
        private InventoryPage inventoryPage = null!;
        private ProductDetailsPage productDetailsPage = null!;

        public LoginTests(string browser)
        {
            this.browser = browser;
        }

        [SetUp]
        public void SetUp()
        {
            TestApp.Start(this.browser);
            TestApp.Get<IWebDriver>().Navigate().GoToUrl(ConfigReader.BaseUrl);

            this.loginPage = TestApp.Get<LoginPage>();
            this.inventoryPage = TestApp.Get<InventoryPage>();
            this.productDetailsPage = TestApp.Get<ProductDetailsPage>();
        }

        [TearDown]
        public void TearDown()
        {
            TestApp.Stop();
        }

        [TestCaseSource(typeof(ConfigReader), nameof(ConfigReader.GetUsers))]
        public void UC1_TestLoginWithOnlyUsername(UserModel user)
        {
            LoggerManager.Instance!.Logger.Information($"[UC-1] Running for user: {user.Username} on {this.browser}");

            this.loginPage.EnterUsername(user.Username);
            this.loginPage.EnterPassword(user.Password);
            this.loginPage.ClearPassword();
            this.loginPage.ClickLogin();

            this.loginPage.GetErrorMessage().Should().Be("Epic sadface: Password is required");
        }

        [TestCaseSource(typeof(ConfigReader), nameof(ConfigReader.GetUsers))]
        public void UC2_TestLoginWithValidCredentials(UserModel user)
        {
            LoggerManager.Instance!.Logger.Information($"[UC-2] Running for user: {user.Username} on {this.browser}");

            this.loginPage.EnterUsername(user.Username);
            this.loginPage.EnterPassword(user.Password);
            this.loginPage.ClickLogin();

            if (user.IsLockedOut)
            {
                this.loginPage.GetErrorMessage().Should().Contain("locked out");
            }
            else
            {
                this.inventoryPage.BurgerMenu.Displayed.Should().BeTrue();
                this.inventoryPage.AppLogo.Text.Should().Be("Swag Labs");
                this.inventoryPage.ShoppingCartIcon.Displayed.Should().BeTrue();
                this.inventoryPage.SortDropdown.Displayed.Should().BeTrue();
                this.inventoryPage.InventoryItems.Should().NotBeEmpty();
            }
        }

        [TestCaseSource(typeof(ConfigReader), nameof(ConfigReader.GetUsers))]
        public void UC3_TestAddingProductsToShoppingCart(UserModel user)
        {
            if (user.IsLockedOut)
            {
                return;
            }

            LoggerManager.Instance!.Logger.Information($"[UC-3] Running for user: {user.Username} on {this.browser}");

            this.loginPage.EnterUsername(user.Username);
            this.loginPage.EnterPassword(user.Password);
            this.loginPage.ClickLogin();

            this.inventoryPage.OpenAnyProductDetails();
            this.productDetailsPage.ClickAddToCart();

            this.inventoryPage.GetCartBadgeCount().Should().Be(user.ExpectedBadge);
        }
    }
}