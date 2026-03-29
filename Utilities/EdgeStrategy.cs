namespace SauceDemoTests.Utilities
{
    using OpenQA.Selenium;
    using SauceDemoTests.Interfaces;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;

    public class EdgeStrategy : IBrowserStrategy
    {
        public IWebDriver CreateDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            return new OpenQA.Selenium.Edge.EdgeDriver();
        }
    }
}