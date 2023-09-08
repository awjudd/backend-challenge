using Challenge.Core.Domain.Models;

namespace Challenge.Core.DataAccess.Contracts.Persistence;

public interface ICustomerRepository : IAsyncRepository<Customer>
{
    Task<Customer?> FindCustomerByName(string name);

    Task<Customer> FindOrCreateCustomer(string name);
}