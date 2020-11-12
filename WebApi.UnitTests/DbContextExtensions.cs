using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApi.DataLayerEF.Database;
using WebApi.DomainLayer.Model;

namespace WebApi.UnitTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this DataContext dbContext)
        {
            // add some products
            dbContext.Products.Add(new Product()
                {Id = 1, Name = "Item 1", ImgUri = "http://", Price = 111.0m, Description = "Descr 1"});

            dbContext.Products.Add(new Product()
                { Id = 2, Name = "Item 2", ImgUri = "http://", Price = 211.0m, Description = "Descr 2" });

            dbContext.Products.Add(new Product()
                { Id = 3, Name = "Item 3", ImgUri = "http://", Price = 311.0m, Description = "Descr 3" });

            dbContext.Products.Add(new Product()
                { Id = 4, Name = "Item 4", ImgUri = "http://", Price = 411.0m, Description = "Descr 4" });

            dbContext.Products.Add(new Product()
                { Id = 5, Name = "Item 5", ImgUri = "http://", Price = 511.0m, Description = "Descr 5" });

            dbContext.Products.Add(new Product()
                { Id = 6, Name = "Item 6", ImgUri = "http://", Price = 611.0m, Description = "Descr 6" });

            dbContext.Products.Add(new Product()
                { Id = 7, Name = "Item 7", ImgUri = "http://", Price = 711.0m, Description = "Descr 7" });

            dbContext.Products.Add(new Product()
                { Id = 8, Name = "Item 8", ImgUri = "http://", Price = 811.0m, Description = "Descr 8" });

            dbContext.Products.Add(new Product()
                { Id = 9, Name = "Item 9", ImgUri = "http://", Price = 911.0m, Description = "Descr 9" });

            dbContext.Products.Add(new Product()
                { Id = 10, Name = "Item 10", ImgUri = "http://", Price = 1011.0m, Description = "Descr 10" });

            dbContext.Products.Add(new Product()
                { Id = 11, Name = "Item 11", ImgUri = "http://", Price = 1111.0m, Description = "Descr 11" });

            dbContext.Products.Add(new Product()
                { Id = 12, Name = "Item 12", ImgUri = "http://", Price = 1211.0m, Description = "Descr 12" });

            dbContext.Products.Add(new Product()
                { Id = 13, Name = "Item 13", ImgUri = "http://", Price = 1311.0m, Description = "Descr 13" });

            dbContext.Products.Add(new Product()
                { Id = 14, Name = "Item 14", ImgUri = "http://", Price = 1411.0m, Description = "Descr 14" });

            dbContext.SaveChanges();
        }
    }
}
