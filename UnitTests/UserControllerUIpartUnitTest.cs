namespace BookStore.UnitTests
{
    using AutoFixture;
    using BookStore.Controllers;
    using BookStore.DTO;
    using BookStore.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using User = Models.User;

    [TestClass]
    public class UserControllerUIpartUnitTest : BaseTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserController? _userController;

        public UserControllerUIpartUnitTest()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public void UserController_Index_SearchIsEmpty_Test()
        {
            var userList = _fixture.CreateMany<User>(3).ToList();
            _userServiceMock.Setup(user => user.Get()).Returns(userList);
            _userServiceMock.Setup(user => user.Users()).Returns(userList.AsQueryable());
            _userController = new UserController(_userServiceMock.Object, _mapper);

            var result = _userController.Index(String.Empty);
            var obj = result as ViewResult;
            var users = obj.Model as List<SecureUserDto>;

            Assert.AreEqual(users.Count(), 3);
            Assert.IsInstanceOfType<ViewResult>(result);          
        }

        [TestMethod]
        public void UserController_Index_WithSerachParam_Test()
        {
            var userList = _fixture.CreateMany<User>(3).ToList();
            var search = userList.First().Name;
            _userServiceMock.Setup(user => user.Get()).Returns(userList);
            _userServiceMock.Setup(user => user.Users()).Returns(userList.AsQueryable());
            _userController = new UserController(_userServiceMock.Object, _mapper);

            var result = _userController.Index(search);
            var obj = result as ViewResult;
            var users = obj.Model as List<SecureUserDto>;

            Assert.AreEqual(users.Count(), 1);
            Assert.AreEqual(search, users.First().Name);
            Assert.IsInstanceOfType<ViewResult>(result);
        }

        [TestMethod]
        public void UserController_Details_Test()
        {
            var user = _fixture.Create<User>();
            var userId = user.Id;
            var userName = user.Name;

            _userServiceMock.Setup(u => u.Get(userId)).Returns(user);
            _userServiceMock.Setup(servise => servise.Exists(user.Id)).Returns(true);
            _userController = new UserController(_userServiceMock.Object, _mapper);

            var result = _userController.Details(userId);
            var obj = result as ViewResult;
            var userDetails = obj.Model as User;

            Assert.AreEqual(userName, userDetails.Name);
            Assert.IsInstanceOfType<ViewResult>(result);
        }

        [TestMethod]
        public void UserController_Create_Test()
        {
            User? createdUser = null;

            _userServiceMock.Setup(service => service.Post(It.IsAny<User>())).Callback<User>(u => createdUser = u);
            var user = _mapper.Map<UserDto>(_fixture.Create<User>());
            _userController = new UserController(_userServiceMock.Object, _mapper);

            var result = _userController.Create(user);
            _userServiceMock.Verify(s => s.Post(It.IsAny<User>()), Times.Once);

            Assert.AreEqual(createdUser.Name, user.Name);
            Assert.AreEqual(createdUser.Login, user.Login);
            Assert.AreEqual(createdUser.Adress, user.Adress);
        }

        [TestMethod]
        public void UserController_Create_RedirectToActionResult_Test()
        {
            var user = _mapper.Map<UserDto>(_fixture.Create<User>());

            _userServiceMock.Setup(service => service.Post(It.IsAny<User>())).Returns(true);
            _userController = new UserController(_userServiceMock.Object, _mapper);
            var result = _userController.Create(user);
            var obj = result as RedirectToActionResult;

            Assert.AreEqual(obj.ActionName, "Index");
        }

        [TestMethod]
        public void UserController_Edit_RedirectToActionResult_Test()
        {
            var user = _fixture.Create<User>();
            var userId = user.Id;
            _userServiceMock.Setup(u => u.Get(userId)).Returns(user);
            _userServiceMock.Setup(servise => servise.Exists(userId)).Returns(true);
            _userServiceMock.Setup(servise => servise.Put(It.IsAny<User>())).Returns(true);
            _userController = new UserController(_userServiceMock.Object, _mapper);
            var updUser = _mapper.Map<UserDto>(user);
            var result = _userController.Edit(userId, updUser);
            var obj = result as RedirectToActionResult;

            Assert.AreEqual(obj.ActionName, "Index");
        }

        [TestMethod]
        public void UserController_Edit_Test()
        {
            var updUserName = "Updated Name";

            var user = _fixture.Create<User>();
            var id = user.Id;
            user.Name = updUserName;
            User? updUser = null;

            _userServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _userServiceMock.Setup(service => service.Put(It.IsAny<User>())).Callback<User>(u => updUser = u);
            _userController = new UserController(_userServiceMock.Object, _mapper);
            
            var result = _userController.Edit(id, _mapper.Map<UserDto>(user));
            _userServiceMock.Verify(s => s.Put(It.IsAny<User>()), Times.Once);

            Assert.AreEqual(updUser.Name, updUserName);
        }

        [TestMethod]
        public void UserController_Delete_Test()
        {
            var user = _fixture.Create<User>();
            var id = user.Id;
            _userServiceMock.Setup(servise => servise.Exists(id)).Returns(true);
            _userServiceMock.Setup(service => service.Delete(It.IsAny<User>())).Returns(true);
            _userController = new UserController(_userServiceMock.Object, _mapper);

            var result = _userController.DeleteConfirmed(id);
            var obj = result as RedirectToActionResult;

            Assert.AreEqual(obj.ActionName, "Index");
        }
    }
}
