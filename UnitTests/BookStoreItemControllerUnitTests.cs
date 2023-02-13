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
    public class BookStoreItemControllerUnitTests: BaseTests
    {
        private Mock<IBookStoreService> _bookStoreServiceMock;
        private BookStoreItemsController? _bookStoreController;

        public BookStoreItemControllerUnitTests()
        {
            _bookStoreServiceMock = new Mock<IBookStoreService>();
        }

        [TestMethod]
        public void Get_BookstoreItems_Return_Ok()
        {
            var itemsList = _fixture.CreateMany<BookStoreItem>(3).ToList();

            _bookStoreServiceMock.Setup(p => p.Get()).Returns(itemsList);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object);

            var result = _bookStoreController.GetBookStoreItems();
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Get_BookStoreItem_ById_Return_Ok()
        {
            var item = _fixture.Create<BookStoreItem>();
            var itemId = item.Id;

            _bookStoreServiceMock.Setup(i => i.Get(itemId)).Returns(item);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object);

            var result = _bookStoreController.GetBookStoreItem(itemId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Post_BookStoreItem_Return_Ok()
        {
            var item = _fixture.Create<BookStoreItem>();

            _bookStoreServiceMock.Setup(service => service.Post(It.IsAny<BookStoreItem>())).Returns(item);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object);

            var result = _bookStoreController.PostBookStoreItem(item);
            var obj = result as ObjectResult;

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestMethod]
        public void Put_BookStoreItem_Return_Ok()
        {
            var item = _fixture.Create<BookStoreItem>();

            _bookStoreServiceMock.Setup(servise => servise.Put(It.IsAny<BookStoreItem>())).Returns(item);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object);

            var result = _bookStoreController.PutBookStoreItem(item.Id, item);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Delete_BookStoreItem_Return_Ok()
        {
            _bookStoreServiceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(true);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object);

            var result = _bookStoreController.DeleteBookStoreItem(It.IsAny<int>());
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}
