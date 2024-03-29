﻿namespace BookStore.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public Status() 
        {
            Bookings = new List<Booking>();
        }
    }
}
