namespace BookStore.Models
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

        public int RoleId { get; set; }

        // Navigation properties  

        public Role Role { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
