using BookStore.Properties.Models;

namespace BookStore.Interfaces
{
    public interface IBookingService
    {
        List<Booking> Get();

        Booking Get(int id);

        Booking Post(Booking model);

        Booking Put(Booking model);

        bool Delete(int id);
    }
}
