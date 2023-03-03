
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
    public class UserControllerUnitTests: BaseTests
    {

        private Mock<IUserService> _userServiceMock;
        private UsersController? _usersController;

        public UserControllerUnitTests()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Get_Users_Return_Ok()
        {
            var userList =_fixture.CreateMany<User>(3).ToList();
            _userServiceMock.Setup(user => user.Get()).Returns(userList);
            _usersController = new UsersController(_userServiceMock.Object, _mapper);

            var result = _usersController.GetUsers();
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Get_User_ById_Return_Ok()
        {
            var user = _fixture.Create<User>();
            var userId = user.Id;

            _userServiceMock.Setup(u => u.Get(userId)).Returns(user);
             _userServiceMock.Setup(servise => servise.Exists(user.Id)).Returns(true);
            _usersController = new UsersController(_userServiceMock.Object, _mapper);

            var result = _usersController.GetUser(userId);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Post_User_Return_Ok()
        {
            var user = _mapper.Map<UserDto>(_fixture.Create<User>());

            _userServiceMock.Setup(service => service.Post(It.IsAny<User>())).Returns(true);
            _usersController = new UsersController(_userServiceMock.Object, _mapper);

            var result = _usersController.PostUser(user);
            var obj = result as ObjectResult;

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Put_User_Return_Ok()
        {
            var user = _fixture.Create<User>();

            _userServiceMock.Setup(servise => servise.Put(It.IsAny<User>())).Returns(true);
            _userServiceMock.Setup(servise => servise.Exists(user.Id)).Returns(true);
            _usersController = new UsersController(_userServiceMock.Object, _mapper);
            var updUser = _mapper.Map<UserDto>(user);

            var result = _usersController.PutUser(user.Id, updUser);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }

        [TestCategory("UnitTest")]
        [TestMethod]
        public void Delete_User_Return_Ok()
        {
            var user = _fixture.Create<User>();
            var id = user.Id;
            _userServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _userServiceMock.Setup(service => service.Delete(It.IsAny<User>())).Returns(true);
            _usersController = new UsersController(_userServiceMock.Object, _mapper);

            var result = _usersController.DeleteUser(id);
            var obj = result as ObjectResult;

            Assert.AreEqual(204, obj.StatusCode);
        }
    }
}
