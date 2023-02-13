
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Properties.Models;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly BookStoreContext _dbContext;

        public UserService (BookStoreContext context)
        {
            _dbContext = context;
        }

        public bool Delete(int id)
        {
            var user = _dbContext.User.Find(id);
            _dbContext.User.Remove(user);
            var role = _dbContext.Role.Find(user.Role_Id);
            role.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }

        public List<User> Get()
        {
            var result = _dbContext.User.ToList();
            return result;
        }

        public User Get(int id)
        {
            var result = _dbContext.User.Find(id);
            return result;
        }

        public User Post(User model)
        {
            var role = _dbContext.Role.Find(model.Role_Id);
            role.Users.Add(model);
            _dbContext.User.AddAsync(model);
            _dbContext.SaveChangesAsync();
            return model;
        }

        public User Put(User model)
        {
            var user = _dbContext.User.Where(u => u.Id == model.Id).FirstOrDefault();
            user.Adress = model.Adress;
            user.Login = model.Login;
            user.Email = model.Email;
            user.Name= model.Name;
            user.Role_Id = model.Role_Id;
            _dbContext.SaveChangesAsync();
            return model;
        }
    }
}
