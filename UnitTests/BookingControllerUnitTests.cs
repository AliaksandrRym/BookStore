namespace BookStore.UnitTests
{
    using AutoFixture;
    using BookStore.Controllers;
    using BookStore.DTO;
    using BookStore.Interfaces;
    using BookStore.Models;
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

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Get_Bookings_Return_Ok()
        {
            var bookings = _fixture.CreateMany<Booking>(3).ToList();

            _bookingServiceMock.Setup(b => b.Get()).Returns(bookings);
            _bookingController = new BookingsController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.GetBookings();
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Get_Bookings_ById_Return_Ok()
        {
            var booking = _fixture.Create<Booking>();
            var bookingId = booking.Id;

            _bookingServiceMock.Setup(b => b.Get(bookingId)).Returns(booking);
            _bookingServiceMock.Setup(servise => servise.Exists(bookingId)).Returns(true);
            _bookingController = new BookingsController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.GetBooking(bookingId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Post_Booking_Return_Ok()
        {
            var booking =_mapper.Map<BookingDto>(_fixture.Create<Booking>());
            _bookingServiceMock.Setup(servise => servise.Exists(booking.Id)).Returns(true);
            _bookingServiceMock.Setup(service => service.Post(It.IsAny<Booking>())).Returns(true);
            _bookingController = new BookingsController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.PostBooking(booking);
            var obj = result as ObjectResult;

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Put_Booking_Return_Ok()
        {
            var booking = _fixture.Create<Booking>();
            var id = booking.Id;
            _bookingServiceMock.Setup(servise => servise.Put(It.IsAny<Booking>())).Returns(true);
            _bookingServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _bookingController = new BookingsController(_bookingServiceMock.Object, _mapper);
            var updBooking = _mapper.Map<BookingDto>(booking);

            var result = _bookingController.PutBooking(booking.Id, updBooking);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Delete_Booking_Return_Ok()
        {
            var booking = _fixture.Create<Booking>();
            var id = booking.Id;
            _bookingServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _bookingServiceMock.Setup(service => service.Delete(It.IsAny<Booking>())).Returns(true);
            _bookingController = new BookingsController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.DeleteBooking(id);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }
    }
}
