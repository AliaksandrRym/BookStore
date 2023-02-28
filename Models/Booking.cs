using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string Delivery_Adress { get; set; }

        public DateTime Delivery_date { get; set; }

        public DateTime Delivery_Time { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
