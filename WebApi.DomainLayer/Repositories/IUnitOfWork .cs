using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DomainLayer.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        Task<int> CommitAsync();
    }
}
