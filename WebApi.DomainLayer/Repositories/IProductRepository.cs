using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.DomainLayer.Model;

namespace WebApi.DomainLayer.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task UpdateDescriptionAsync(Product product);
    }
}
