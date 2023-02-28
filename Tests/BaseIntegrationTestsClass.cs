namespace BookStore.Tests
{
    using BookStore.Data;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    [TestClass]
    public class BaseIntegrationTestsClass
    {
        protected HttpClient _client;

        [TestInitialize] 
            public void Init() 
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateDefaultClient();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _client.Dispose();
        }
    }
}
