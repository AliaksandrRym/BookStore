namespace BookStore.Services
{
    using BookStore.Data;
    using BookStore.Interfaces;
    using BookStore.Properties.Models;
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
            var result = _dbContext.Booking.ToList();
            return result;
        }

        public Booking Get(int id)
        {
            var result = _dbContext.Booking.Where(b => b.Id == id).FirstOrDefault();
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
    }
}
