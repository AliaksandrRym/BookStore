namespace BookStore.Models
{
    public class BookStoreItem
    {
        public int Id { get; set; }

        public int Available { get; set; }

        public int Booked { get; set; }

        public int Sold { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
