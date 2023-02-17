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
    public class ProductControllerUnitTests: BaseTests
    {
        private Mock<IProductService> _productServiceMock;
        private ProductsController? _productController;

        public ProductControllerUnitTests()
        {
            _productServiceMock = new Mock<IProductService>();
        }

        [TestMethod]
        public void Get_Products_Return_Ok()
        {
            var productList = _fixture.CreateMany<Product>(3).ToList();

            _productServiceMock.Setup(p => p.Get()).Returns(productList);
            _productController = new ProductsController(_productServiceMock.Object, _mapper);

            var result =  _productController.GetProducts();
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Get_Product_ById_Return_Ok()
        {
            var product = _fixture.Create<Product>();
            var productId = product.Id;

            _productServiceMock.Setup(u => u.Get(productId)).Returns(product);
            _productServiceMock.Setup(servise => servise.Exists(productId)).Returns(true);
            _productController = new ProductsController(_productServiceMock.Object, _mapper);

            var result = _productController.GetProduct(productId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Post_Product_Return_Ok()
        {
            var product = _mapper.Map<ProductDto>(_fixture.Create<Product>());

            _productServiceMock.Setup(service => service.Post(It.IsAny<Product>())).Returns(true);
            _productController = new ProductsController(_productServiceMock.Object, _mapper);

            var result = _productController.PostProduct(product);
            var obj = result as ObjectResult;

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestMethod]
        public void Put_Product_Return_Ok()
        {
            var product = _fixture.Create<Product>();

            _productServiceMock.Setup(servise => servise.Put(It.IsAny<Product>())).Returns(true);
            _productServiceMock.Setup(servise => servise.Exists(product.Id)).Returns(true);
            _productController = new ProductsController(_productServiceMock.Object, _mapper);
            var updProduct = _mapper.Map<ProductDto>(product);

            var result = _productController.PutProduct(product.Id, updProduct);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }

        [TestMethod]
        public void Delete_Product_Return_Ok()
        {
            var product = _fixture.Create<Product>();
            var id = product.Id;
            _productServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _productServiceMock.Setup(service => service.Delete(It.IsAny<Product>())).Returns(true);
            _productController = new ProductsController(_productServiceMock.Object, _mapper);

            var result = _productController.DeleteProduct(id);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }
    }
}
