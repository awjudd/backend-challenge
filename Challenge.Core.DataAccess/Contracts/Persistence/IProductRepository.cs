using Challenge.Core.Domain.Models;

namespace Challenge.Core.DataAccess.Contracts.Persistence;

public interface IProductRepository : IAsyncRepository<Product>
{
    Task<Product?> FindProductByName(string name);

    Task<Product> FindOrCreateProduct(string name);
}