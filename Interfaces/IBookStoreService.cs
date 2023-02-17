namespace BookStore.Interfaces
{
    using BookStore.Models;

    public interface IBookStoreService: IBaseService
    {
        List<BookStoreItem> Get();

        BookStoreItem Get(int id);

        bool Post(BookStoreItem model);

        bool Put(BookStoreItem model);

        bool Delete(BookStoreItem model);
    }
}
