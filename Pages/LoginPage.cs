using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class LoginPage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        private IWebElement UsernameInput
        {
            get
            {
                return this.driver.FindElement(By.CssSelector("#user-name"));
            }
        }

        private IWebElement PasswordInput
        {
            get
            {
                return this.driver.FindElement(By.CssSelector("#password"));
            }
        }

        private IWebElement LoginButton
        {
            get
            {
                return this.driver.FindElement(By.CssSelector("#login-button"));
            }
        }

        private IWebElement ErrorMessage
        {
            get
            {
                return this.driver.FindElement(By.CssSelector(".error-message-container h3"));
            }
        }

        public void EnterUsername(string username)
        {
            this.UsernameInput.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            this.PasswordInput.SendKeys(password);
        }

        public void ClearPassword()
        {
            this.PasswordInput.SendKeys(Keys.Control + "a");
            this.PasswordInput.SendKeys(Keys.Delete);
        }

        public void ClickLogin()
        {
            this.LoginButton.Click();
        }

        public string GetErrorMessage()
        {
            return this.ErrorMessage.Text;
        }
    }
}