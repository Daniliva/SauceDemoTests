using OpenQA.Selenium;

namespace SauceDemoTests.Core.Interfaces
{
    public interface IWebBrowserDriverFactory
    {
        IWebDriver CreateDriver();
    }
}