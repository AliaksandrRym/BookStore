namespace BookStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public string? Image_Path { get; set; }

        //Navigation properties

        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<BookStoreItem>? BookStoreItems { get; set; }

        public Product()
        {
            Bookings = new List<Booking>();
            BookStoreItems = new List<BookStoreItem>();
        }
    }
}
