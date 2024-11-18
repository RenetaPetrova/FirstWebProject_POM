using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstWebProject_POM.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        public string BaseUrl = "http://eaapp.somee.com/";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement HomePageLink => driver.FindElement(By.XPath("//a[text()='Home']"));
        public IWebElement AboutLink => driver.FindElement(By.XPath("//a[text()='About']"));
        public IWebElement EmployeeListLink => driver.FindElement(By.XPath("//a[text()='Employee List']"));
        public IWebElement RegisterLink => driver.FindElement(By.XPath("//a[text()='Register']"));
        public IWebElement LoginLink => driver.FindElement(By.XPath("//a[text()='Login']"));
        public IWebElement VisitNowLink => driver.FindElement(By.XPath("//a[text()='Visit now »']"));
        public IWebElement UdemyLink => driver.FindElement(By.XPath("//a[text()='Udemy']"));
        public IWebElement YoutubeLink => driver.FindElement(By.XPath("//a[text()='YouTube']"));
        public IWebElement LearnMoreLink => driver.FindElement(By.XPath("//a[text()='Learn more »']"));
        public IWebElement LogoffLink => driver.FindElement(By.XPath("//a[text()='Log off']"));
        public IWebElement GetSourceCodeLink => driver.FindElement(By.XPath("//a[text()='Get Source code »']"));
    }
}
