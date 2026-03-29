namespace SauceDemoTests.Pages
{
    using OpenQA.Selenium;

    public class InventoryPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        public IWebElement BurgerMenu => this.driver.FindElement(By.CssSelector("#react-burger-menu-btn"));

        public IWebElement AppLogo => this.driver.FindElement(By.CssSelector(".app_logo"));

        public IWebElement ShoppingCartIcon => this.driver.FindElement(By.CssSelector(".shopping_cart_link"));

        public IWebElement SortDropdown => this.driver.FindElement(By.CssSelector(".product_sort_container"));

        public IReadOnlyCollection<IWebElement> InventoryItems => this.driver.FindElements(By.CssSelector(".inventory_item"));

        public void OpenAnyProductDetails()
        {
            var firstProduct = this.driver.FindElement(By.CssSelector(".inventory_item_name"));
            firstProduct.Click();
        }

        public string GetCartBadgeCount()
        {
            return this.driver.FindElement(By.CssSelector(".shopping_cart_badge")).Text;
        }
    }
}