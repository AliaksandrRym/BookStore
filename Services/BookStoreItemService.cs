namespace BookStore.Services
{
    using BookStore.Data;
    using BookStore.Interfaces;
    using BookStore.Properties.Models;
    using System.Collections.Generic;

    public class BookStoreItemService : IBookStoreService
    {
        private readonly BookStoreContext _dbContext;

        public BookStoreItemService (BookStoreContext context)
        {
            _dbContext= context;    
        }
        public bool Delete(int id)
        {
            var bookStoreItem = _dbContext.BookStore.Find(id);
            _dbContext.BookStore.Remove(bookStoreItem);

            var product = _dbContext.Product.Find(bookStoreItem.Product_Id);
            product.BookStoreItems.Remove(bookStoreItem);

            _dbContext.SaveChanges();
            return true;
        }

        public List<BookStoreItem> Get()
        {
            var result = _dbContext.BookStore.ToList();
            return result;
        }

        public BookStoreItem Get(int id)
        {
            var result = _dbContext.BookStore.Find(id);
            return result;
        }

        public BookStoreItem Post(BookStoreItem model)
        {
            var product = _dbContext.Product.Where(p => p.Id == model.Product_Id).FirstOrDefault();
            model.Product = product;
            model.Product_Name = product.Name;
            product.BookStoreItems.Add(model);
            _dbContext.BookStore.Add(model);
            _dbContext.SaveChanges();
            return model;
        }

        public BookStoreItem Put(BookStoreItem model)
        {
            var bookStoreItem = _dbContext.BookStore.Where(b => b.Id == model.Id).FirstOrDefault();
            bookStoreItem.Product_Name = model.Product_Name;
            bookStoreItem.Product_Id = model.Product_Id;
            bookStoreItem.Sold = model.Sold;
            bookStoreItem.Available = model.Available;
            bookStoreItem.Booked = model.Booked;
            _dbContext.SaveChanges();
            return model;
        }
    }
}
