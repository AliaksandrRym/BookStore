namespace BookStore.Tests
{
    using BookStore.Constants;
    using BookStore.Properties.Models;
    using BookStore.Tests.TestData;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;

    [TestClass]
    public class UsersIntegtationTests: BaseIntegrationTestsClass
    {
        [TestMethod]
        public async Task Get_Users_Return_Ok()
        { 
            var response = await _client.GetAsync( Endpoints.User);
            var users = await _client.GetFromJsonAsync<List<User>>(Endpoints.User);
            Assert.IsTrue(users.Any(), "Users List is empty");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET api/Users is not {HttpStatusCode.OK}");
         }

        [TestMethod]
        public async Task Get_User_ById_Test()
        {
            var users = await _client.GetFromJsonAsync<List<User>>(Endpoints.User);
            var userFromList = users.First();
            var user = await _client.GetFromJsonAsync<User>(Endpoints.User + "/" + userFromList.Id);
            var response = await _client.GetAsync(Endpoints.User + "/" + userFromList.Id);

            Assert.AreEqual(userFromList.Name, user.Name);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Status code for GET api/Users/{userFromList.Id} is not {HttpStatusCode.OK}");
        }

        [TestMethod]
        public async Task Post_User_Test()
        {
            var users = await _client.GetFromJsonAsync<List<User>>(Endpoints.User);
            var testUser = new TestUser();
            User user = new User
            {
                Name = testUser.Name,
                Email = testUser.Email,
                Password = testUser.Password,
                Role_Id = testUser.Role_Id,
                Login = testUser.Login,
                Adress = testUser.Adress,
                Role = null,
                Bookings= null,
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(Endpoints.User, user);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"Status code for POST api/Users is not {HttpStatusCode.OK}");
        }


        [TestMethod]
        public async Task Delete_User_ById_Test()
        {
            var testUser = new TestUser();
            User user = new User
            {
                Name = testUser.Name,
                Email = testUser.Email,
                Password = testUser.Password,
                Role_Id = testUser.Role_Id,
                Login = testUser.Login,
                Adress = testUser.Adress,
                Role = null,
                Bookings = null,
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(Endpoints.User, user);
            var users = await _client.GetFromJsonAsync<List<User>>(Endpoints.User);
            var id = users.Where(u => u.Name == testUser.Name).First().Id;
            var deleteResponse = await _client.DeleteAsync(Endpoints.User +"/" + id);

            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode, $"Status code for Delete api/Users {id} is not {HttpStatusCode.OK}");
        }
    }
}
