namespace BookStore.Properties.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Booking>? Bookings { get; set; }

        public Status() 
        {
            Bookings = new List<Booking>();
        }
    }
}
