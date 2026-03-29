namespace SauceDemoTests.Pages
{
    using OpenQA.Selenium;

    public class LoginPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        private IWebElement UsernameInput => this.driver.FindElement(By.CssSelector("#user-name"));

        private IWebElement PasswordInput => this.driver.FindElement(By.CssSelector("#password"));

        private IWebElement LoginButton => this.driver.FindElement(By.CssSelector("#login-button"));

        private IWebElement ErrorMessage => this.driver.FindElement(By.CssSelector(".error-message-container h3"));

        public void EnterUsername(string username) => this.UsernameInput.SendKeys(username);

        public void EnterPassword(string password) => this.PasswordInput.SendKeys(password);

        public void ClearPassword()
        {
            this.PasswordInput.SendKeys(Keys.Control + "a");
            this.PasswordInput.SendKeys(Keys.Delete);
        }

        public void ClickLogin() => this.LoginButton.Click();

        public string GetErrorMessage() => this.ErrorMessage.Text;
    }
}