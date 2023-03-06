namespace BookStore.Tests
{
    using BookStore.Tests.Helpers;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseIntegrationTestsClass
    {
        protected static CustomWebApplicationFactory<Program> _factory;
        protected static HttpClient _client;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            if (_factory == null)
            {
              _factory = new CustomWebApplicationFactory<Program>();
            }

            if (_client == null)
            {
                _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = true
                });
            }
        }
    }
}
