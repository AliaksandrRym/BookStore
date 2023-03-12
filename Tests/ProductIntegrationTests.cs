namespace BookStore.Tests
{
    using BookStore.Constants;
    using BookStore.Models;
    using BookStore.Tests.Helpers;
    using BookStore.Tests.TestData;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;

    [TestClass]
    public class ProductIntegrationTests: BaseIntegrationTestsClass
    {

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Get_Products_Return_Ok()
        {
            var response = await _client.GetAsync(Endpoints.Products);
            var products = await _client.GetFromJsonAsync<List<Product>>(Endpoints.Products);
            Assert.IsTrue(products.Any(), "Products list is empty");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET api/Products is not {HttpStatusCode.OK}");
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Get_Products_ById_Test()
        {
            var products = await _client.GetFromJsonAsync<List<Product>>(Endpoints.Products);
            var productFromList = products.First();
            var product = await _client.GetFromJsonAsync<Product>(Endpoints.Products + "/" + productFromList.Id);
            var response = await _client.GetAsync(Endpoints.Products + "/" + productFromList.Id);

            Assert.AreEqual(productFromList.Name, product.Name);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET api/Products/{productFromList.Id} is not {HttpStatusCode.OK}");
        }
    }
}
