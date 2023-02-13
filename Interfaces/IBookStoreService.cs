namespace BookStore.Interfaces
{
    using BookStore.Properties.Models;

    public interface IBookStoreService
    {
        List<BookStoreItem> Get();

        BookStoreItem Get(int id);

        BookStoreItem Post(BookStoreItem model);

        BookStoreItem Put(BookStoreItem model);

        bool Delete(int id);
    }
}
