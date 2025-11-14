using OpenQA.Selenium;
using SauceDemoTests.Interfaces;

namespace SauceDemoTests.Utilities
{
    public static class BrowserFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            IBrowserStrategy strategy = browser switch
            {
                "Firefox" => new FirefoxStrategy(),
                "Edge" => new EdgeStrategy(),
                _ => throw new System.Exception("Invalid browser specified.")
            };
            return strategy.CreateDriver();
        }
    }
}