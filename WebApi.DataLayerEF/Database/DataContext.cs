using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApi.DomainLayer.Model;

namespace WebApi.DataLayerEF.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Lampa", ImgUri = "http://127.0.0.1/lampa.jpg", Price = 299.50m, Description = "obyčejná lampa" },
                new Product { Id = 2, Name = "Stůl", ImgUri = "http://127.0.0.1/stul.jpg", Price = 699.99m, Description = "neobyčejný stůl" },
                new Product { Id = 3, Name = "Skříň", ImgUri = "http://127.0.0.1/skrin.jpg", Price = 1611.50m, Description = "skvělá skříň" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
