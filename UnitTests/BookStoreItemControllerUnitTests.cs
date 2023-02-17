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
            var storeList = _fixture.CreateMany<BookStoreItem>(3).ToList();
            _bookStoreServiceMock.Setup(store => store.Get()).Returns(storeList);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object, _mapper);

            var result = _bookStoreController.GetBookStoreItems();
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Get_BookStoreItem_ById_Return_Ok()
        {
            var store = _fixture.Create<BookStoreItem>();
            var storeId = store.Id;

            _bookStoreServiceMock.Setup(u => u.Get(storeId)).Returns(store);
            _bookStoreServiceMock.Setup(servise => servise.Exists(store.Id)).Returns(true);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object, _mapper);

            var result = _bookStoreController.GetBookStoreItem(storeId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Post_BookStoreItem_Return_Ok()
        {
            var store = _mapper.Map<BookStoreItemDto>(_fixture.Create<BookStoreItem>());

            _bookStoreServiceMock.Setup(service => service.Post(It.IsAny<BookStoreItem>())).Returns(true);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object, _mapper);

            var result = _bookStoreController.PostBookStoreItem(store);
            var obj = result as ObjectResult;

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestMethod]
        public void Put_BookStoreItem_Return_Ok()
        {
            var store = _fixture.Create<BookStoreItem>();
            _bookStoreServiceMock.Setup(servise => servise.Put(It.IsAny<BookStoreItem>())).Returns(true);
            _bookStoreServiceMock.Setup(servise => servise.Exists(store.Id)).Returns(true);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object, _mapper);
            var updStore = _mapper.Map<BookStoreItemDto>(store);

            var result = _bookStoreController.PutBookStoreItem(store.Id, updStore);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }

        [TestMethod]
        public void Delete_BookStoreItem_Return_Ok()
        {
            var store = _fixture.Create<BookStoreItem>();
            var id = store.Id;
            _bookStoreServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _bookStoreServiceMock.Setup(service => service.Delete(It.IsAny<BookStoreItem>())).Returns(true);
            _bookStoreController = new BookStoreItemsController(_bookStoreServiceMock.Object, _mapper);

            var result = _bookStoreController.DeleteBookStoreItem(id);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }
    }
}
