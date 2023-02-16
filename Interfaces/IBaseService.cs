using BookStore.Properties.Models;

namespace BookStore.Interfaces
{
    public interface IBaseService
    {
        bool Exists(int id);

        bool Save();
    }
}
