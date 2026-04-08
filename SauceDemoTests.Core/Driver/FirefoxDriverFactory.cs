using OpenQA.Selenium;
using SauceDemoTests.Core.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemoTests.Core.Driver
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