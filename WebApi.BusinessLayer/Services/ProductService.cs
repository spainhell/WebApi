using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.DomainLayer.Model;
using WebApi.DomainLayer.Repositories;
using WebApi.DomainLayer.Services;

namespace WebApi.BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<Product> InsertProduct(Product newProduct)
        {
            await _unitOfWork.Products.InsertAsync(newProduct);
            await _unitOfWork.CommitAsync();
            return newProduct;
        }

        public async Task UpdateProduct(Product originalProduct, Product newProduct)
        {
            originalProduct.Name = newProduct.Name;
            originalProduct.ImgUri = newProduct.ImgUri;
            originalProduct.Price = newProduct.Price;
            originalProduct.Description = newProduct.Description;
            _unitOfWork.Products.Update(originalProduct);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateDescription(Product product, string description)
        {
            if (product.Description == description) return;
            _unitOfWork.Products.UpdateDescription(product, description);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.CommitAsync();
        }
    }
}
