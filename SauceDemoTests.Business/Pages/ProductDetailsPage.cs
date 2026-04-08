using OpenQA.Selenium;

namespace SauceDemoTests.Business.Pages
{
    public class ProductDetailsPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        private IWebElement AddToCartButton
        {
            get
            {
                return this.driver.FindElement(By.CssSelector(".inventory_details_container button[data-test^='add-to-cart']"));
            }
        }

        public void ClickAddToCart()
        {
            this.AddToCartButton.Click();
        }
    }
}
