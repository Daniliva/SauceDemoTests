using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoTests.Core.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SauceDemoTests.Core.Driver
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