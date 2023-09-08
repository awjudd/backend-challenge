using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Core.DataAccess.Persistence;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ChallengeDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Customer?> FindCustomerByName(string name)
    {
        return await Context.Customers.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Customer> FindOrCreateCustomer(string name)
    {
        var customer = await FindCustomerByName(name);

        if (customer is not null)
        {
            return customer;
        }

        return await AddAsync(new Customer
            {
                Name = name
            }
        );
    }
}