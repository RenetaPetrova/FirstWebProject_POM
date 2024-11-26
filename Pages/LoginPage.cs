using OpenQA.Selenium;

namespace FirstWebProject_POM.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public string Url => BaseUrl + "Account/Login";

        public IWebElement Username => driver.FindElement(By.Id("UserName"));
        public IWebElement Password => driver.FindElement(By.Id("Password"));
        public IWebElement LogInButton => driver.FindElement(By.XPath("//input[@class='btn btn-default']"));
        public IWebElement RegistrationLink => driver.FindElement(By.XPath("//p/a[text()='Register as a new user']"));
        public IWebElement UsernameErrorMessage => driver.FindElement(By.XPath("(//span[@class='text-danger field-validation-error'])[1]"));
        public IWebElement PasswordErrorMessage => driver.FindElement(By.XPath("(//span[@class='text-danger field-validation-error'])[2]"));
        public IWebElement InvalidLoginErrorMessage => driver.FindElement(By.XPath("(//ul/li[text()='Invalid login attempt.'])"));



        public void OpenLoginPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        

        public void PerformLogin(string username, string password)
        {
            Username.Clear();
            Username.SendKeys(username);
            Password.Clear();
            Password.SendKeys(password);
            LogInButton.Click();
        }

        public void AssertLoginErrorMessages()
        {
            Assert.That(UsernameErrorMessage.Text.Trim(), Is.EqualTo("The UserName field is required."));
            Assert.That(PasswordErrorMessage.Text.Trim(), Is.EqualTo("The Password field is required."));
        }

    }
}
