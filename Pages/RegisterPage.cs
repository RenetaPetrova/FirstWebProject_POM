using OpenQA.Selenium;

namespace FirstWebProject_POM.Pages
{
    public class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver) 
        {
            
        }

        public string Url => BaseUrl + "Account/Register";

        public IWebElement userNameInput => driver.FindElement(By.Id("UserName"));
        public IWebElement PasswordInput => driver.FindElement(By.Id("Password"));
        public IWebElement ConfirmPasswordInput => driver.FindElement(By.Id("ConfirmPassword"));
        public IWebElement EmailInput => driver.FindElement(By.Id("Email"));
        public IWebElement RegisterButton => driver.FindElement(By.XPath("//input[@class='btn btn-default']"));
        public IWebElement UsernameRegisterMainError => driver.FindElement(By.XPath("//ul/li[text()='The UserName field is required.']"));
        public IWebElement UsernameTooShortRegisterError => driver.FindElement(By.XPath("//ul/li[text()='The UserName must be at least 6 charecters long.']"));
        public IWebElement UsernameOnlyDigitsRegisterError => driver.FindElement(By.CssSelector("div.validation-summary-errors ul li:first-child"));
        public IWebElement PasswordEmptyRegisterError => driver.FindElement(By.XPath("//ul/li[text()='The Password field is required.']"));
        public IWebElement EmailEmptyRegisterError => driver.FindElement(By.XPath("//ul/li[text()='The Email field is required.']"));


        public void OpenRegisterPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void PerformRegistration(string newUsername, string newPassword, string newEmail)
        {
            userNameInput.Clear();
            userNameInput.SendKeys(newUsername);
            PasswordInput.Clear();
            PasswordInput.SendKeys(newPassword);
            ConfirmPasswordInput.Clear();
            ConfirmPasswordInput.SendKeys(newPassword);
            EmailInput.Clear();
            EmailInput.SendKeys(newEmail);
            RegisterButton.Click();
        }

        public void AssertRegisterAllErrorMessages()
        {
            Assert.That(UsernameRegisterMainError.Text.Trim(), Is.EqualTo("The UserName field is required."));
            Assert.That(PasswordEmptyRegisterError.Text.Trim(), Is.EqualTo("The Password field is required."));
            Assert.That(EmailEmptyRegisterError.Text.Trim(), Is.EqualTo("The Email field is required."));
        }

        public void AssertErrorUsernameTooShort()
        {
            Assert.That(UsernameTooShortRegisterError.Text.Trim(), Is.EqualTo("The UserName must be at least 6 charecters long."));
        }
        public void AssertErrorUsernameRequirenments()
        {
            Assert.That(UsernameOnlyDigitsRegisterError.Text.Trim(), 
                Is.EqualTo("Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z'). " +
                "Passwords must have at least one uppercase ('A'-'Z')."));
        }
    }
}
