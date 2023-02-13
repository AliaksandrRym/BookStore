namespace BookStore.UnitTests
{
    using AutoFixture;
    using BookStore.Controllers;
    using BookStore.Interfaces;
    using BookStore.Properties.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class BookingControllerUnitTests: BaseTests
    {
        private Mock<IBookingService> _bookingServiceMock;
        private BookingsController? _bookingController;

        public BookingControllerUnitTests()
        {
            _bookingServiceMock = new Mock<IBookingService>();
        }

        [TestMethod]
        public void Get_Bookings_Return_Ok()
        {
            var bookings = _fixture.CreateMany<Booking>(3).ToList();

            _bookingServiceMock.Setup(b => b.Get()).Returns(bookings);
            _bookingController = new BookingsController(_bookingServiceMock.Object);

            var result = _bookingController.GetBookings();
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Get_Bookings_ById_Return_Ok()
        {
            var booking = _fixture.Create<Booking>();
            var bookingId = booking.Id;

            _bookingServiceMock.Setup(b => b.Get(bookingId)).Returns(booking);
            _bookingController = new BookingsController(_bookingServiceMock.Object);

            var result = _bookingController.GetBooking(bookingId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Post_Booking_Return_Ok()
        {
            var booking = _fixture.Create<Booking>();

            _bookingServiceMock.Setup(service => service.Post(It.IsAny<Booking>())).Returns(booking);
            _bookingController = new BookingsController(_bookingServiceMock.Object);

            var result = _bookingController.PostBooking(booking);
            var obj = result as ObjectResult;

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestMethod]
        public void Put_Booking_Return_Ok()
        {
            var booking = _fixture.Create<Booking>();

            _bookingServiceMock.Setup(servise => servise.Put(It.IsAny<Booking>())).Returns(booking);
            _bookingController = new BookingsController(_bookingServiceMock.Object);

            var result = _bookingController.PutBooking(booking.Id, booking);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Delete_Booking_Return_Ok()
        {
            _bookingServiceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(true);
            _bookingController = new BookingsController(_bookingServiceMock.Object);

            var result = _bookingController.DeleteBooking(It.IsAny<int>());
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}
