namespace BookStore.Tests
{
    using BookStore.Data;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseIntegrationTestsClass
    {
        protected HttpClient _client;

        private readonly BookStoreContext _dbContext;
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
