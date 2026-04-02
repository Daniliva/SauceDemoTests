using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoTests.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SauceDemoTests.Utilities
{
    public class ChromeDriverFactory : IWebBrowserDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            return new ChromeDriver();
        }
    }
}