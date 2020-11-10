using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.DomainLayer.Model;

namespace WebApi.DomainLayer.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> InsertProduct(Product newProduct);
        Task UpdateProduct(Product originalProduct, Product newProduct);
        Task UpdateDescription(Product product, string description);
        Task DeleteProduct(Product product);
    }
}
