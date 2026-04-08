using System.Collections.Generic;
using OpenQA.Selenium;

namespace SauceDemoTests.Business.Pages
{
    public class InventoryPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        public IWebElement BurgerMenu
        {
            get
            {
                return this.driver.FindElement(By.CssSelector("#react-burger-menu-btn"));
            }
        }

        public IWebElement AppLogo
        {
            get
            {
                return this.driver.FindElement(By.CssSelector(".app_logo"));
            }
        }

        public IWebElement ShoppingCartIcon
        {
            get
            {
                return this.driver.FindElement(By.CssSelector(".shopping_cart_link"));
            }
        }

        public IWebElement SortDropdown
        {
            get
            {
                return this.driver.FindElement(By.CssSelector(".product_sort_container"));
            }
        }

        public IReadOnlyCollection<IWebElement> InventoryItems
        {
            get
            {
                return this.driver.FindElements(By.CssSelector(".inventory_item"));
            }
        }

        public void OpenAnyProductDetails()
        {
            this.driver.FindElement(By.CssSelector(".inventory_item_name")).Click();
        }

        public string GetCartBadgeCount()
        {
            var badges = this.driver.FindElements(By.CssSelector(".shopping_cart_badge"));
            return badges.Count > 0 ? badges[0].Text : "0";
        }
    }
}