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
            var p = GetByIdAsync(product.Id).Result;
            if (p.Description == description) return;
            p.Description = description;
            Update(p);
        }
    }
}
