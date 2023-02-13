using System.ComponentModel.DataAnnotations;

namespace BookStore.Properties.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string Delivery_Adress { get; set; }

        public DateTime Delivery_date { get; set; }

        public DateTime Delivery_Time { get; set; }

        public int User_Id { get; set; }

        public int Status_Id { get; set; }

        public int Product_Id { get; set; }

        // Navigation properties

        public User? User { get; set; }

        public Status? Status { get; set; }

        public Product? Product { get; set; }

        public Booking() 
        { 
            User = new User();
            Product = new Product();
            Status= new Status();
        }

    }
}
