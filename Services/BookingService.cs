namespace BookStore.Services
{
    using BookStore.Data;
    using BookStore.Interfaces;
    using BookStore.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class BookingService : IBookingService
    {
        private readonly BookStoreContext _dbContext;

        public BookingService(BookStoreContext context)
        {
            _dbContext = context;
        }
        public bool Delete(Booking model)
        {
            _dbContext.Remove(model);
            return Save();
        }

        public bool Exists(int id)
        {
            return _dbContext.Booking.Any(b => b.Id == id);
        }

        public List<Booking> Get()
        {
            var result = _dbContext.Booking.Include(b => b.Product).Include(b => b.Status).Include(b => b.User).ToList();
            return result;
        }

        public Booking Get(int id)
        {
            var result =  _dbContext.Booking
                .Include(b => b.Product)
                .Include(b => b.Status)
                .Include(b => b.User)
                .Where(b => b.Id == id).FirstOrDefault();
            return result;
        }

        public bool Post(Booking model)
        {
            _dbContext.Add(model);
            return Save();
        }

        public bool Put(Booking model)
        {
            _dbContext.Update(model);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public IQueryable<Booking> Bookings()
        {
            return _dbContext.Booking.Select(b => b);
        }

        public List<User> GetUsers()
        {
            var result = _dbContext.User.ToList();
            return result;
        }

        public List<Product> GetProducts()
        {
            var result = _dbContext.Product.Include(p => p.Bookings).Include(p => p.BookStoreItems).ToList();
            return result;
        }

        public Product GetProduct(int id)
        {
            var result = Get(id).Product;
            return result;
        }

        public List<Status> GetStatuses()
        {
            var result = _dbContext.Status.Include(s => s.Bookings).ToList();
            return result;
        }
    }
}
