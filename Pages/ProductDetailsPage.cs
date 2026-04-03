using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class ProductDetailsPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        private IWebElement AddToCartButton
        {
            get
            {
                return this.driver.FindElement(By.CssSelector("button[data-test^='add-to-cart']"));
            }
        }

        public void ClickAddToCart()
        {
            this.AddToCartButton.Click();
        }
    }
}
