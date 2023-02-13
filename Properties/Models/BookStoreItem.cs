namespace BookStore.Properties.Models
{
    public class BookStoreItem
    {
        public int Id { get; set; }

        public string? Product_Name { get; set; }

        public int Available { get; set; }

        public int Booked { get; set; }

        public int Sold { get; set; }

        public int Product_Id { get; set; }

        public Product? Product { get; set; }
    }
}
