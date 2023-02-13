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
        public bool Delete(int id)
        {
            var booking = _dbContext.Booking.Find(id);
            _dbContext.Booking.Remove(booking);

            var user = _dbContext.User.Find(booking.User_Id);
            user.Bookings.Remove(booking);

            var status = _dbContext.Status.Find(booking.Status_Id);
            status.Bookings.Remove(booking);

            var product = _dbContext.Product.Find(booking.Product_Id);
            product.Bookings.Remove(booking);

            _dbContext.SaveChanges();
            return true;
        }

        public List<Booking> Get()
        {
            var result = _dbContext.Booking.ToList();
            return result;
        }

        public Booking Get(int id)
        {
            var result = _dbContext.Booking.Find(id);
            return result;
        }

        public Booking Post(Booking model)
        {
            var user = _dbContext.User.Find(model.User_Id);
            user.Bookings.Add(model);
            var status = _dbContext.Status.Where(s => s.Id == model.Status_Id).FirstOrDefault();
            status.Bookings.Add(model);
            var product = _dbContext.Product.Find(model.Product_Id);
            product.Bookings.Add(model);
            model.Product = product;
            model.Status = status;
            model.Product = product;
            _dbContext.Booking.Add(model);
            _dbContext.SaveChanges();
            return model;
        }

        public Booking Put(Booking model)
        {
            var booking = _dbContext.Booking.Where(b => b.Id == model.Id).FirstOrDefault();
            booking.Delivery_Adress = model.Delivery_Adress;
            booking.Delivery_date = model.Delivery_date;
            booking.Delivery_Time = model.Delivery_Time;

            booking.User_Id = model.User_Id;
            booking.User = _dbContext.User.Find(model.User_Id);

            booking.Status_Id= model.Status_Id;
            booking.Status = _dbContext.Status.Find(model.Status_Id);

            booking.Product_Id= model.Product_Id;
            booking.Product = _dbContext.Product.Find(model.Product_Id);

            _dbContext.SaveChanges();
            return model;
        }
    }
}
