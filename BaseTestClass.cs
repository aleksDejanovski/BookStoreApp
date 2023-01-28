using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace Book_Store_app
{
    public class BaseTestClass
    {
        public IWebDriver driver;
        public WebDriverWait Wait;
        
        public string username = "user";
        public string pass = "aA123456789!";
        internal MainPage pageMain;
        internal LoginPage pageLogin;
        internal RegistrationPage pageRegister;
        internal ProfilePage pageProf;
        internal Actions actions;
        


        [SetUp]
        public void SetUpMethod()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
            pageMain = new MainPage(driver);
            pageLogin = new LoginPage(driver);
            pageRegister = new RegistrationPage(driver);
            pageProf = new ProfilePage(driver);
            actions = new Actions(driver);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

        }

        [TearDown]
        public void TearDownMethod()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}