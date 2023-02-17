namespace BookStore.Models
{
    public class Role
    {
        public int id { get; set; }

        public string Role_Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
