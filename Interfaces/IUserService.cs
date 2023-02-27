using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Interfaces
{
    public interface IUserService : IBaseService
    {
        List<User> Get();

        User Get(int id);

        bool Post(User model);

        bool Put(User model);

        bool Delete(User model);

        List<Role> GetRoles();

        public IQueryable<User> Users();
    }
}
