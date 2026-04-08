using OpenQA.Selenium;
using SauceDemoTests.Core.Interfaces;
using SauceDemoTests.Core.Logging;

namespace SauceDemoTests.Core.Driver
{
    public static class BrowserFactory
    {
        public static IWebDriver CreateDriver(string browserName)
        {
            LoggerManager.Instance!.Logger.Information($"[Factory] Initializing specific factory for: {browserName}");

            IWebBrowserDriverFactory factory = browserName.ToLower() switch
            {
                "chrome" => new ChromeDriverFactory(),
                "firefox" => new FirefoxDriverFactory(),
                _ => throw new System.ArgumentException($"Browser '{browserName}' is not supported.")
            };
            return factory.CreateDriver();
        }
    }
}