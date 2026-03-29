namespace SauceDemoTests.Pages
{
    using OpenQA.Selenium;

    public class ProductDetailsPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        private IWebElement AddToCartButton => this.driver.FindElement(By.CssSelector(".btn_inventory"));

        public void ClickAddToCart() => this.AddToCartButton.Click();
    }
}
