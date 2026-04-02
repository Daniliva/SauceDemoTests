using OpenQA.Selenium;
using SauceDemoTests.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemoTests.Utilities
{
    public class FirefoxDriverFactory : IWebBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new OpenQA.Selenium.Firefox.FirefoxDriver();
        }
    }
}