using BookStore.Properties.Models;

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
    }   
}
