﻿using FirstWebProject_POM.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstWebProject_POM.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        public LoginPage loginPage;
        public RegisterPage registerPage;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var chromeOprions = new ChromeOptions();
            chromeOprions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOprions.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(chromeOprions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //initiate the additional pages
            loginPage = new LoginPage(driver);
            registerPage = new RegisterPage(driver);

            loginPage.OpenLoginPage();
            loginPage.PerformLogin("lambda", "Test123!");

        }

        [OneTimeTearDown]
        public void OneTimeTearDown() 
        {
            driver.Quit();
            driver.Close();
        }
    }
}