using OpenQA.Selenium;
using SauceDemoTests.Utilities;

namespace SauceDemoTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private ElementAdapter Username => new ElementAdapter(this.driver.FindElement(By.CssSelector("#user-name")));

        private ElementAdapter Password => new ElementAdapter(this.driver.FindElement(By.CssSelector("#password")));

        private ElementAdapter LoginButton => new ElementAdapter(this.driver.FindElement(By.CssSelector("#login-button")));

        private ElementAdapter ErrorMessage => new ElementAdapter(this.driver.FindElement(By.CssSelector(".error-message-container")));

        public void EnterUsername(string user) => this.Username.SendKeys(user);

        public void ClearUsername() => this.Username.Clear();

        public void EnterPassword(string pass) => this.Password.SendKeys(pass);

        public void ClearPassword() => this.Password.Clear();

        public void ClickLogin() => this.LoginButton.Click();

        public string GetErrorMessage() => this.ErrorMessage.Text;

        public string GetDashboardTitle() => new ElementAdapter(this.driver.FindElement(By.CssSelector(".app_logo"))).Text;

        public bool IsDashboardLoaded() => this.driver.Url.Contains("inventory.html");
    }
}
