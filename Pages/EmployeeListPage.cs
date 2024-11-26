using OpenQA.Selenium;

namespace FirstWebProject_POM.Pages
{
    public class EmployeeListPage : BasePage
    {
        public EmployeeListPage(IWebDriver driver) : base(driver)
        {

        }

        public string Url => BaseUrl + "Employee";
        public IWebElement BenefitsButton => driver.FindElement(By.XPath("//a[text()='Benefits']"));
        public IWebElement EditButton => driver.FindElement(By.XPath("//a[text()='Delete']"));
        public IWebElement DeleteButton => driver.FindElement(By.XPath("//a[text()='Edit']"));
        public IWebElement SearchField => driver.FindElement(By.XPath("//input[@name='searchTerm']"));
        public IWebElement SearchButton => driver.FindElement(By.XPath("//input[@value='Search']"));



        public void OpenEmployeeListPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void PerformSearch(string searchWord)
        {
            SearchField.Clear();
            SearchField.SendKeys(searchWord);
            SearchButton.Click();
        }
    }
}
