using OpenQA.Selenium;

namespace SauceDemoTests.Interfaces
{
    public interface IWebBrowserDriverFactory
    {
        IWebDriver CreateDriver();
    }
}