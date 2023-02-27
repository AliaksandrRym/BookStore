using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Interfaces
{
    public interface IBookingService: IBaseService
    {
        List<Booking> Get();

        Booking Get(int id);

        bool Post(Booking model);

        bool Put(Booking model);

        bool Delete(Booking model);

        bool Save();

        public IQueryable<Booking> Bookings();

        public List<User> GetUsers();

        public List<Product> GetProducts();

        public List<Status> GetStatuses();

        public Product GetProduct(int id);
    }   
}
