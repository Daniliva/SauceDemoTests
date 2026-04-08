using OpenQA.Selenium;

namespace SauceDemoTests.Core.Driver
{
    public sealed class DriverSingleton
    {
        private static readonly ThreadLocal<IWebDriver> DriverHolder = new ThreadLocal<IWebDriver>();

        private DriverSingleton() { }

        public static IWebDriver GetDriver(string browser = "chrome")
        {
            if (DriverHolder.Value == null)
            {
                DriverHolder.Value = BrowserFactory.CreateDriver(browser);
            }

            return DriverHolder.Value;
        }

        public static void QuitDriver()
        {
            if (DriverHolder.Value != null)
            {
                DriverHolder.Value.Quit();
                DriverHolder.Value.Dispose();
                DriverHolder.Value = null!;
            }
        }
    }
}