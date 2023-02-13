using BookStore.Properties.Models;

namespace BookStore.Interfaces
{
    public interface IProductService
    {
        List<Product> Get();

        Product Get(int id);

        Product Post(Product model);

        Product Put(Product model);

        bool Delete(int id);
    }
}
