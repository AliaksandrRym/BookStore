using BookStore.Properties.Models;
namespace BookStore.Interfaces
{
    public interface IUserService : IBaseService
    {
        List<User> Get();

        User Get(int id);

        bool Post(User model);

        bool Put(User model);

        bool Delete(User model);
    }
}
