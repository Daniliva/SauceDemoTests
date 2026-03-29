namespace SauceDemoTests.Utilities
{
    using OpenQA.Selenium;
    using SauceDemoTests.Interfaces;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;

    public class FirefoxStrategy : IBrowserStrategy
    {
        public IWebDriver CreateDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new OpenQA.Selenium.Firefox.FirefoxDriver();
        }
    }
}