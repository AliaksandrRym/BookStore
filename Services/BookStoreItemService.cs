namespace BookStore.Services
{
    using BookStore.Data;
    using BookStore.Interfaces;
    using BookStore.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class BookStoreItemService : IBookStoreService
    {
        private readonly BookStoreContext _dbContext;

        public BookStoreItemService (BookStoreContext context)
        {
            _dbContext= context;    
        }
        public bool Delete(BookStoreItem model)
        {
            _dbContext.Remove(model);
            return Save();
        }

        public bool Exists(int id)
        {
            return _dbContext.BookStore.Any(b => b.Id == id);
        }

        public List<BookStoreItem> Get()
        {
            var result = _dbContext.BookStore.Include(s => s.Product).ToList();
            return result;
        }

        public BookStoreItem Get(int id)
        {
            var result = _dbContext.BookStore.Include(b => b.Product).Where(b => b.Id == id).FirstOrDefault();
            return result;
        }

        public bool Post(BookStoreItem model)
        {
            _dbContext.Add(model);
            return Save();
        }

        public bool Put(BookStoreItem model)
        {
            _dbContext.Update(model);
            return Save();
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
        public IQueryable<BookStoreItem> BookStoreItems()
        {
            return _dbContext.BookStore.Select(s => s);
        }

        public List<Product> Products()
        {
            return _dbContext.Product.Include(p => p.BookStoreItems).Include(p => p.Bookings).ToList();
        }
    }
}
