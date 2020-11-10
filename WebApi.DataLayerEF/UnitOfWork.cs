using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataLayerEF.Database;
using WebApi.DataLayerEF.Repositories;
using WebApi.DomainLayer.Repositories;

namespace WebApi.DataLayerEF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private ProductRepository _productRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
