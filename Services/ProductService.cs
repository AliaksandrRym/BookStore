﻿namespace BookStore.Services
{
    using BookStore.Data;
    using BookStore.Interfaces;
    using BookStore.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly BookStoreContext _dbContext;

        public ProductService(BookStoreContext context)
        {
            _dbContext = context;
        }
        public bool Delete(Product model)
        {
            _dbContext.Remove(model);
            return Save();
        }

        public bool Exists(int id)
        {
            return _dbContext.Product.Any(u => u.Id == id);
        }

        public List<Product> Get()
        {
            var result = _dbContext.Product.Include(p => p.Bookings).Include(p => p.BookStoreItems).ToList();
            return result;
        }

        public Product Get(int id)
        {
            var result = _dbContext.Product
                .Include(p => p.BookStoreItems)
                .Include(p => p.Bookings)
                .Where(p => p.Id == id).FirstOrDefault();
            return result;
        }

        public bool Post(Product model)
        {
            _dbContext.Add(model);
            return Save();
        }

        public bool Put(Product model)
        {
            _dbContext.Update(model);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public IQueryable<Product> Products()
        {
            return _dbContext.Product.Select(p => p);
        }
    }
}
