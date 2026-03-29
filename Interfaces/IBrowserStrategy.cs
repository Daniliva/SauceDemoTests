namespace SauceDemoTests.Interfaces
{
    using OpenQA.Selenium;

    public interface IBrowserStrategy
    {
        IWebDriver CreateDriver();
    }
}
