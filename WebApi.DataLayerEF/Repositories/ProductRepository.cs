using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.DomainLayer.Model;
using WebApi.DomainLayer.Repositories;

namespace WebApi.DataLayerEF.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public void UpdateDescription(Product product, string description)
        {
            // this logic moved to BusinessLayer:
            //var p = GetByIdAsync(product.Id).Result;
            //if (product.Description == description) return;

            product.Description = description;
            Update(product);
        }
    }
}
