namespace BookStore.Tests
{
    using BookStore.Constants;
    using BookStore.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;

    [TestClass]
    public class BookingsIntegrationTests : BaseIntegrationTestsClass
    {

        [TestMethod]
        [TestCategory("Integration")]
        public async Task Get_Bookings_Integration_Return_Ok()
        {
            var response = await _client.GetAsync(Endpoints.Bookings);
            var bookings = await _client.GetFromJsonAsync<List<Booking>>(Endpoints.Bookings);
            Assert.IsTrue(bookings.Any(), "Bookings list is empty");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET api/Bookings is not {HttpStatusCode.OK}");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task Get_Booking_ById_Integration_Test()
        {
            var bookings = await _client.GetFromJsonAsync<List<Booking>>(Endpoints.Bookings);
            var bookingFromList = bookings.First();
            var booking = await _client.GetFromJsonAsync<Booking>(Endpoints.Bookings + "/" + bookingFromList.Id);
            var response = await _client.GetAsync(Endpoints.Bookings + "/" + bookingFromList.Id);

            Assert.AreEqual(bookingFromList.Delivery_Adress, booking.Delivery_Adress);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET api/Bookings/{bookingFromList.Id} is not {HttpStatusCode.OK}");
        }
    }
}
