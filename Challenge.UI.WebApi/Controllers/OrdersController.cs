using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.Domain.Models;
using Challenge.UI.WebApi.DataTransfer;
using Challenge.UI.WebApi.Mappers;
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
    [ProducesResponseType(typeof(IEnumerable<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        return Ok((await _orderRepository.ListAllAsync()).ToDto());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrderById([FromRoute] Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return order.ToDto();
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder(
        [FromBody] OrderRequestDto orderRequestBody
    )
    {
        var customer = await _customerRepository.FindOrCreateCustomer(orderRequestBody.Customer);
        var product = await _productRepository.FindOrCreateProduct(orderRequestBody.Product);
        var order = await _orderRepository.AddOrderForCustomer(customer, product);

        return CreatedAtAction(nameof(GetOrderById), new {id = order.Id}, order.ToDto());
    }
}