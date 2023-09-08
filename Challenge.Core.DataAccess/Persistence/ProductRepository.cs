using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Core.DataAccess.Persistence;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ChallengeDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Product?> FindProductByName(string name)
    {
        return await Context.Products.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Product> FindOrCreateProduct(string name)
    {
        var product = await FindProductByName(name);

        if (product is not null)
        {
            return product;
        }

        return await AddAsync(new Product
        {
            Name = name
        });
    }
}