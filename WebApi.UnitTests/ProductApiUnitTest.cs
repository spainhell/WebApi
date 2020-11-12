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

        /// <summary>
        /// Return all products
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetProductsAsync()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetProductsAsync));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);

            // get response from Controller
            var response = await controller.GetAllProducts();
            var result = response as OkObjectResult;
            var value = result.Value as IList<Product>;
            dbContext.Dispose();

            Assert.True(value.Count() == 14);
        }

        /// <summary>
        /// Return existing product item
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetProductItemAsync()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetProductItemAsync));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);
            int id = 14;

            // get response from Controller
            var response = await controller.GetProduct(id);
            var result = response as OkObjectResult;
            var value = result.Value as Product;

            dbContext.Dispose();

            Assert.True(value.Id == 14);
        }

        /// <summary>
        /// Looking for non-exist product id
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetProductItemAsyncBadRequest()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetProductItemAsyncBadRequest));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);
            int id = 99;

            // get response from Controller
            var response = await controller.GetProduct(id);
            var result = response as NotFoundResult;

            dbContext.Dispose();

            Assert.True(result != null);
        }

        /// <summary>
        /// Update product description
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestPutProductDescriptionAsync()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestPutProductDescriptionAsync));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);
            int id = 1;

            var product = new ProductDescription()
            {
                Description = "Nový popis položky"
            };
            
            // get response from Controller
            var response = await controller.UpdateProductDescription(id, product);
            var result = response as NoContentResult;

            dbContext.Dispose();

            Assert.True(result != null);
        }

        /// <summary>
        /// Update non-exist product description
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestPutProductDescriptionBadRequestAsync()
        {
            // prepare DB and Controller
            var dbContext = DbContextMocker.GetDbContext(nameof(TestPutProductDescriptionBadRequestAsync));
            var prodService = new ProductService(new UnitOfWork(dbContext));
            var controller = new ProductsController(null, prodService);
            int id = 1;

            var product = new ProductDescription()
            {
                Description = "Nový popis položky"
            };

            // get response from Controller
            var response = await controller.UpdateProductDescription(id, product);
            var result = response as NoContentResult;

            dbContext.Dispose();

            Assert.True(result != null);
        }
    }
}