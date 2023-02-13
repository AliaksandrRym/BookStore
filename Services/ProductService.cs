using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Properties.Models;

namespace BookStore.Services
{
    public class ProductService : IProductService
    {
        private readonly BookStoreContext _dbContext;

        public ProductService(BookStoreContext context)
        {
            _dbContext = context;
        }
        public bool Delete(int id)
        {
            var product = _dbContext.Product.Find(id);
            _dbContext.Product.Remove(product);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Product> Get()
        {
            var result = _dbContext.Product.ToList();
            return result;
        }

        public Product Get(int id)
        {
            var result = _dbContext.Product.Find(id);
            return result;
        }

        public Product Post(Product model)
        {
            _dbContext.Product.Add(model);
            _dbContext.SaveChanges();
            return model;
        }

        public Product Put(Product model)
        {
            var product = _dbContext.Product.Where(u => u.Id == model.Id).FirstOrDefault();
            product.Author = model.Author;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Image_Path = model.Image_Path;
            product.Name = model.Name;
            _dbContext.SaveChangesAsync();
            return model;
        }
    }
}
