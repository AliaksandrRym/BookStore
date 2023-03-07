namespace BookStore.UnitTests
{
    using BookStore.DTO;
    using AutoFixture;
    using BookStore.Controllers;
    using BookStore.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using BookStore.Models;
    using BookStore.Enums;

    [TestClass]
    public class BookingControllerUIpartUnitTests : BaseTests
    {
        private Mock<IBookingService> _bookingServiceMock;
        private BookingController? _bookingController;

        public BookingControllerUIpartUnitTests()
        {
            _bookingServiceMock = new Mock<IBookingService>();
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void BookingController_Index_SearchIsEmpty_Test()
        {
            var bookingList = _fixture.CreateMany<Booking>(3).ToList();
            _bookingServiceMock.Setup(b => b.Get()).Returns(bookingList);
            _bookingServiceMock.Setup(b => b.Bookings()).Returns(bookingList.AsQueryable());
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.Index(String.Empty);
            var obj = result as ViewResult;
            var bookings = obj.Model as List<Booking>;

            Assert.AreEqual(3, bookings.Count());
            Assert.IsInstanceOfType<ViewResult>(result);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void BookingController_Index_WithSerachParam_Test()
        {
            var bookingList = _fixture.CreateMany<Booking>(3).ToList();
            var search = bookingList.First().Product.Name;
            _bookingServiceMock.Setup(b => b.Get()).Returns(bookingList);
            _bookingServiceMock.Setup(b => b.Bookings()).Returns(bookingList.AsQueryable());
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.Index(search);
            var obj = result as ViewResult;
            var bookings = obj.Model as List<Booking>;

            Assert.AreEqual(1, bookings.Count());
            Assert.IsTrue(bookings.TrueForAll(b => b.Product.Name.Contains(search)));
            Assert.IsInstanceOfType<ViewResult>(result);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Booking_Details_Test()
        {
            var booking = _fixture.Create<Booking>();
            var bookingId = booking.Id;
            var Adress = booking.Delivery_Adress;

            _bookingServiceMock.Setup(b => b.Get(bookingId)).Returns(booking);
            _bookingServiceMock.Setup(servise => servise.Exists(bookingId)).Returns(true);
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.Details(bookingId);
            var obj = result as ViewResult;
            var bookingDetails = obj.Model as Booking;

            Assert.AreEqual(Adress, bookingDetails.Delivery_Adress);
            Assert.IsInstanceOfType<ViewResult>(result);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void BookingController_Create_Test()
        {
            var productList = _fixture.CreateMany<Product>(3).ToList();
            _bookingServiceMock.Setup(b => b.GetProducts()).Returns(productList);

            var statusList = _fixture.CreateMany<Status>(3).ToList();
            _bookingServiceMock.Setup(b => b.GetStatuses()).Returns(statusList);

            Booking? createdBooking = null;

            var booking = _fixture.Create<Booking>();
            booking.Product = productList.First();
            var product = booking.Product;

            _bookingServiceMock.Setup(b => b.GetProduct(booking.Id)).Returns(product);
            _bookingServiceMock.Setup(service => service.Post(It.IsAny<Booking>())).Callback<Booking>(b => createdBooking = b);
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.Create(booking.Product.Name, _mapper.Map<BookingDto>(booking));
            _bookingServiceMock.Verify(s => s.Post(It.IsAny<Booking>()), Times.Once);

            Assert.AreEqual(createdBooking.Delivery_Adress, booking.Delivery_Adress);
            Assert.AreEqual(createdBooking.Delivery_Time, booking.Delivery_Time);
            Assert.AreEqual(createdBooking.Delivery_date, booking.Delivery_date);
            Assert.AreEqual((int)Statuses.SUBMITED, createdBooking.StatusId);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void BookingController_Create_RedirectToActionResult_Test()
        {
            var productList = _fixture.CreateMany<Product>(3).ToList();
            _bookingServiceMock.Setup(b => b.GetProducts()).Returns(productList);

            var statusList = _fixture.CreateMany<Status>(3).ToList();
            _bookingServiceMock.Setup(b => b.GetStatuses()).Returns(statusList);

            var booking = _fixture.Create<Booking>();
            booking.Product = productList.First();
            var product = booking.Product;

            _bookingServiceMock.Setup(service => service.Post(It.IsAny<Booking>())).Returns(true);
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);
            var result = _bookingController.Create(product.Name, _mapper.Map<BookingDto>(booking));
            var obj = result as RedirectToActionResult;

            Assert.AreEqual("Index", obj.ActionName);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void BookingController_Edit_RedirectToActionResult_Test()
        {
            var booking = _fixture.Create<Booking>();
            var bookingId = booking.Id;
            _bookingServiceMock.Setup(u => u.Get(bookingId)).Returns(booking);
            _bookingServiceMock.Setup(servise => servise.Exists(bookingId)).Returns(true);
            _bookingServiceMock.Setup(servise => servise.Put(It.IsAny<Booking>())).Returns(true);
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);
            var updBooking = _mapper.Map<BookingDto>(booking);
            var result = _bookingController.Edit(bookingId, updBooking);
            var obj = result as RedirectToActionResult;

            Assert.AreEqual("Index", obj.ActionName);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Booking_Controller_Edit_Test()
        {
            var updBookingAddress = "Updated Address";

            var booking = _fixture.Create<Booking>();
            var id = booking.Id;
            booking.Delivery_Adress = updBookingAddress;
            Booking? updBooking = null;

            _bookingServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _bookingServiceMock.Setup(service => service.Put(It.IsAny<Booking>())).Callback<Booking>(b => updBooking = b);
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.Edit(id, _mapper.Map<BookingDto>(booking));
            _bookingServiceMock.Verify(b => b.Put(It.IsAny<Booking>()), Times.Once);

            Assert.AreEqual(updBooking.Delivery_Adress, updBookingAddress);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void BookingController_Delete_Test()
        {
            var booking = _fixture.Create<Booking>();
            var id = booking.Id;
            _bookingServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _bookingServiceMock.Setup(service => service.Delete(It.IsAny<Booking>())).Returns(true);
            _bookingController = new BookingController(_bookingServiceMock.Object, _mapper);

            var result = _bookingController.DeleteConfirmed(id);
            var obj = result as RedirectToActionResult;

            Assert.AreEqual("Index", obj.ActionName);
        }
    }
}
