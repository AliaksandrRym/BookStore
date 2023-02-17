using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BookStore.Models;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext (DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Role { get; set; } = default!;

        public DbSet<User> User { get; set; } = default!;

        public DbSet<Booking> Booking { get; set; } = default!;

        public DbSet<Product> Product { get; set; } = default!;

        public DbSet<BookStoreItem> BookStore { get; set; } = default!;

        public DbSet<Status> Status { get; set; } = default!;

    }
}
