using FirstWebProject_POM.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstWebProject_POM.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        public LoginPage loginPage;
        public RegisterPage registerPage;
        public BasePage basePage;
        public AboutPage aboutPage;
        public EmployeeListPage employeeListPage;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var chromeOprions = new ChromeOptions();
            chromeOprions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOprions.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(chromeOprions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Clear cookies to ensure no session persists
            driver.Manage().Cookies.DeleteAllCookies();

            //initiate the additional pages
            loginPage = new LoginPage(driver);
            registerPage = new RegisterPage(driver);
            basePage = new BasePage(driver);
            aboutPage = new AboutPage(driver);
            employeeListPage = new EmployeeListPage(driver);


            loginPage.OpenLoginPage();
            loginPage.PerformLogin("TestUser", "Test123!");

        }

        [OneTimeTearDown]
        public void OneTimeTearDown() 
        {
            driver.Quit();
            driver.Close();
        }
    }
}
