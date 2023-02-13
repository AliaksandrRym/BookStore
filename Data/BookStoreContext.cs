using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStore.Properties.Models;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext (DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<BookStore.Properties.Models.Role> Role { get; set; } = default!;

        public DbSet<BookStore.Properties.Models.User> User { get; set; } = default!;

        public DbSet<BookStore.Properties.Models.Booking> Booking { get; set; } = default!;

        public DbSet<BookStore.Properties.Models.Product> Product { get; set; } = default!;

        public DbSet<BookStore.Properties.Models.BookStoreItem> BookStore { get; set; } = default!;

        public DbSet<BookStore.Properties.Models.Status> Status { get; set; } = default!;
    }
}
