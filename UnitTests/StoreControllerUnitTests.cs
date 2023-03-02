
namespace BookStore.UnitTests
{
    using AutoFixture;
    using BookStore.Controllers;
    using BookStore.DTO;
    using BookStore.Enums;
    using BookStore.Interfaces;
    using BookStore.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class StoreControllerUnitTests: BaseTests
    {
        private Mock<IBookStoreService> _storeServiceMock;
        private BookStoreItemController? _storeController;

        public StoreControllerUnitTests()
        {
            _storeServiceMock = new Mock<IBookStoreService>();
        }

        [TestMethod]
        public void StoreController_Index_SearchIsEmpty_Test()
        {
            var storeList = _fixture.CreateMany<BookStoreItem>(3).ToList();
            _storeServiceMock.Setup(s => s.Get()).Returns(storeList);
            _storeServiceMock.Setup(s => s.BookStoreItems()).Returns(storeList.AsQueryable());
            _storeController = new BookStoreItemController(_storeServiceMock.Object, _mapper);

            var result = _storeController.Index(String.Empty);
            var obj = result as ViewResult;
            var items = obj.Model as List<BookStoreItem>;

            Assert.AreEqual(items.Count(), 3);
            Assert.IsInstanceOfType<ViewResult>(result);
        }

        [Ignore]
        [TestMethod]
        public void StoreItem_Create_Test()
        {
            var productList = _fixture.CreateMany<Product>(3).ToList();
            _storeServiceMock.Setup(b => b.Products()).Returns(productList);

            var item = _fixture.Create<BookStoreItem>();
            item.Product = productList.First();
            var product = item.Product;

            _storeServiceMock.Setup(service => service.Post(It.IsAny<BookStoreItem>())).Returns(true);
            _storeController = new BookStoreItemController(_storeServiceMock.Object, _mapper);
            var result = _storeController.Create(product.Name, _mapper.Map<BookStoreItemDto>(item));
            var obj = result as RedirectToActionResult;

            Assert.AreEqual(obj.ActionName, "Index");
        }

        [Ignore]
        [TestMethod]
        public void StoreController_Create_Test()
        {
            var prName = "Test product";
            var item = _fixture.Create<BookStoreItem>();
            item.Product.Name = prName; 

            var productList = _fixture.CreateMany<Product>(3).ToList();
            productList.First().Name = prName;
            productList.SelectMany(p => p.Bookings).ToList().ForEach(b => b.StatusId = (int)Statuses.SUBMITED);
            var booking = productList.SelectMany(p => p.Bookings).ToList().First();
            booking.StatusId = (int)Statuses.CANCELED;
            booking = productList.SelectMany(p => p.Bookings).ToList().Last();
            booking.StatusId = (int)Statuses.APPROVED;
            item.Product.Bookings = productList.SelectMany(p => p.Bookings).ToList();            
           _storeServiceMock.Setup(b => b.Products()).Returns(productList);
            _storeServiceMock.Setup(service => service.Post(It.IsAny<BookStoreItem>())).Returns(true);
            _storeController = new BookStoreItemController(_storeServiceMock.Object, _mapper);
            var result = _storeController.Create(prName, _mapper.Map<BookStoreItemDto>(item));
            var obj = result as RedirectToActionResult;

            Assert.AreEqual(obj.ActionName, "Index");
        }

        [TestMethod]
        public void StoreController_Delete_Test()
        {
            var item = _fixture.Create<BookStoreItem>();
            var id = item.Id;
            _storeServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _storeServiceMock.Setup(service => service.Delete(It.IsAny<BookStoreItem>())).Returns(true);
            _storeController = new BookStoreItemController(_storeServiceMock.Object, _mapper);

            var result = _storeController.DeleteConfirmed(id);
            var obj = result as RedirectToActionResult;

            Assert.AreEqual(obj.ActionName, "Index");
        }
    }
}
