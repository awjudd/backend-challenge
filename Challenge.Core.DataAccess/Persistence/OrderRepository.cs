using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Core.DataAccess.Persistence;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ChallengeDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Order> AddOrderForCustomer(Customer customer, Product product,
        CancellationToken cancellationToken = default)
    {
        var order = await FindProductOrdersForCustomer(customer, product, cancellationToken);

        if (order is null)
        {
            order = new Order
            {
                Customer = customer,
                Product = product,
                Quantity = 1
            };

            await AddAsync(order);
        }
        else
        {
            order.Quantity++;

            await UpdateAsync(order);
        }

        return order;
    }

    public async Task<IEnumerable<Order>> FindOrdersForCustomer(Customer customer,
        CancellationToken cancellationToken = default)
    {
        return await Context.Orders.Where(x => x.Customer.Name == customer.Name).ToListAsync();
    }

    public async Task<IEnumerable<(Customer, IEnumerable<Order>)>> GetOrdersByCustomer(
        CancellationToken cancellationToken = default)
    {
        return await Context.Orders
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .GroupBy(x => x.Customer)
            .Select(x => new ValueTuple<Customer, IEnumerable<Order>>(x.Key, x.ToList()))
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> FindProductOrdersForCustomer(Customer customer, Product product,
        CancellationToken cancellationToken = default)
    {
        return (await FindOrdersForCustomer(customer, cancellationToken)).FirstOrDefault(x => x.Product.Name == product.Name);
    }
}