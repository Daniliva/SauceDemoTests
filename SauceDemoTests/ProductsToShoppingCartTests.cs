using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemoTests.Business.Pages;
using SauceDemoTests.Core.Configuration;
using SauceDemoTests.Core.Logging;

namespace SauceDemoTests.Tests
{
    [TestFixtureSource(typeof(ConfigReader), nameof(ConfigReader.GetBrowsers))]
    public class ShoppingCartTests
    {
        private readonly string browser;

        private LoginPage loginPage = null!;
        private InventoryPage inventoryPage = null!;
        private ProductDetailsPage productDetailsPage = null!;

        public ShoppingCartTests(string browser)
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
        public void UC3_TestAddingProducts(UserModel user)
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