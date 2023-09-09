using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.Domain.Models;
using Challenge.UI.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.UI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrdersController(ILogger<OrdersController> logger, IOrderRepository orderRepository,
        ICustomerRepository customerRepository, IProductRepository productRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    [HttpGet(Name = "GetAllOrders")]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        return Ok(await _orderRepository.ListAllAsync());
    }

    [HttpGet("{id:guid}", Name = "GetOrderById")]
    public async Task<ActionResult<Order>> GetOrderById(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Order>> CreateOrder(
        [FromBody] OrderDto orderBody
    )
    {
        var customer = await _customerRepository.FindOrCreateCustomer(orderBody.CustomerName);
        var product = await _productRepository.FindOrCreateProduct(orderBody.ProductName);
        var order = _orderRepository.AddOrderForCustomer(customer, product);

        return Ok(order);
    }
}