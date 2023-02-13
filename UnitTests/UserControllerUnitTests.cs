using AutoFixture;
using BookStore.Controllers;
using BookStore.Interfaces;
using BookStore.Properties.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;

namespace BookStore.UnitTests
{
    [TestClass]
    public class UserControllerUnitTests: BaseTests
    {

        private Mock<IUserService> _userServiceMock;
        private UsersController? _usersController;

        public UserControllerUnitTests()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public void Get_Users_Return_Ok()
        {
            var userList = _fixture.CreateMany<User>(3).ToList();

            _userServiceMock.Setup(user => user.Get()).Returns(userList);
            _usersController = new UsersController(_userServiceMock.Object);

            var result = _usersController.GetUsers();
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }


        [TestMethod]
        public void Get_User_ById_Return_Ok()
        {
            var user = _fixture.Create<User>();
            var userId = user.Id;

            _userServiceMock.Setup(u => u.Get(userId)).Returns(user);
            _usersController = new UsersController(_userServiceMock.Object);

            var result = _usersController.GetUser(userId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Post_User_Return_Ok()
        {
            var user = _fixture.Create<User>();

            _userServiceMock.Setup(service => service.Post(It.IsAny<User>())).Returns(user);
            _usersController = new UsersController(_userServiceMock.Object);

            var result = _usersController.PostUser(user);
            var obj = result as ObjectResult;

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestMethod]
        public void Put_User_Return_Ok()
        {
            var user = _fixture.Create<User>();

            _userServiceMock.Setup(servise => servise.Put(It.IsAny<User>())).Returns(user);
            _usersController = new UsersController(_userServiceMock.Object);


            var result = _usersController.PutUser(user.Id, user);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Delete_User_Return_Ok()
        {
            _userServiceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(true);
            _usersController = new UsersController(_userServiceMock.Object);

            var result = _usersController.DeleteUser(It.IsAny<int>());
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}
