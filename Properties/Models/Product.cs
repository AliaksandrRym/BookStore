namespace BookStore.Properties.Models
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

        public List<Booking>? Bookings { get; set; }

        public List<BookStoreItem>? BookStoreItems { get; set; }

        public Product()
        { 
            BookStoreItems = new List<BookStoreItem>();
            Bookings = new List<Booking>();
        }
    }
}
