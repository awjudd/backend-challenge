using Challenge.Core.Domain.Models;

namespace Challenge.Core.Application.Contracts.Persistence;

public interface ICustomerRepository: IAsyncRepository<Customer>
{
    
}