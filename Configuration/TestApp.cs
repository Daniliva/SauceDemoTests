using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SauceDemoTests.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SauceDemoTests.Configuration
{
    public static class TestApp
    {
        private static readonly ThreadLocal<ServiceProvider> Provider = new ThreadLocal<ServiceProvider>();

        public static void Start(string browser)
        {
            var services = new ServiceCollection();

            services.AddScoped<IWebDriver>(p =>
            {
                IWebDriver driver;
                if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
                {
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    driver = new ChromeDriver();
                }
                else
                {
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                }

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.Manage().Window.Maximize();
                return driver;
            });

            services.AddScoped<LoginPage>();
            services.AddScoped<InventoryPage>();
            services.AddScoped<ProductDetailsPage>();

            Provider.Value = services.BuildServiceProvider();
        }

        public static T Get<T>()
            where T : notnull
        {
            return Provider.Value!.GetRequiredService<T>();
        }

        public static void Stop()
        {
            if (Provider.Value != null)
            {
                var driver = Provider.Value.GetService<IWebDriver>();
                driver?.Quit();
                driver?.Dispose();

                Provider.Value.Dispose();
                Provider.Value = null!;
            }
        }
    }
}