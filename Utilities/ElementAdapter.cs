namespace SauceDemoTests.Utilities
{
    using OpenQA.Selenium;

    public class ElementAdapter(IWebElement element)
    {
        private readonly IWebElement element = element;

        public string Text => this.element.Text;

        public void SendKeys(string text)
        {
            this.element.SendKeys(text);
            LoggerManager.Instance?.Logger.Information($"Sent keys '{text}' to element.");
        }

        public void Clear()
        {
            this.element.SendKeys(Keys.Control + "a");
            this.element.SendKeys(Keys.Delete);
            LoggerManager.Instance!.Logger.Information("Cleared element.");
        }

        public void Click()
        {
            this.element.Click();
            LoggerManager.Instance!.Logger.Information("Clicked element.");
        }
    }
}