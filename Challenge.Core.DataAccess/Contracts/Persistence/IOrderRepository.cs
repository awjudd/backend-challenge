using Challenge.Core.Domain.Models;

namespace Challenge.Core.DataAccess.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<Order>
{
    Task<Order> AddOrderForCustomer(Customer customer, Product product, CancellationToken cancellationToken = default);

    Task<IEnumerable<Order>> FindOrdersForCustomer(Customer customer, CancellationToken cancellationToken = default);

    Task<IEnumerable<(Customer, IEnumerable<Order>)>> GetOrdersByCustomer(CancellationToken cancellationToken = default);
}