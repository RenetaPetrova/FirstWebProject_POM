using OpenQA.Selenium;

namespace FirstWebProject_POM.Pages
{
    public class AboutPage : BasePage
    {
        public AboutPage(IWebDriver driver) : base(driver)
        {

        }

        public string Url => BaseUrl + "Home/About";
        public IWebElement PageHeading => driver.FindElement(By.XPath("//div[@class='container body-content']/h2[text()='About.']"));
        public IWebElement PageContent => driver.FindElement(By.XPath("//p[text()='ExecuteAutomation Employee Application v1.0 is a simple web application for showing very few functionality of Employee details.']"));
        public IWebElement PageFooter => driver.FindElement(By.XPath("//p[text()='© 2024 - Powered by ExecuteAutomation.com']"));



        public void OpenAboutPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
