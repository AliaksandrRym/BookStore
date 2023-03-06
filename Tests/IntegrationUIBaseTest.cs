namespace BookStore.Tests
{
using BookStore.Tests.Helpers;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium.Chrome;

    [TestClass]
    public class IntegrationUIBaseTest: BaseIntegrationTestsClass
    {
        protected ChromeDriver _driver;


        [TestInitialize]
        public void TestsInit()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://localhost:7137/");
            _driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
