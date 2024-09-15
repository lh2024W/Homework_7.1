using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7._1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null;
        public DbSet<Product> Products { get; set; } = null;
        public DbSet<Order> Orders { get; set; } = null;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null;

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(e => e.Date).HasDefaultValueSql("GETDATE()");
        }
    }
}
