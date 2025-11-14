using OpenQA.Selenium;

namespace SauceDemoTests.Interfaces
{
    public interface IBrowserStrategy
    {
        IWebDriver CreateDriver();
    }
}
