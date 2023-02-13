namespace BookStore.Properties.Models
{
    public class Role
    {
        public int id { get; set; }

        public string Role_Name { get; set; }

        public List<User>? Users { get; set; }

        public Role() 
        { 
            Users = new List<User>();
        }  
    }
}
