using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCTAssignmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FCTAssignmentApp.Data
{
    public class CustomerContext
    : DbContext
    {
        public CustomerContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Purchase>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Item 1", Price = 10.55 },
                new Product { Id = 2, Name = "Item 2", Price = 25.99 },
                new Product { Id = 3, Name = "Item 3", Price = 5.15 },
                new Product { Id = 4, Name = "Test item 1", Price = 99.55 },
                new Product { Id = 5, Name = "Test item 2", Price = 20.30 },
                new Product { Id = 6, Name = "Test item 3", Price = 9.99 },
                new Product { Id = 7, Name = "Abc item 1", Price = 159.99 },
                new Product { Id = 8, Name = "Abc Item 2", Price = 11.99 },
                new Product { Id = 9, Name = "Running Shoes", Price = 59.99 });
        }
    }
}
