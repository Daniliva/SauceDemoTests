using OpenQA.Selenium;
using SauceDemoTests.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemoTests.Utilities
{
    public class EdgeStrategy : IBrowserStrategy
    {
        public IWebDriver CreateDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            return new OpenQA.Selenium.Edge.EdgeDriver();
        }
    }
}