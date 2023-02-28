namespace BookStore.Tests
{
using BookStore.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

    [TestClass]
    public class IntegrationUIBaseTest
    {
        protected CustomWebAplicationFactory _factory;
        protected HttpClient _client;
        protected ChromeDriver _driver;


        [TestInitialize]
        public void TestsInit()
        {
            var _factory = new CustomWebAplicationFactory();
            _client = _factory.CreateDefaultClient();

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://localhost:7137/");
            _driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Quit();
            _driver.Dispose();
            _client.Dispose();
        }
    }
}
