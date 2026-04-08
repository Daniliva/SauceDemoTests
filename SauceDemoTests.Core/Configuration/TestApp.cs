using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using SauceDemoTests.Business.Pages;
using SauceDemoTests.Core.Driver;
using SauceDemoTests.Core.Logging;


namespace SauceDemoTests.Core.Configuration
{
    public static class TestApp
    {
        private static readonly ThreadLocal<ServiceProvider> Provider = new ThreadLocal<ServiceProvider>();

        public static void Start(string browser)
        {
            LoggerManager.Instance!.Logger.Information($"Initializing TestApp for browser: {browser}");
            var services = new ServiceCollection();

            services.AddScoped<IWebDriver>(p =>
            {
                IWebDriver driver = DriverSingleton.GetDriver(browser);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
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
                LoggerManager.Instance!.Logger.Information("Stopping TestApp and cleaning up..."); 
                
                DriverSingleton.QuitDriver();

                Provider.Value.Dispose();
                Provider.Value = null!;
            }
        }
    }
}