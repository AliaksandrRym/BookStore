using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IProductService: IBaseService
    {
        List<Product> Get();

        Product Get(int id);

        bool Post(Product model);

        bool Put(Product model);

        bool Delete(Product model);
    }
}
