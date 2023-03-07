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
    public class ProductControllerUIPartUnitTests: BaseTests
    {
        private Mock<IProductService> _productServiceMock;
        private ProductController? _productController;

        public ProductControllerUIPartUnitTests()
        {
            _productServiceMock = new Mock<IProductService>();
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void ProductController_Index_SearchIsEmpty_Test()
        {
            var productList = _fixture.CreateMany<Product>(3).ToList();
            _productServiceMock.Setup(p => p.Get()).Returns(productList);
            _productServiceMock.Setup(p => p.Products()).Returns(productList.AsQueryable());
            _productController = new ProductController(_productServiceMock.Object, _mapper);

            var result = _productController.Index(String.Empty);
            var obj = result as ViewResult;
            var products = obj.Model as List<Product>;

            Assert.AreEqual(3, products.Count());
            Assert.IsInstanceOfType<ViewResult>(result);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void ProductController_Create_Test()
        {
            Product? createdProduct = null;

            _productServiceMock.Setup(service => service.Post(It.IsAny<Product>())).Callback<Product>(p => createdProduct = p);
            var product = _mapper.Map<ProductDto>(_fixture.Create<Product>());
            _productController = new ProductController(_productServiceMock.Object, _mapper);

            var result = _productController.Create(product);
            _productServiceMock.Verify(p => p.Post(It.IsAny<Product>()), Times.Once);

            Assert.AreEqual(createdProduct.Name, product.Name);
            Assert.AreEqual(createdProduct.Author, product.Author);
            Assert.AreEqual(createdProduct.Description, product.Description);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void ProductController_Edit_Test()
        {
            var updProductName = "Updated Product";

            var product = _fixture.Create<Product>();
            var id = product.Id;
            product.Name = updProductName;
            Product? updProduct = null;

            _productServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _productServiceMock.Setup(service => service.Put(It.IsAny<Product>())).Callback<Product>(p => updProduct = p);
            _productController = new ProductController(_productServiceMock.Object, _mapper);

            var result = _productController.Edit(id, _mapper.Map<ProductDto>(product));
            _productServiceMock.Verify(s => s.Put(It.IsAny<Product>()), Times.Once);

            Assert.AreEqual(updProduct.Name, updProductName);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void ProductController_Delete_Test()
        {
            var product = _fixture.Create<Product>();
            var id = product.Id;
            _productServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _productServiceMock.Setup(service => service.Delete(It.IsAny<Product>())).Returns(true);
            _productController = new ProductController(_productServiceMock.Object, _mapper);

            var result = _productController.DeleteConfirmed(id);
            var obj = result as RedirectToActionResult;

            Assert.AreEqual("Index", obj.ActionName);
        }
    }
}
    