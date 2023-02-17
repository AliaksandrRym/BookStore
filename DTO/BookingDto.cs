using BookStore.Models;

namespace BookStore.DTO
{
    public class BookingDto
    {
        public int Id { get; set; }

        public string Delivery_Adress { get; set; }

        public DateTime Delivery_date { get; set; }

        public DateTime Delivery_Time { get; set; }

        public int UserId { get; set; }

        public int StatusId { get; set; }

        public int ProductId { get; set; }

    }
}
