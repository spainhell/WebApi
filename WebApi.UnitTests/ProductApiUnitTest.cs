using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.BusinessLayer.Services;
using WebApi.DataLayerEF;
using WebApi.DomainLayer.Model;
using WebApi.RestApi.Controllers;
using WebApi.RestApi.DTO;

namespace WebApi.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGetProductsAsync()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetProductsAsync));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);

            // get response from Controller
            var response = await controller.GetAllProducts();
            var value = response.Value;

            dbContext.Dispose();

            Assert.True(value.Count() == 14);
        }

        [Test]
        public async Task TestGetProductItemAsync()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetProductsAsync));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);
            int id = 15;

            // get response from Controller
            var response = await controller.GetProduct(id);
            var value = response.Value;

            dbContext.Dispose();

            Assert.True(value.Id == 15);
        }

        [Test]
        public async Task TestPutProductDescriptionAsync()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetProductsAsync));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);
            int id = 1;

            var product = new ProductDescription()
            {
                Description = "Nový popis položky"
            };
            
            // get response from Controller
            var response = await controller.UpdateProductDescription(id, product);

            dbContext.Dispose();

            Assert.True(response is NoContentResult);
        }
    }
}