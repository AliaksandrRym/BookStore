using BookStore.Properties.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Interfaces
{
    public interface IUserService
    {
        List<User> Get();

        User Get(int id);

        User Post(User model);

        User Put(User model);

        bool Delete(int id);

    }
}
