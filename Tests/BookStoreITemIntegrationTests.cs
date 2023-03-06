namespace BookStore.Tests
{
    using BookStore.Constants;
    using BookStore.Models;
    using BookStore.Tests.TestData;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;

    //[Ignore]
    [TestClass]
    public class BookStoreITemIntegrationTests: BaseIntegrationTestsClass
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task Get_BookStoreItems_Return_Ok()
        {
            var response = await _client.GetAsync(Endpoints.BookStoreItems);
            var bookStoreItems = await _client.GetFromJsonAsync<List<BookStoreItem>>(Endpoints.BookStoreItems);
            Assert.IsTrue(bookStoreItems.Any(), "BookStoreItems list is empty");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET {Endpoints.BookStoreItems} is not {HttpStatusCode.OK}");
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Get_BookStoreItem_ById_Test()
        {
            var bookStoreItems = await _client.GetFromJsonAsync<List<BookStoreItem>>(Endpoints.BookStoreItems);
            var bookStoreItemFromList = bookStoreItems.First();
            var bookStoreItem = await _client.GetFromJsonAsync<BookStoreItem>(Endpoints.BookStoreItems + "/" + bookStoreItemFromList.Id);
            var response = await _client.GetAsync(Endpoints.BookStoreItems + "/" + bookStoreItemFromList.Id);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET {Endpoints.BookStoreItems}/{bookStoreItemFromList.Id} is not {HttpStatusCode.OK}");
        }
    }
}
