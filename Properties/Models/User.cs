namespace BookStore.Properties.Models
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string? Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int Role_Id { get; set; }

        // Navigation properties  
        public Role? Role { get; set; }

        public List<Booking>? Bookings {get; set;}

        public User() 
        {
            Bookings = new List<Booking>();
        }   
    }
}
