namespace BookStore.Services
{
    using BookStore.Data;
    using BookStore.Interfaces;
    using BookStore.Properties.Models;

    public class UserService : IUserService
    {
        private readonly BookStoreContext _dbContext;

        public UserService (BookStoreContext context)
        {
            _dbContext = context;
        }

        public bool Delete(User model)
        {
            _dbContext.Remove(model);
            return Save();
        }

        public List<User> Get()
        {
            var result = _dbContext.User.ToList();
            return result;
        }

        public User Get(int id)
        {
            var result = _dbContext.User.Where(u => u.Id == id).FirstOrDefault();
            return result;
        }

        public bool Post(User model)
        {
            _dbContext.Add(model);
            return Save();
        }

        public bool Put(User model)
        {
            _dbContext.Update(model);
            return Save();
        }

        public bool Exists(int id)
        {
            return _dbContext.User.Any(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true: false;
        }
    }
}
